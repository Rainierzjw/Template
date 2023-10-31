/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称： UI_3_VideoPlayer
* 创建日期：2023-07-31 19:08:03
* 作者名称：张旌伟
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace UI
{
    /// <summary>
    /// 
    /// </summary>
	public class UIVideoPlayer : BaseUI
	{
        public Transform control;
        public Slider videoSeekSld;
        public Button playBtn;
        public Button pauseBtn;
        public MediaPlayer mediaPlayer;
        public RectTransform bufferedSliderRect;
        private float videoSeekValue;
        private Image bufferedSliderImage;
        private bool wasPlayingOnScrub;
        public string folder = "Video/";
        private string path;
        public bool isControl = false;

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (bufferedSliderRect != null)
            {
                bufferedSliderImage = bufferedSliderRect.GetComponent<Image>();
            }
            playBtn.onClick.AddListener(OnPlayButton);
            pauseBtn.onClick.AddListener(OnPauseButton);
            videoSeekSld.onValueChanged.AddListener(OnVideoSeekSlider);
        }

        protected override void OnShow()
        {
            base.OnShow();
            mediaPlayer.m_VideoPath = System.IO.Path.Combine(folder, path);
            if (string.IsNullOrEmpty(mediaPlayer.m_VideoPath))
            {
                mediaPlayer.CloseVideo();
            }
            else
            {
                mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, mediaPlayer.m_VideoPath);
            }
            ControlHide();
        }

        protected override void OnHide()
        {
            base.OnHide();
            mediaPlayer.CloseVideo();
        }

        private void Update()
        {
            if (mediaPlayer && mediaPlayer.Info != null && mediaPlayer.Info.GetDurationMs() > 0f)
            {
                float time = mediaPlayer.Control.GetCurrentTimeMs();
                float duration = mediaPlayer.Info.GetDurationMs();
                float d = Mathf.Clamp(time / duration, 0.0f, 1.0f);

                // Debug.Log(string.Format("time: {0}, duration: {1}, d: {2}", time, duration, d));

                videoSeekValue = d;
                videoSeekSld.value = d;

                if (bufferedSliderRect != null)
                {
                    float t1 = 0f;
                    float t2 = mediaPlayer.Control.GetBufferingProgress();
                    if (t2 <= 0f)
                    {
                        if (mediaPlayer.Control.GetBufferedTimeRangeCount() > 0)
                        {
                            mediaPlayer.Control.GetBufferedTimeRange(0, ref t1, ref t2);
                            t1 /= mediaPlayer.Info.GetDurationMs();
                            t2 /= mediaPlayer.Info.GetDurationMs();
                        }
                    }

                    Vector2 anchorMin = Vector2.zero;
                    Vector2 anchorMax = Vector2.one;

                    if (bufferedSliderImage != null &&
                        bufferedSliderImage.type == Image.Type.Filled)
                    {
                        bufferedSliderImage.fillAmount = d;
                    }
                    else
                    {
                        anchorMin[0] = t1;
                        anchorMax[0] = t2;
                    }

                    bufferedSliderRect.anchorMin = anchorMin;
                    bufferedSliderRect.anchorMax = anchorMax;
                }
            }
            if (mediaPlayer.Control.IsFinished())
                ControlShow();
        }

        public void Show(string name)
        {
            path = name;
            OnShow();
        }

        public void OnPlayButton()
        {
            if (mediaPlayer)
            {
                mediaPlayer.Control.Play();
                playBtn.gameObject.SetActive(false);
                pauseBtn.gameObject.SetActive(true);
            }
        }

        public void OnPauseButton()
        {
            if (mediaPlayer)
            {
                mediaPlayer.Control.Pause();
                playBtn.gameObject.SetActive(true);
                pauseBtn.gameObject.SetActive(false);
            }
        }

        public void OnVideoSeekSlider(float value)
        {
            if (mediaPlayer && videoSeekSld && value != videoSeekValue)
            {
                mediaPlayer.Control.Seek(value * mediaPlayer.Info.GetDurationMs());
            }
        }

        public void OnVideoSliderDown()
        {
            isControl = true;
            if (mediaPlayer)
            {
                wasPlayingOnScrub = mediaPlayer.Control.IsPlaying();
                if (wasPlayingOnScrub)
                {
                    mediaPlayer.Control.Pause();
                }
            }
        }

        public void OnVideoSliderUp()
        {
            isControl = false;
            if (mediaPlayer && wasPlayingOnScrub)
            {
                mediaPlayer.Control.Play();
                wasPlayingOnScrub = false;
            }
        }

        public void ControlShow()
        {
            control.DOLocalMoveY(0f, 0.5f);
        }

        public void ControlHide()
        {
            if (!isControl)
                control.DOLocalMoveY(-90f, 0.5f);
        }
    }
}

