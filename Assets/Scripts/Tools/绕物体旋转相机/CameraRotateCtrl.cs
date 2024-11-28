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
using DG.Tweening;
using App;
using SceneComponent;
using UnityEditor;
using System;

namespace Misc
{
    /// <summary>
    /// 
    /// </summary>
    public class CameraRotateCtrl : /*MonoSingleton<CameraRotateCtrl>*/MonoBehaviour
    {
        // Camera target to look at.
        //相机跟随目标
        public Transform target;

        //相机旋转角度
        public Vector3 CameraRotation;

        // Exposed vars for the camera position from the target.
        //从目标到摄像机位置的外露vars.
        public float height = 20f;
        public float distance = 20f;

        // Camera limits.
        //相机移动范围
        public float min = 10f;
        public float max = 60;

        // Options.
        //public bool doRotate;
        //相机旋转以及缩放功能开关
        public bool doZoom;
        public bool doRotate;

        // The movement amount when zooming.缩放时的移动量。
        public float zoomStep = 30f;
        public float zoomSpeed = 5f;
        private float heightWanted;
        private float distanceWanted;


        public float xSpeed = 3.0f;
        public float ySpeed = 3.0f;

        public float yMinLimit = -20f;
        public float yMaxLimit = 80f;

        public float xMinLimit = 30f;
        public float xMaxLimit = 220f;

        // public float distanceMin = 1.5f;
        // public float distanceMax = 15f;

        public float smoothTime = 2f;

        // 控制相机XY轴旋转的参数
        [HideInInspector] public float rotationYAxis = 230.0f;
        [HideInInspector] public float rotationXAxis = -8.0f;

        float velocityX = 0.0f;
        float velocityY = 0.0f;

        //两根手指
        private Touch oldTouch1;

        private Touch oldTouch2;
        //Vector2 m_screenPos = Vector2.zero; //记录手指触碰的位置

        float scaleFactor;

        // Result vectors.
        private Vector3 zoomResult; //缩放后坐标
        private Quaternion rotationResult;
        private Vector3 targetAdjustedPosition;
        private Quaternion rotation;


        public Vector3 Position; //当前摄像机的位置
        public Vector3 Rotation; //当前摄像机的角度

        public bool IsInit = false;
        /// <summary>
        /// 摄像机
        /// </summary>
        private Camera camera;

        [HideInInspector] public Transform newTarget;
        [HideInInspector] public Vector4 newAimVector2;

        private void Awake()
        {
            //_Instance = this;
            camera = transform.GetComponent<Camera>();
        }

        void Start()
        {
            init();
        }

        void init()
        {
            Position = transform.position;
            rotation = transform.rotation;

            //得到相机欧拉角
            Vector3 angles = transform.eulerAngles;
            //相机绕Y轴转动的角度值
            rotationYAxis = angles.y;
            //相机绕X轴转动的角度值
            rotationXAxis = angles.x;
            print("相机初始位置" + rotationXAxis);
            //print("Y轴数值"+ rotationYAxis);
            //print("X轴数值" + rotationXAxis);

            // Initialise default zoom vals.
            //相机当前高度赋值于目标高度
            heightWanted = height;
            distanceWanted = distance;
            // Setup our default camera. We set the zoom result to be our default position.
            zoomResult = new Vector3(0f, height, -distance);
        }

        public static float InitAngle = -90;
        public float CurrAngle = 45;
        public float WantedScale = 20; //想要的缩进大小

        void Update()
        {

        }

