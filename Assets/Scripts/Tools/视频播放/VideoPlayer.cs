using DG.Tweening;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VideoPlayer : MonoBehaviour
{
    [SerializeField] private MediaPlayer mediaPlayer;
    [SerializeField] private CanvasGroup controls;
    [SerializeField] private Slider timeLine;
    [SerializeField] private Button playAndPause;
    [SerializeField] private Button mute;
    [SerializeField] private Slider volume;
    [SerializeField] private Text timeDetail;

    /// <summary>
    /// ��¼��ǰ����
    /// </summary>
    private float recordVolum;
    /// <summary>
    /// ��ק������ʱ�Ƿ��ڲ���
    /// </summary>
    bool wasPlayingBeforeTimelineDrag;

    #region UnityEvent
    private void Awake()
    {
        mediaPlayer = transform.Find("MediaPlayer").GetComponent<MediaPlayer>();
        controls = transform.Find("Controls").GetComponent<CanvasGroup>();
        timeLine = controls.transform.Find("Timeline").GetComponent<Slider>();
        playAndPause = controls.transform.Find("BottomRow").Find("ButtonPlayPause").GetComponent<Button>();
        mute = controls.transform.Find("BottomRow").Find("ButtonVolume").GetComponent<Button>();
        volume = controls.transform.Find("BottomRow").Find("VolumeMask").Find("SliderVolume").GetComponent<Slider>();
        timeDetail = controls.transform.Find("BottomRow").Find("TextTimeDuration").GetComponent<Text>();

        playAndPause.onClick.AddListener(OnPlayOrPauseClick);
        mute.onClick.AddListener(OnMuteClick);
        volume.onValueChanged.AddListener(OnVolumChange);
        mediaPlayer.Events.AddListener(OnMediaPlayerEvent);

        BandingCanvasTriggerEvent();
        BandingTimeLineSliderTriggerEnevt();
        InitStyle();
    }

    void Update()
    {
        UpdaeTimeLine();
    }
    #endregion

    #region UIStyleChange
    /// <summary>
    /// ���İ�ť��ʽ--���Ż���ͣ
    /// </summary>
    /// <param name="b">trueΪ���� falseΪ��ͣ</param>
    private void ChangeStyle_PauseOrPlay(bool b)
    {
        playAndPause.GetComponent<Image>().material.SetFloat("_Morph", b ? 0 : 1);
    }

    /// <summary>
    /// ���İ�ť��ʽ--����
    /// </summary>
    /// <param name="b">trueΪ������ falseΪ����</param>
    private void ChangeStyle_Mute(bool b)
    {
        mute.GetComponent<Image>().material.SetFloat("_Mute", b ? 0 : 1);
    }

    private void InitStyle()
    {
        HideControls();
        volume.value = 1;
        if (mediaPlayer != null)
        {
            ChangeStyle_Mute(!mediaPlayer.AudioMuted);
            ChangeStyle_PauseOrPlay(mediaPlayer.AutoStart);
        }
    }
    #endregion

    #region UpdateFunc
    /// <summary>
    /// ʵʱ���� ����ʾ���ȵ�ʱ��� ���������ȡ� 
    /// </summary>
    private void UpdaeTimeLine()
    {
        if (mediaPlayer.Info != null)
        {
            if (timeLine.maxValue != (int)mediaPlayer.Info.GetDuration())
            {
                timeLine.maxValue = (int)mediaPlayer.Info.GetDuration();
            }
            if (mediaPlayer.Control.IsPlaying())
                timeLine.value = (float)mediaPlayer.Control.GetCurrentTime();
            timeDetail.text = $"{TimeFormat((int)mediaPlayer.Control.GetCurrentTime())}/{TimeFormat((int)mediaPlayer.Info.GetDuration())}";
        }
    }
    #endregion

    #region UIEventBanding
    /// <summary>
    /// ������ͣ
    /// </summary>
    private void OnPlayOrPauseClick()
    {
        ChangeStyle_PauseOrPlay(mediaPlayer.Control.IsPaused());
        if (mediaPlayer.Control.IsPaused())
            mediaPlayer.Control.Play();
        else
            mediaPlayer.Control.Pause();
    }
    /// <summary>
    /// ����
    /// </summary>
    private void OnMuteClick()
    {
        if (!mediaPlayer.AudioMuted)
        {
            recordVolum = volume.value;
            volume.interactable = false;
            volume.value = 0;
        }
        else
        {
            volume.interactable = true;
            volume.value = recordVolum;
        }
        ChangeStyle_Mute(mediaPlayer.AudioMuted);
        mediaPlayer.AudioMuted = !mediaPlayer.AudioMuted;
    }
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="value"></param>
    private void OnVolumChange(float value)
    {
        mediaPlayer.Control.SetVolume(value);
    }

    /// <summary>
    /// �� ���ƽ��������ʾ���¼�
    /// </summary>
    private void BandingCanvasTriggerEvent()
    {
        EventTrigger eventTrigger = transform.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry;
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) =>
        {
            ShowControls();
        });
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) =>
        {
            HideControls();
        });
        eventTrigger.triggers.Add(entry);
    }
    public void ShowControls()
    {
        DOTween.To(() => controls.alpha, x => controls.alpha = x, 1, 0.75f).OnComplete(() => { controls.interactable = true; });
    }
    public void HideControls()
    {
        DOTween.To(() => controls.alpha, x => controls.alpha = x, 0, 0.75f).OnComplete(() => { controls.interactable = false; });
    }
    /// <summary>
    /// �� ������������Ƶ���ȵ��¼�
    /// </summary>
    private void BandingTimeLineSliderTriggerEnevt()
    {
        EventTrigger eventTrigger = timeLine.transform.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnTimeSliderBeginDrag(); });
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnTimeSliderDrag(); });
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => { OnTimeSliderEndDrag(); });
        eventTrigger.triggers.Add(entry);
    }

    private void OnTimeSliderBeginDrag()
    {
        if (mediaPlayer && mediaPlayer.Control != null)
        {
            wasPlayingBeforeTimelineDrag = mediaPlayer.Control.IsPlaying();
            if (wasPlayingBeforeTimelineDrag)
            {
                mediaPlayer.Pause();
            }
            OnTimeSliderDrag();
        }
    }

    private void OnTimeSliderDrag()
    {
        if (mediaPlayer && mediaPlayer.Control != null)
        {
            var time = timeLine.value;
            mediaPlayer.Control.Seek(time);
        }
    }

    private void OnTimeSliderEndDrag()
    {
        if (mediaPlayer && mediaPlayer.Control != null)
        {
            if (wasPlayingBeforeTimelineDrag)
            {
                mediaPlayer.Play();
                wasPlayingBeforeTimelineDrag = false;
            }
        }
    }
    /// <summary>
    /// MediaPlayer ��Ƶ���Żص��¼�
    /// </summary>
    /// <param name="mp"></param>
    /// <param name="et"></param>
    /// <param name="errorCode"></param>
    public void OnMediaPlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
        switch (et)
        {
            //case MediaPlayerEvent.EventType.Started:
            //    OnMediaPlayerStarted(mp);
            //    break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                OnMediaPlayerFinished(mp);
                break;
        }
    }
    //private void OnMediaPlayerStarted(MediaPlayer mp)
    //{

    //}
    private void OnMediaPlayerFinished(MediaPlayer mp)
    {
        ChangeStyle_PauseOrPlay(false);
    }

    #endregion

    #region ToolFunc
    /// <summary>
    /// ��תʱ���ʽ
    /// </summary>
    /// <param name="TotleTime"></param>
    /// <returns></returns>
    private string TimeFormat(int TotleTime)
    {
        int hour;
        int minute;
        int second;
        hour = TotleTime / 3600;
        if (hour == 0)
        {
            minute = TotleTime / 60;
            second = TotleTime % 60;
            return $"{minute.ToString("D2")}:{second.ToString("D2")}";
        }
        else
        {
            minute = (TotleTime - 3600) / 60;
            second = TotleTime % 60;
            return $"{hour.ToString("D2")}:{minute.ToString("D2")}:{second.ToString("D2")}";
        }
    }
    #endregion

    #region PublicFunc

    #endregion
}
