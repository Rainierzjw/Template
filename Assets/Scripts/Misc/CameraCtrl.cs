/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： CameraCtrl
* 创建日期：2022-03-06 14:39:55
* 作者名称：zjw
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using App;
using Data;
using DG.Tweening;
using Extend;
using UnityEngine.EventSystems;

namespace Misc
{
    /// <summary>
    /// 
    /// </summary>
    public class CameraCtrl : MonoSingleton<CameraCtrl>
    {
        public enum EMode
        {
            First,
            Third,
            Fly
        }

        public readonly IField<EMode> mode = new Field<EMode>(EMode.First);

        public readonly IField<float> moveSpeed = new Field<float>(1);

        public readonly IField<float> thirdDis = new Field<float>();

        private float _thirdDis;

        public readonly IField<float> rotateSpeed = new Field<float>(40);

        public readonly IField<float> hitDis = new Field<float>(5);

        public readonly IField<bool> orthographic = new Field<bool>();

        public readonly IField<bool> can = new Field<bool>(true);

        private float _camFOV = 60;

        public float camFOV
        {
            get => _camFOV;
            set
            {
                if (value > _camFOV)
                {
                    _camFOV = cam.fieldOfView = Mathf.Min(80, value);
                }
                else if (value < _camFOV)
                {
                    _camFOV = cam.fieldOfView = Mathf.Max(30, value);
                }
            }
        }

        private float _camOS = 5;

        public float camOS
        {
            get => _camOS;
            set
            {
                if (value > _camOS)
                {
                    _camOS = cam.orthographicSize = Mathf.Min(7f, value);
                }
                else if (value < _camOS)
                {
                    _camOS = cam.orthographicSize = Mathf.Max(3.75f, value);
                }
            }
        }

        private float playerHeight;

        private Rigidbody rigidbody;

        private Transform h_tnf;

        private Transform v_tnf;

        public Camera cam { get; private set; }

        private Transform target_tnf;

        private int layerMask;

        protected void Awake()
        {
            layerMask = LayerMask.GetMask("Default");

            rigidbody = transform.GetComponent<Rigidbody>();
            h_tnf = transform.FindTnf2("H");
            v_tnf = transform.FindTnf2("V");
            target_tnf = transform.FindTnf2("Target");
            cam = v_tnf.FindComp<Camera>("Camera");
            thirdDis.onChange += OnChangeThirdDis;
            mode.onChange += OnChangeMode;
            orthographic.onChange += OnChangeOrthographic;
            can.onChange += OnChangeCan;
            playerHeight = transform.GetComponent<CapsuleCollider>().height;
            v_tnf.localEulerAngles = new Vector3(20, 0, 0);

            OnChangeMode(mode.data);
        }

        protected void OnDestory()
        {
            thirdDis.onChange -= OnChangeThirdDis;
            mode.onChange -= OnChangeMode;
            orthographic.onChange -= OnChangeOrthographic;
            can.onChange -= OnChangeCan;
        }

        private void OnChangeMode(EMode obj)
        {
            switch (obj)
            {
                case EMode.First:
                    //rigidbody.MovePosition(cam.transform.position);
                    h_tnf.DOKill();
                    h_tnf.localPosition = new Vector3(0, playerHeight - transform.localPosition.y, 0);
                    target_tnf.gameObject.SetActive(false);
                    thirdDis.data = 0;
                    //rigidbody.useGravity = false;
                    break;
                case EMode.Third:
                    h_tnf.DOKill();
                    h_tnf.DOLocalMove(target_tnf.localPosition, 0.6f);
                    target_tnf.gameObject.SetActive(true);
                    thirdDis.data = 10;
                    //rigidbody.useGravity = false;
                    break;
                case EMode.Fly:
                    rigidbody.MovePosition(cam.transform.position);
                    h_tnf.DOKill();
                    h_tnf.DOLocalMove(Vector3.zero, 0.6f);
                    target_tnf.gameObject.SetActive(false);
                    thirdDis.data = 0;
                    //rigidbody.useGravity = false;
                    break;
            }
        }

        private void OnChangeThirdDis(float val)
        {
            _thirdDis = val;
        }

        private void OnChangeOrthographic(bool val)
        {
            cam.orthographic = val;
        }

        private void OnChangeCan(bool val)
        {
            can.data = val;
        }