        void LateUpdate()
        {
            //if(GlobalParameter.openUI)
            //    return;
            if (IsInit == true)
            {
                //distanceWanted = WantedScale;
                rotationXAxis = 25;
                DOTween.To(() => distanceWanted, x => distanceWanted = x, 19, 0.01f);
                //DOTween.To(() => rotationXAxis, x => rotationXAxis = x, CurrAngle, 0.5f);
                //回复位置会颤抖，修改为直接改变摄像机位置
                //SECBuFenLiuTi.Instance.aimCamera.transform.localPosition = new Vector3(0, 0.0243f, -0.138f);
                //SECBuFenLiuTi.Instance.aimCamera.transform.localEulerAngles = new Vector3(10, 0, 0);
                camera.fieldOfView = 60;
                rotationYAxis = 0;
                IsInit = false;
            }


            // Check target.
            //检测目标是否存在
            if (!target)
            {
                //Debug.LogError("This camera has no target, you need to assign a target in the inspector.");
                return;
            }

            //相机视角缩放
            if (doZoom)
            {
                #region 原视角缩放方法
                //print(doRotate);
                //if (Input.touchCount <= 0)
                //{
                // return;
                //}
                //float mouseInput;
                //if (Input.touchCount > 1)
                //{
                //    Touch newTouch1 = Input.GetTouch(0);
                //    Touch newTouch2 = Input.GetTouch(1);
                //    //第2点刚开始接触屏幕, 只记录，不做处理  
                //    if (newTouch2.phase == TouchPhase.Began)
                //    {
                //        oldTouch2 = newTouch2;
                //        oldTouch1 = newTouch1;
                //        //return;
                //    }

                //    //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
                //    float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
                //    float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);
                //    //两个距离只差，为正表示放大，为负表示缩小
                //    float offset = newDistance - oldDistance;
                //    //缩放因子
                //    scaleFactor = offset / 1000f;

                //    mouseInput = scaleFactor;

                //    heightWanted -= zoomStep * mouseInput;
                //    distanceWanted -= zoomStep * mouseInput;
                //}

                // Record our mouse input. If we zoom add this to our height and distance.
                //记录鼠标滚轮滚动时的变量 并赋值记录
                //mouseInput特性：正常状态为0；滚轮前推一格变为+0.1一次，后拉则变为-0.1一次
                // Input.GetAxis("Mouse ScrollWheel");
                //if (Input.touchCount <= 0)
                //{
                //    mouseInput = Input.GetAxis("Mouse ScrollWheel");

                //    heightWanted -= zoomStep * mouseInput;
                //    distanceWanted -= zoomStep * mouseInput;
                //}
                //print("+++"+mouseInput);

                // Make sure they meet our min/max values.
                //限制相机高度范围
                //heightWanted = Mathf.Clamp(heightWanted, min, max);
                //distanceWanted = Mathf.Clamp(distanceWanted, min, max);
                //差值计算，动态修改相机高度值（平滑的变化）
                //height = Mathf.Lerp(height, heightWanted, Time.deltaTime * zoomSpeed);
                //distance = Mathf.Lerp(distance, distanceWanted, Time.deltaTime * zoomSpeed);

                // Post our result.
                //缩放后坐标
                //zoomResult = new Vector3(0f, height, -distance);
                #endregion
                if (Input.GetAxis("Mouse ScrollWheel") < 0 && camera.fieldOfView <= 80)
                {
                    camera.fieldOfView += 2;
                }
                else if (Input.GetAxis("Mouse ScrollWheel") > 0 && camera.fieldOfView >= 10)
                {
                    camera.fieldOfView -= 2;
                }
            }

            //相机视角旋转
            if (doRotate)
            {
                //print("水平" + Input.GetAxis("Horizontal"));
                //print("竖直" + Input.GetAxis("Vertical"));
                if (Input.touchCount == 1)
                {
                    Touch newTouch1 = Input.GetTouch(0);
                    //Touch touch = Input.GetTouch(0);
                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        oldTouch1 = newTouch1;
                        //m_screenPos = touch.position;
                    }

                    if (Input.touches[0].phase == TouchPhase.Moved)
                    {
                        float CX = newTouch1.position.x - oldTouch1.position.x;
                        float CY = newTouch1.position.y - oldTouch1.position.y;

                        velocityX += xSpeed * CX * 0.02f * Time.deltaTime;
                        velocityY += ySpeed * CY * 0.02f * Time.deltaTime;
                    }
                }

                if (Input.GetMouseButton(0))//Input.GetMouseButton(2) || Input.GetMouseButton(0) || 
                {
                    // print("欧拉角"+transform.eulerAngles);
                    velocityX += xSpeed * Input.GetAxis("Mouse X") * 0.02f;
                    velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
                }

                rotationYAxis += velocityX;
                rotationXAxis -= velocityY;
                if (rotationXAxis >= xMaxLimit)
                {
                    rotationXAxis = xMaxLimit;
                }
                else if (rotationXAxis <= xMinLimit)
                {
                    rotationXAxis = xMinLimit;
                }

                if (yMaxLimit != 360 && yMinLimit != -360)
                {
                    if (rotationYAxis >= yMaxLimit)
                    {
                        rotationYAxis = yMaxLimit;
                    }
                    else if (rotationYAxis <= yMinLimit)
                    {
                        rotationYAxis = yMinLimit;
                    }
                }
            }

            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            //相机跟随
            Vector3 position = rotation * negDistance + target.position;
            //改变相机Rotation，从而旋转相机
            transform.rotation = rotation;


