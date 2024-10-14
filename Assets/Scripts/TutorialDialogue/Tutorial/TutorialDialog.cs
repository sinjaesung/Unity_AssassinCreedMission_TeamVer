using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(DialogSystem))]
public class TutorialDialog : TutorialBase
{
    //ĳ���͵��� ��縦 �����ϴ� DialogSystem
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