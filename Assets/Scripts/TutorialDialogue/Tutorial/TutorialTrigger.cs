using UnityEngine;

public class TutorialTrigger : TutorialBase
{

   // public bool isTrigger { set; get; } = false;

    public TutorialTargeting_boolParam targetCollideObj;

    public bool isDynamicTag_Picking = false;
    public string MatchingTagTargetString;
    public override void Enter()
    {
        gameObject.SetActive(true);

        Debug.Log("TutorialTrigger Enter>>"+ MatchingTagTargetString);

        if (isDynamicTag_Picking)
        {
            var gameObjs= GameObject.FindGameObjectsWithTag(MatchingTagTargetString);
            for(int g=0; g<gameObjs.Length; g++)
            {
                Debug.Log("TutorialTrigger "+g + "|" + MatchingTagTargetString);
                var item = gameObjs[g].GetComponent<TutorialTargeting_boolParam>();
                item.SetTargetTut(this);//그들중 임의의 하나와 캐릭터가 부딪혔을때 관련 다음 튜토리얼 실행>>
            }
        }
        else
        {
            targetCollideObj.SetTargetTut(this);
        }   
    }
    void Update()
    {
        if (isDynamicTag_Picking)
        {
            var gameObjs = GameObject.FindGameObjectsWithTag(MatchingTagTargetString);
            for (int g = 0; g < gameObjs.Length; g++)
            {
                Debug.Log("TutorialTrigger " + g + "|" + MatchingTagTargetString);
                var item = gameObjs[g].GetComponent<TutorialTargeting_boolParam>();
                item.SetTargetTut(this);//그들중 임의의 하나와 캐릭터가 부딪혔을때 관련 다음 튜토리얼 실행>>
            }
        }
        else
        {
            targetCollideObj.SetTargetTut(this);
        }
    }
    public override void Execute(TutorialController controller)
    {
        /*
		/// 거리 기준
		if ( (triggerObject.position - playerController.transform.position).sqrMagnitude < 0.1f )
		{
			controller.SetNextTutorial();
		}*/

        if (IsEnd == true)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Debug.Log("TutorialTrigger Exit>>");
    }
}

