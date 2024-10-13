using UnityEngine;

public class TutorialActive : TutorialBase
{
    [SerializeField]
    private GameObject ActiveTarget;

    public override void Enter()
    {
        gameObject.SetActive(true);

        ActiveTarget.SetActive(true);

        Debug.Log("TutorialActive Enter>>");
    }


    public override void Execute(TutorialController controller)
    {
        //if (isCompleted == true)
        //{
            controller.SetNextTutorial();
        //}
    }

    public override void Exit()
    {
        Debug.Log("TutorialActive Exit>>");
    }
}