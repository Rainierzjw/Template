/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UIAuxiliary
* 创建日期：2023-10-31 09:47:37
* 作者名称：张旌伟
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using IngameDebugConsole;

namespace Actor
{
    /// <summary>
    /// 
    /// </summary>
	public class UIAuxiliary : MonoBehaviour 
	{
        public KeyCode keyCode1 = KeyCode.LeftAlt;
        public KeyCode keyCode2 = KeyCode.T;
        public GameObject bg;
        public Text fps;
        public InputField timescale;
        public Dropdown quility;
        public Toggle debug;
        public DebugLogPopup popup;

        [Tooltip("Toggles whether the FPS text is visible.")]
        public bool displayFPS = true;
        [Tooltip("The frames per second deemed acceptable that is used as the benchmark to change the FPS text colour.")]
        public int targetFPS = 60;
        [Tooltip("The size of the font the FPS is displayed in.")]
        public int fontSize = 30;
        [Tooltip("The colour of the FPS text when the frames per second are within reasonable limits of the Target FPS.")]
        public Color goodColor = Color.green;
        [Tooltip("The colour of the FPS text when the frames per second are falling short of reasonable limits of the Target FPS.")]
        public Color warnColor = Color.yellow;
        [Tooltip("The colour of the FPS text when the frames per second are at an unreasonable level of the Target FPS.")]
        public Color badColor = Color.red;

        protected const float updateInterval = 0.5f;
        protected int framesCount;
        protected float framesTime;

        private void Start ()
        {
            timescale.onValueChanged.AddListener(ChangeTimeScale);
            quility.onValueChanged.AddListener(ChangeQuility);
            debug.onValueChanged.AddListener(Console);
        }

        private void Update()
        {
            if ((Input.GetKey(keyCode1) && Input.GetKeyDown(keyCode2)) || (Input.GetKeyDown(keyCode1) && Input.GetKey(keyCode2)))
            {
                bg.SetActive(!bg.activeSelf);
            }

            framesCount++;
            framesTime += Time.unscaledDeltaTime;
            if (framesTime > updateInterval)
            {
                if (fps != null)
                {
                    if (displayFPS)
                    {
                        float value = framesCount / framesTime;
                        fps.text = string.Format("{0:F2}", value);
                        fps.color = (value > (targetFPS - 5) ? goodColor :
                                     (value > (targetFPS - 30) ? warnColor :
                                      badColor));
                    }
                    else
                    {
                        fps.text = "";
                    }
                }
                framesCount = 0;
                framesTime = 0;
            }
        }       

        private void ChangeTimeScale(string timeScale)
        {
            Time.timeScale = int.Parse(timeScale);
        }

        private void ChangeQuility(int value)
        {
            QualitySettings.SetQualityLevel(value + 2, true);
        }

        private void Console(bool value)
        {
            if(value)
            {
                DebugLogManager.Instance.HideLogWindow();
            }
            else
            {
                DebugLogManager.Instance.HideLogWindow();
                popup.Hide();
            }
        }
	}
}

