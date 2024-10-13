using System.Collections.Generic;
using UnityEngine;

public class TutorialCondStatus : TutorialBase
{
    public override void Enter()
    {
        gameObject.SetActive(true);

        Debug.Log("TutorialCondStatus Enter>>");

        //제거해야할 오브젝트들을 활성화
        /* for(int i=0; i<objectList.Length; ++i)
         {
             objectList[i].SetActive(true);
         }*/
    }


    public override void Execute(TutorialController controller)
    {
        if (IsEnd)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Debug.Log("TutorialDestroyTagObjects Exit>>");
    }
}