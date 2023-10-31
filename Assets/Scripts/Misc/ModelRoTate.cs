using UnityEngine;
using System.Collections;
using App;

/// <summary>
/// ������鿴��ת����ű�
/// </summary>
public class ModelRoTate : MonoSingleton<ModelRoTate>
{
    [SerializeField]
    private enum RotateAxis
    {
        Free,
        YAxis,
        XAxis
    }
    [SerializeField]
    private enum MouceKey
    {
        Left,
        Right
    }
    public Camera camera;
    public GameObject model;

    [SerializeField] private RotateAxis _rorateAxis;

    [SerializeField] private MouceKey _mouceKey;

    public bool isCanRotate = true;
    public float rotateSpeed = 10;

    public bool isCanScrollView = true;

    void Update()
    {
        if (isCanRotate)
        {
            RotModel();
        }
        if (isCanScrollView)
        {
            ViewModel();
        }
    }

    //������ת
    void RotModel()
    {
        if (Input.GetMouseButton((int)_mouceKey))
        {
            switch (_rorateAxis)
            {
                case RotateAxis.Free:
                    model.transform.RotateAround(model.transform.position, camera.transform.up, -rotateSpeed * Input.GetAxis("Mouse X"));
                    model.transform.RotateAround(model.transform.position, camera.transform.right, rotateSpeed * Input.GetAxis("Mouse Y"));
                    break;
                case RotateAxis.YAxis:
                    model.transform.RotateAround(model.transform.position, camera.transform.up, -rotateSpeed * Input.GetAxis("Mouse X"));
                    break;
                case RotateAxis.XAxis:
                    model.transform.RotateAround(model.transform.position, camera.transform.right, rotateSpeed * Input.GetAxis("Mouse Y"));
                    break;
                default:
                    break;
            }
        }
    }
    //����������Զ
    public void ViewModel()
    {
        #region ͸�����
        //if (Input.GetAxis("Mouse ScrollWheel") < 0 && camera.fieldOfView < 100)
        //{
        //    camera.fieldOfView += 2;
        //}
        //else if (Input.GetAxis("Mouse ScrollWheel") > 0 && camera.fieldOfView > 45)
        //{
        //    camera.fieldOfView -= 2;
        //}
        #endregion
        #region �������
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && camera.orthographicSize < 0.8f)
        {
            camera.orthographicSize += 0.02f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0 && camera.orthographicSize > 0.4f)
        {
            camera.orthographicSize -= 0.02f;
        }
        #endregion
    }
}



