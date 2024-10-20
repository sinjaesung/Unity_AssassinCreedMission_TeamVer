using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class AdvancedPlayer : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("Components")]
    [SerializeField] private VideoPlayer videoPlayer;

    [SerializeField] private RectTransform rtWindow;


    [Header("Information")]
    [SerializeField] private Vector2 prevWindowSize = new Vector2(0, 0);
    [SerializeField] private Vector2 prevWindowSize2 = Vector2.zero;

    [SerializeField] private Image imgUIFrame;

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text textTime;

    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(SetSliderValue);
        //�ʱ�ȭ �۾��� ���ش�.
        prevWindowSize = rtWindow.sizeDelta;

        //����ð� Ȯ���ϱ� ���� �ʿ��� �͵��� �ִ�.
        //������ �ѽð�(max), ����ǰ��ִ� ���� �ð�(cur)
        slider.maxValue = (float)videoPlayer.length;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void WindowModeEvent()
    {
        Debug.Log("â���");

        rtWindow.anchorMin = new Vector2(0.5f, 0.5f);
        rtWindow.anchorMax = new Vector2(0.5f, 0.5f);

        //rtWindow.sizeDelta = new Vector2(1280f, 720f);
        rtWindow.sizeDelta = prevWindowSize;
    }

    public void FullModeEvent()
    {
        Debug.Log("��üȭ��");
        //=======================
        //1.anchor���� 0,0,1,1 ���·� �Ҵ�
        //2.offsetSize�� 0,0,0,0 ���� �ʱ�ȭ

        rtWindow.anchorMin = new Vector2(0, 0);
        rtWindow.anchorMax = new Vector2(1, 1);

        rtWindow.offsetMin = new Vector2(0, 0);
        rtWindow.offsetMax = new Vector2(0, 0);
    }

    public void SetPlayEvent()
    {
        Debug.Log("���");
        videoPlayer.Play();
    }

    public void SetPauseEvent()
    {
        Debug.Log("�Ͻ�����");
        videoPlayer.Pause();
    }

    public void SetStopEvent()
    {
        Debug.Log("����");
        videoPlayer.Stop();
        videoPlayer.playbackSpeed = 1.0f;
    }

    #region �������̽��� ����ߴ�.
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("ȣ��");
        imgUIFrame.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("��ȣ��");
        imgUIFrame.gameObject.SetActive(false);
    }
    #endregion

    private void SetSliderValue(float _value)
    {
        Debug.Log("SetSliderValue>:" + _value);
        videoPlayer.time = _value;
    }
    private string CalcTime(double _time)
    {
        string strTime = string.Empty;
        //���� ����ǰ� �ִ� ������ �ð����� �޾ƿ´�.
        float time = (float)_time;

        int hour = (int)time / 3600;
        int min = (int)(time % 3600) / 60;
        int sec = (int)(time % 60);

        if(hour > 0)
        {
            //������ ����ð��� 1�ð� �̻��̸� �ð��� ǥ��
            strTime = hour.ToString("D2") + ":" + min.ToString("D2") + ":" + sec.ToString("D2");
        }
        else
        {
            strTime = min.ToString("D2") + ":" + sec.ToString("D2");
        }

        return strTime;
    }

    void Update()
    {
        Debug.Log("now playtime:" + videoPlayer.time + "/" + videoPlayer.length);

        slider.value = (float)videoPlayer.time;

        //textTime.text = string.Format("{0} / {1}", CalcTime(videoPlayer.time), CalcTime(videoPlayer.length));
        textTime.text = CalcTime(videoPlayer.time) + " / " + CalcTime(videoPlayer.length);
    }
}
