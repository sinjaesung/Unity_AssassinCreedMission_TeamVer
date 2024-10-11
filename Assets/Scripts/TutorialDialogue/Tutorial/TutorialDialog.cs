using UnityEngine;

[RequireComponent(typeof(DialogSystem))]
public class TutorialDialog : TutorialBase
{
    //ĳ���͵��� ��縦 �����ϴ� DialogSystem
    private DialogSystem dialogSystem;

    public override void Enter()
    {
        gameObject.SetActive(true);

        Debug.Log("TutorialDialog Enter>>");
        dialogSystem = GetComponent<DialogSystem>();
        dialogSystem.Setup();
    }

    public override void Execute(TutorialController controller)
    {
        //���� �б⿡ ����Ǵ� ��� ����
        bool isCompleted = dialogSystem.UpdateDialog();

        //���� �б��� ��� ������ �Ϸ�Ǹ�
        if(isCompleted == true)
        {
            //���� Ʃ�丮��� �̵�
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Debug.Log("TutorialDialog Exit>>");
    }
}