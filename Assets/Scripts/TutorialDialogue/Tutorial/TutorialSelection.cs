using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TutorialSelection : TutorialBase
{
    public int selectedIndex;
    public bool isCompleted = false;

    public override void Enter()
    {
        Debug.Log("TutorialSelection Enter>>");
        gameObject.SetActive(true);
    }
    public void OnSelectedAction(int index)
    {
        Debug.Log("TutorialSelection select index>" + index);
        selectedIndex = index;
    }
    public void OnConfirmAction()
    {
        isCompleted = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public override void Execute(TutorialController controller)
    {
        if (isCompleted == true)
        {
            controller.SelectedSceneIndex = selectedIndex;
            controller.SetNextTutorial();
        }
    }
    public override void Exit()
    {
        Debug.Log("TutorialSelection Exit>>");
    }
}
