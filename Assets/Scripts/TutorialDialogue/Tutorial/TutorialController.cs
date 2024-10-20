using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private List<TutorialBase> tutorials;
    [SerializeField]
    private string nextSceneName = "";

    public TutorialBase currentTutorial = null;
    private int currentIndex = -1;

    public bool SelectedSceneMove = false;
    public string[] SelectedScenes;
    public int SelectedSceneIndex;
    private void Start()
    {
        SetNextTutorial();
    }

    private void Update()
    {
        if(currentTutorial != null)
        {
            currentTutorial.Execute(this);
        }
    }

    public void SetNextTutorial()
    {
        Debug.Log("SetNextTutorial >> " + currentIndex);
        //���� Ʃ�丮���� Exit() �޼ҵ� ȣ��
        if(currentTutorial != null)
        {
            currentTutorial.Exit();
        }

        //������ Ʃ�丮���� �����ߴٸ� CompletedAllTutorials() �޼ҵ� ȣ��
        if(currentIndex >= tutorials.Count - 1)
        {
            CompletedAllTutorials();
            return;
        }

        //���� Ʃ�丮�� ������ currentTutorial�� ���
        currentIndex++;
        currentTutorial = tutorials[currentIndex];

        //���� �ٲ� Ʃ�丮���� Enter() �޼ҵ� ȣ��
        currentTutorial.Enter();
    }

    public void CompletedAllTutorials()
    {
        currentTutorial = null;

        //�ൿ ����� ���� ������ �Ǿ��� �� �ڵ� �߰� �ۼ�
        //����� �� ��ȯ

        Debug.Log("Complete All >> Ending�� �̵�>>>");

        if (SelectedSceneMove == false)
        {
            if (!nextSceneName.Equals(""))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
        else
        {
            SceneManager.LoadScene(SelectedScenes[SelectedSceneIndex]);
        }
    }
}