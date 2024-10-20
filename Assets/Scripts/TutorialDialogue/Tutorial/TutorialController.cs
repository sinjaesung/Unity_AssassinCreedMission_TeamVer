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
        //현재 튜토리얼의 Exit() 메소드 호출
        if(currentTutorial != null)
        {
            currentTutorial.Exit();
        }

        //마지막 튜토리얼을 진행했다면 CompletedAllTutorials() 메소드 호출
        if(currentIndex >= tutorials.Count - 1)
        {
            CompletedAllTutorials();
            return;
        }

        //다음 튜토리얼 과정을 currentTutorial로 등록
        currentIndex++;
        currentTutorial = tutorials[currentIndex];

        //새로 바뀐 튜토리얼의 Enter() 메소드 호출
        currentTutorial.Enter();
    }

    public void CompletedAllTutorials()
    {
        currentTutorial = null;

        //행동 양식이 여러 종류가 되었을 때 코드 추가 작성
        //현재는 씬 전환

        Debug.Log("Complete All >> Ending씬 이동>>>");

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