            //将缩放后的坐标作为相机的当前坐标位置
            transform.position = position;
            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            //限制相机转动角度
            return Mathf.Clamp(angle, min, max);
        }

        public void InitPoint()
        {
            heightWanted = min;
            distanceWanted = min;

        }

        public void InitReturn(float a, float b)
        {
            heightWanted = a;
            distanceWanted = b;
        }



        public void ChangeTarget(Transform newTarget, float time = 0.75f,Action onChanged = null)
        {
            target = null;
            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            //相机跟随
            Vector3 position = rotation * negDistance + newTarget.position;
            ////改变相机Rotation，从而旋转相机
            //transform.rotation = rotation;
            ////将缩放后的坐标作为相机的当前坐标位置
            //transform.position = position;

            transform.DORotateQuaternion(rotation, time);
            transform.DOMove(position, time).OnComplete(() => 
            {
                target = newTarget;
                onChanged?.Invoke();
            });

        }

        public void ResetRotate()
        {
            transform.position = new Vector3(0, 0, /*-10*/-distance);
            transform.eulerAngles = Vector3.zero;

            Position = transform.position;
            rotation = transform.rotation;

            //得到相机欧拉角
            Vector3 angles = transform.eulerAngles;
            //相机绕Y轴转动的角度值
            rotationYAxis = angles.y;
            //相机绕X轴转动的角度值
            rotationXAxis = angles.x;

            camera.fieldOfView = 60;

            //相机当前高度赋值于目标高度
            heightWanted = height;
            distanceWanted = distance;
            // Setup our default camera. We set the zoom result to be our default position.
            zoomResult = new Vector3(0f, height, -distance);
        }

        public void ChangeToRotate(float rx, float ry, float FOV = 60, float time = 0.75f, Action action = null)
        {
            DOTween.To(() => camera.fieldOfView, f => camera.fieldOfView = f, FOV, time);
            DOTween.To(() => rotationXAxis, x => rotationXAxis = x, rx, time);
            DOTween.To(() => rotationYAxis, y => rotationYAxis = y, ry, time).OnComplete(() => action?.Invoke());
        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(CameraRotateCtrl))]
    public class CameraRotateCtrlEditor : Editor
    {
        bool showFoldout = false;

        bool changeTarget = true;

        bool setRotation = true;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CameraRotateCtrl crc = (CameraRotateCtrl)target;
            GUILayout.Space(5);
            showFoldout = EditorGUILayout.Foldout(showFoldout, "功能列表");
            if (showFoldout)
            {
                changeTarget = EditorGUILayout.Foldout(changeTarget, "改变观察目标");
                if (changeTarget)
                {
                    crc.newTarget = (Transform)EditorGUILayout.ObjectField("New Target", crc.newTarget, typeof(Transform), true);
                    if (GUILayout.Button("移动到新目标"))
                    {
                        crc.ChangeTarget(crc.newTarget);
                    }
                }

                if (GUILayout.Button("重置角度"))
                {
                    crc.ResetRotate();
                }

                setRotation = EditorGUILayout.Foldout(setRotation, "改变观察角度");
                if (setRotation)
                {
                    crc.newAimVector2 = EditorGUILayout.Vector3Field("目标点XY角度", crc.newAimVector2);
                    if (GUILayout.Button("移动到新目标"))
                    {
                        crc.ChangeToRotate(crc.newAimVector2.x, crc.newAimVector2.y, crc.newAimVector2.z, crc.newAimVector2.w);
                    }
                }
            }
        }
    }
#endif
}