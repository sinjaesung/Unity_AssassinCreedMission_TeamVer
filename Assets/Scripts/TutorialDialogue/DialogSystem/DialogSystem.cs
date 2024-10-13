using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum Speaker {  Player = 0, Npc}

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private Dialog[] dialogs; //���� �б��� ��� ���
    [SerializeField]
    private Image[] SpeakerImages;//��ȭâ ĳ���� �̹���
    [SerializeField]
    private Sprite[] SpeakerSprites;
    [SerializeField]
    private Image[] imageDialogs; //��ȭâ Image UI
    [SerializeField]
    private TextMeshProUGUI[] textNames; //���� ������� ĳ���� �̸� ��� Text UI
    [SerializeField]
    private TextMeshProUGUI[] textDialogues; // ���� ��� ��� Text UI
    [SerializeField]
    private GameObject[] objectArrows; // ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� ������Ʈ
    [SerializeField]
    private float typingSpeed; // �ؽ�Ʈ Ÿ���� ȿ���� ��� �ӵ�
    [SerializeField]
    private KeyCode keyCodeSkip = KeyCode.Space; // Ÿ���� ȿ���� ��ŵ�ϴ� Ű

    private int currentIndex = -1;
    private bool isTypingEffect = false; // �ؽ�Ʈ Ÿ���� ȿ���� ���������
    private Speaker currentSpeaker = Speaker.Player;

    [SerializeField]
    public int SpearkCnt = 2;
    public void Setup()
    {
        for (int i = 0; i < SpearkCnt; ++i)
        {
            //��� ��ȭ ���� ���ӿ�����Ʈ ��Ȱ��ȭ
            InActiveObjects(i);
        }

        SetNextDialog();
    }

    public bool UpdateDialog()
    {
        if (Input.GetKeyDown(keyCodeSkip) || Input.GetMouseButtonDown(0))
        {
            //�ؽ�Ʈ Ÿ���� ȿ���� ������϶� ���콺 ���� Ŭ���ϸ� Ÿ���� ȿ�� ����
            if (isTypingEffect == true)
            {
                //Ÿ���� ȿ���� �����ϰ�, ���� ��� ��ü�� ����Ѵ�
                StopCoroutine("TypingText");
                isTypingEffect = false;
                textDialogues[(int)currentSpeaker].text = dialogs[currentIndex].dialogue;
                //��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
                objectArrows[(int)currentSpeaker].SetActive(true);

                return false;
            }

            //���� ��� ����
            if (dialogs.Length > currentIndex + 1)
            {
                SetNextDialog();
            }
            //��簡 �� �̻� ���� ��� true ��ȯ
            else
            {
                //��� ĳ���� �̹����� ��Ӱ� ����
                for (int i = 0; i < SpearkCnt; ++i)
                {
                    //��� ��ȭ ���� ���ӿ�����Ʈ ��Ȱ��ȭ
                    InActiveObjects(i);
                }

                return true;
            }
        }

        return false;
    }

    private void SetNextDialog()
    {
        //���� ȭ���� ��ȭ ���� ������Ʈ ��Ȱ��ȭ
        InActiveObjects((int)currentSpeaker);

        currentIndex++;

        //���� ȭ�� ����
        currentSpeaker = dialogs[currentIndex].speaker;

        //��ȭâ Ȱ��ȭ
        imageDialogs[(int)currentSpeaker].gameObject.SetActive(true);

        //���� ȭ�� �̸� �ؽ�Ʈ Ȱ��ȭ �� ����
        textNames[(int)currentSpeaker].gameObject.SetActive(true);
        textNames[(int)currentSpeaker].text = dialogs[currentIndex].speaker.ToString();

        //ȭ���� ��� �ؽ�Ʈ Ȱ��ȭ �� ���� (Typing Effect)
        textDialogues[(int)currentSpeaker].gameObject.SetActive(true);

        if (SpeakerImages[(int)currentSpeaker] != null)
        {
            SpeakerImages[(int)currentSpeaker].gameObject.SetActive(true);
            SpeakerImages[(int)currentSpeaker].sprite = SpeakerSprites[(int)currentSpeaker];
        }

        StartCoroutine(nameof(TypingText));
    }

    private void InActiveObjects(int index)
    {
        imageDialogs[index].gameObject.SetActive(false);
        textNames[index].gameObject.SetActive(false);
        textDialogues[index].gameObject.SetActive(false);
        objectArrows[index].SetActive(false);
        if (SpeakerImages[index] != null)
        {
            SpeakerImages[index].gameObject.SetActive(false);
        }
    }

    private IEnumerator TypingText()
    {
        int index = 0;

        isTypingEffect = true;

        // �ؽ�Ʈ�� �ѱ��ھ� Ÿ����ġ�� ���
        while (index < dialogs[currentIndex].dialogue.Length)
        {
            textDialogues[(int)currentSpeaker].text = dialogs[currentIndex].dialogue.Substring(0, index);

            index++;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;

        //��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
        objectArrows[(int)currentSpeaker].SetActive(true);
    }
}

[System.Serializable]
public struct Dialog
{
    public Speaker speaker; // ȭ��
    [TextArea(3, 5)]
    public string dialogue;	// ���
}
