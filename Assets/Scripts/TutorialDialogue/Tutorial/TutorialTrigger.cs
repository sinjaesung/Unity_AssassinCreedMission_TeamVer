using UnityEngine;

public class TutorialTrigger : TutorialBase
{
    [SerializeField]
    private Transform triggerObject;

    public bool isTrigger { set; get; } = false;

    public override void Enter()
    {
        gameObject.SetActive(true);

        Debug.Log("TutorialTrigger Enter>>");
        // Trigger 오브젝트 활성화
        triggerObject.gameObject.SetActive(true);
    }

    public override void Execute(TutorialController controller)
    {
        /*
		/// 거리 기준
		if ( (triggerObject.position - playerController.transform.position).sqrMagnitude < 0.1f )
		{
			controller.SetNextTutorial();
		}*/

        if (isTrigger == true)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Debug.Log("TutorialTrigger Exit>>");

        // Trigger 오브젝트 비활성화
        triggerObject.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isTrigger = true;

            Debug.Log(triggerObject.name + "오브젝트 CollidersEnter 반응 다음 튜토리얼");
            //collision.gameObject.SetActive(false);
        }
    }
}

