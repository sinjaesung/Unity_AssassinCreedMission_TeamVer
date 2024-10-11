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
        // Trigger ������Ʈ Ȱ��ȭ
        triggerObject.gameObject.SetActive(true);
    }

    public override void Execute(TutorialController controller)
    {
        /*
		/// �Ÿ� ����
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

        // Trigger ������Ʈ ��Ȱ��ȭ
        triggerObject.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isTrigger = true;

            Debug.Log(triggerObject.name + "������Ʈ CollidersEnter ���� ���� Ʃ�丮��");
            //collision.gameObject.SetActive(false);
        }
    }
}

