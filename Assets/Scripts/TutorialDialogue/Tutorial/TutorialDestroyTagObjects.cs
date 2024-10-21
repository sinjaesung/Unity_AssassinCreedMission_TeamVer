using UnityEngine;

public class TutorialDestroyTagObjects : TutorialBase
{
    /*[SerializeField]
    private GameObject[] objectList;*/
    [SerializeField] public MissionQuest targetMissionQuest;
    [SerializeField]
    private string tagName;

    public AudioSource TargetBgmAudioSource;
    public override void Enter()
    {
        gameObject.SetActive(true);

        Debug.Log("TutorialDestroyTagObjects Enter>>");

        if (targetMissionQuest != null)
        {
            targetMissionQuest.gameObject.SetActive(true);
        }
    }

    
    public override void Execute(TutorialController controller)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);
        
        if (objects.Length == 0)
        {
            if (targetMissionQuest != null)
            {
                targetMissionQuest.MissionPassing();//미션1,2,3 등 통과
            }
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Debug.Log("TutorialDestroyTagObjects Exit>>");

        if (TargetBgmAudioSource != null)
        {
            TargetBgmAudioSource.Stop();
        }
    }
}