using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;


        //private Quaternion m_CharacterTargetRot;
        //private Quaternion m_CameraTargetRot;

        public void Init(Transform character, Transform camera)
        {
            //m_CharacterTargetRot = character.localRotation;
            //m_CameraTargetRot = camera.localRotation;
        }


        public void LookRotation(Transform character, Transform camera)
        {
            if (!Input.GetMouseButton(1))
                return;
            //float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
            //float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

            //m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            //m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

            //if (clampVerticalRotation)
            //    m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

            //if (smooth)
            //{
            //    character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot, smoothTime * Time.deltaTime);
            //    camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot, smoothTime * Time.deltaTime);
            //    Debug.Log("Ìø×ªÐý×ª----------¡·¡·¡·¡· X");
            //}
            //else
            //{
            //    character.localRotation = m_CharacterTargetRot;
            //    camera.localRotation = m_CameraTargetRot;
            //    Debug.Log("---------- ++++ Y");
            //}
            float yRot = Input.GetAxis("Mouse X") * XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensitivity;
            Vector3 yEuler = character.localEulerAngles + new Vector3(0, yRot, 0);
            Vector3 xEuler = camera.localEulerAngles - new Vector3(xRot, 0, 0);
            xEuler = ClampRotate(xEuler);
            character.localEulerAngles = yEuler;
            camera.localEulerAngles = xEuler;
        }

        Vector3 ClampRotate(Vector3 rotate)
        {
            if (rotate.x < 180)
            {
                rotate.x = Mathf.Clamp(rotate.x, -45, 45);
            }
            else
            {
                rotate.x = Mathf.Clamp(rotate.x, 315, 360);
            }
            return rotate;
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
