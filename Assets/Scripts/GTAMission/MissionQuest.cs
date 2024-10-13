using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionQuest : MonoBehaviour
{
    public Missions missions;

    public string MatchTagString = "TARGET_A";
    public int MatchTagObjCnt = 0;
    //public GameObject SaveUIgameObject;


    private void Update()
    {
        var targetTagObjects = GameObject.FindGameObjectsWithTag(MatchTagString);
        MatchTagObjCnt = targetTagObjects.Length;
    }

    public virtual void MissionPassing()
    {
        //�̼�1(AŸ����� ���ֶ�)
        if (missions.Mission1 == false && missions.Mission2 == false && missions.Mission3 == false && missions.Mission4 == false)
        {
            if (IsValidPass())
            {
                Debug.Log(MatchTagString + ">�ش� Ÿ���� ������Ʈ ��� ���Žÿ��� �̼� ���");
                missions.Mission1 = true;
            }
        }    
    }

    protected bool IsValidPass()
    {
        var targetTagObjects = GameObject.FindGameObjectsWithTag(MatchTagString);
        MatchTagObjCnt = targetTagObjects.Length;

        if(MatchTagObjCnt <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

  /*  IEnumerator SaveUI()
    {
        SaveUIgameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SaveUIgameObject.SetActive(false);
    }*/
}