        private void Update()
        {
            if (!can.data) return;

            var pos = Vector3.zero;
            var ms = moveSpeed.data * Time.deltaTime;
            if (Input.GetKey(KeyCode.W))
            {
                pos += h_tnf.forward * ms;
            }

            if (Input.GetKey(KeyCode.S))
            {
                pos -= h_tnf.forward * ms;
            }

            if (Input.GetKey(KeyCode.A))
            {
                pos -= h_tnf.right * ms;
            }

            if (Input.GetKey(KeyCode.D))
            {
                pos += h_tnf.right * ms;
            }

            if (Input.GetKey(KeyCode.E))
            {
                switch (mode.data)
                {
                    case EMode.First:
                        h_tnf.localPosition =
                            new Vector3(0, Mathf.Min(h_tnf.localPosition.y + ms, playerHeight / 2), 0);
                        break;
                    case EMode.Fly:
                        pos += h_tnf.up * ms;
                        break;
                }
            }

            if (Input.GetKey(KeyCode.Q))
            {
                switch (mode.data)
                {
                    case EMode.First:
                        h_tnf.localPosition =
                            new Vector3(0, Mathf.Max(h_tnf.localPosition.y - ms, -playerHeight / 2), 0);
                        break;
                    case EMode.Fly:
                        pos -= h_tnf.up * ms;
                        break;
                }
            }

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                var wheel = Input.GetAxis("Mouse ScrollWheel");
                if (Mathf.Abs(wheel) > 0.01f)
                {
                    if (orthographic.data)
                    {
                        camOS += wheel * moveSpeed.data * 0.5f;
                    }
                    else
                    {
                        camFOV += wheel * moveSpeed.data * 3;
                    }
                }
            }

            rigidbody.MovePosition(LimitPos(rigidbody.position + pos));

            var rs = rotateSpeed.data;
            var dt = Time.deltaTime;
            if (Input.GetMouseButton(1))
            {
                h_tnf.localEulerAngles +=
                    new Vector3(0, rs * Input.GetAxis("Mouse X") * dt, 0);
                v_tnf.localEulerAngles -=
                    new Vector3(rs * Input.GetAxis("Mouse Y") * dt, 0, 0);
            }

            cam.transform.localPosition =
                _thirdDis > 0 && Physics.Raycast(new Ray(v_tnf.position, v_tnf.forward * -1), out var hit, _thirdDis,
                    layerMask)
                    ? new Vector3(0, 0, -hit.distance)
                    : new Vector3(0, 0, -_thirdDis);
        }

        public Vector3 GetHitPos()
        {
            if (MouseHit(out var hit, hitDis.data))
            {
                return hit.point;
            }

            return cam.transform.position + cam.ScreenPointToRay(Input.mousePosition).direction * hitDis.data;
        }

        public Vector3 GetHitPos(params string[] layers)
        {
            return MouseHit(out var hit, hitDis.data, layers) ? hit.point : MouseToWorldPos();
        }

        public Vector3 MouseToWorldPos()
        {
            return cam.transform.position + cam.ScreenPointToRay(Input.mousePosition).direction * hitDis.data;
        }

        public Vector3 MouseToWorldPosByDis(float dis)
        {
            return cam.transform.position + cam.ScreenPointToRay(Input.mousePosition).direction * dis;
        }

        public bool MouseHit(out RaycastHit hit, int mask, float dis = Mathf.Infinity)
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hit, dis, mask);
        }

        public bool MouseHit(out RaycastHit hit, float dis = Mathf.Infinity, params string[] layers)
        {
            return MouseHit(out hit, LayerMask.GetMask(layers), dis);
        }

        public bool MouseHit(out RaycastHit hit, float dis = Mathf.Infinity)
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hit, dis);
        }

        public bool MouseHit(out RaycastHit hit, params string[] layers)
        {
            return MouseHit(out hit, Mathf.Infinity, layers);
        }

        public bool Hit(out RaycastHit hit, int mask, float dis = Mathf.Infinity)
        {
            return Physics.Raycast(new Ray(cam.transform.position, cam.transform.forward), out hit, dis, mask);
        }

        public bool Hit(out RaycastHit hit, float dis = Mathf.Infinity, params string[] layers)
        {
            return Hit(out hit, LayerMask.GetMask(layers), dis);
        }

        public bool Hit(out RaycastHit hit, float dis = Mathf.Infinity)
        {
            return Physics.Raycast(new Ray(cam.transform.position, cam.transform.forward), out hit, dis);
        }

        public bool Hit(out RaycastHit hit, params string[] layers)
        {
            return Hit(out hit, Mathf.Infinity, layers);
        }

        public void MoveToTarget(Transform tnf, Vector3 direction, float dis, float time, bool isorthographic = false)
        {
            var targetPos = tnf.position + direction.normalized * dis;
            var pos = new Vector3(targetPos.x, transform.position.y, targetPos.z); //transform.y
            transform.DOMove(pos, time);
            h_tnf.DOMoveY(targetPos.y, time);
            if (isorthographic)
            {
                v_tnf.DOLocalRotate(new Vector3(0, 0, 0), time);
                h_tnf.DOLocalRotate(new Vector3(0, 180, 0), time);
            }
        }

        private Vector3 LimitPos(Vector3 originV3)
        {
            Vector3 resultV3 = Vector3.one;
            //resultV3.x = Mathf.Max(-3.5f, Mathf.Min(3.5f, originV3.x));
            //resultV3.y = Mathf.Max(0, Mathf.Min(2, originV3.y));
            //resultV3.z = Mathf.Max(-5, Mathf.Min(6, originV3.z)); 
            resultV3.x = Mathf.Max(-10, originV3.x);
            resultV3.y = Mathf.Max(0, Mathf.Min(2, originV3.y));
            resultV3.z = Mathf.Max(-10, originV3.z);
            return resultV3;
        }
    }
}