using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(DialogSystem))]
public class TutorialDialog : TutorialBase
{
    //캐릭터들의 대사를 진행하는 DialogSystem
    private DialogSystem dialogSystem;

    public AISpawner targetAispawner;
    public int spawnCnt;

    public PoliceSpawner[] targetPolicespawners;
    public int[] spawnCnt_polices;

    public BossSpawner bossSpawner;

    public float delayTime;

    public override void Enter()
    {
        gameObject.SetActive(true);

        Debug.Log("TutorialDialog Enter>>");
        dialogSystem = GetComponent<DialogSystem>();
        dialogSystem.Setup();

        if (targetAispawner != null)
        {
            StartCoroutine(SpawnCharacter());
        }

        if (targetPolicespawners != null)
        {
            for(int e=0; e< targetPolicespawners.Length; e++)
            {
                var item = targetPolicespawners[e];
                var cnt = spawnCnt_polices[e];
                StartCoroutine(SpawnPolices(item,cnt));
            }
        }

        if (bossSpawner != null)
        {
            StartCoroutine(SpawnBoss());
        }
    }
    private IEnumerator SpawnCharacter()
    {
        yield return new WaitForSeconds(delayTime);

        if (targetAispawner != null)
        {
            targetAispawner.SpawnEnemies(spawnCnt);
        }
    }
    private IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(delayTime);

        if (bossSpawner != null)
        {
            bossSpawner.SpawnTarget();
        }
    }
    private IEnumerator SpawnPolices(PoliceSpawner target,int spawnCnt)
    {
        yield return new WaitForSeconds(delayTime);

        if (target != null)
        {
            target.SpawnEnemies(spawnCnt);
        }
    }
    public override void Execute(TutorialController controller)
    {
        //현재 분기에 진행되는 대사 진행
        bool isCompleted = dialogSystem.UpdateDialog();

        //현재 분기의 대사 진행이 완료되면
        if(isCompleted == true)
        {
            //다음 튜토리얼로 이동
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Debug.Log("TutorialDialog Exit>>");
    }
}