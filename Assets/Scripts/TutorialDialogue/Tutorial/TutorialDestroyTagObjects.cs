using UnityEngine;

public class TutorialDestroyTagObjects : TutorialBase
{
    /*[SerializeField]
    private GameObject[] objectList;*/
    [SerializeField]
    private string tagName;

    public override void Enter()
    {
        gameObject.SetActive(true);

        Debug.Log("TutorialDestroyTagObjects Enter>>");
      
        //�����ؾ��� ������Ʈ���� Ȱ��ȭ
       /* for(int i=0; i<objectList.Length; ++i)
        {
            objectList[i].SetActive(true);
        }*/
    }

    
    public override void Execute(TutorialController controller)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);
        
        if (objects.Length == 0)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Debug.Log("TutorialDestroyTagObjects Exit>>");
    }
}