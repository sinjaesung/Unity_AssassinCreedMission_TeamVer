using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialSample : MonoBehaviour
{
    [Header("Tutorial01 - Fade Effect")]
    [SerializeField]
    private FadeEffect fadeImage;// ���̵� ȿ�� ����� ���� �̹���

    [Header("Tutorial02 - Dialog")]
    [SerializeField]
    private TextMeshProUGUI textName; // ȭ�� �̸�
    [SerializeField]
    private TextMeshProUGUI textDialog;	// ���
    [SerializeField]
    [TextArea(0,1)]
    private string stringDialog = "���..��� ������!!";  // ���
    [SerializeField]
    private float typingSpeed = 0.5f;  // ��� Ÿ���� �ӵ�
    [SerializeField]
    private GameObject ojbectArrow;  // Ŀ�� �̹���
    [SerializeField]
    private float waitTime = 2.0f;	// ��� �ð�

    [Header("Tutorial03 - SFX")]
    [SerializeField]
    private AudioSource audioBoom;  // ������ ���

    [SerializeField]
    private string nextSceneName; // ���� �� �̸�

    private void Start()
    {
        // ȭ���� ���� ���������. ȭ���� ������ ������� OnDialog() �޼ҵ� ȣ��
        fadeImage.FadeIn(OnDialog);
    }

    private void OnDialog()
    {
        //ȭ�� �̸� ���� �� Ȱ��ȭ
        textName.text = "������A";
        textName.gameObject.SetActive(true);

        //��� Ȱ��ȭ �� Ÿ���� ȿ�� ���
        textDialog.gameObject.SetActive(true);
        StartCoroutine(nameof(TypingTextAndWaitTime));
    }

    private IEnumerator TypingTextAndWaitTime()
    {
        int index = 0;

        while(index < stringDialog.Length)
        {
            textDialog.text = stringDialog.Substring(0, index);

            index++;

            yield return new WaitForSeconds(typingSpeed);
        }

        //��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
        ojbectArrow.SetActive(true);

        //2�� ���
        yield return new WaitForSeconds(waitTime);

        //ȭ���� ���� ��Ӱ�. ������ ��ο����� PlaySoundAndChangeScene() �޼ҵ� ȣ��
        fadeImage.FadeOut(PlaySoundAndChangeScene);
    }

    private void PlaySoundAndChangeScene()
    {
        // ���� ���� ���
        StartCoroutine(nameof(OnPlaySoundAndChangeScene));
    }

    private IEnumerator OnPlaySoundAndChangeScene()
    {
        // ���� ���
        audioBoom.Play();

        while (true)
        {
            // ���� ����� �Ϸ�Ǹ�
            if (audioBoom.isPlaying == false)
            {
                // �� ��ȯ
                SceneManager.LoadScene(nextSceneName);
            }

            yield return null;
        }
    }
}