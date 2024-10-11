using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeEffect : MonoBehaviour
{

    //���̵� ȿ���� ������ �� ȣ���ϰ� ���� �޼ҵ带 ���, ȣ���ϴ� �̺�Ʈ Ŭ����
    [System.Serializable]
    private class FadeEvent : UnityEvent { }
    private FadeEvent onFadeEvent = new FadeEvent();

    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime; //���̵� �Ǵ� �ð�
    [SerializeField]
    private AnimationCurve fadeCurve;// ���̵� ȿ���� ����Ǵ� ���� ���� ��� ������ ����
    private Image fadeImage; //���̵� ȿ���� ���Ǵ� ���� ���� �̹���

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    public void FadeIn(UnityAction action)
    {
        StartCoroutine(Fade(action, 1, 0));
    }

    public void FadeOut(UnityAction action)
    {
        StartCoroutine(Fade(action, 0, 1));
    }

    private IEnumerator Fade(UnityAction action,float start,float end)
    {
        //action �޼ҵ带 �̺�Ʈ�� ���
        onFadeEvent.AddListener(action);

        float current = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            fadeImage.color = color;

            yield return null;
        }

        //action �޼ҵ带 ����
        onFadeEvent.Invoke();

        //action �޼ҵ带 �̺�Ʈ���� ����
        onFadeEvent.RemoveListener(action);
    }
}
