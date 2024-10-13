using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Mission3 : MissionQuest
{

    private void Update()
    {
        var targetTagObjects = GameObject.FindGameObjectsWithTag(MatchTagString);
        MatchTagObjCnt = targetTagObjects.Length;
    }

    public override void MissionPassing()
    {
        //�̼�2(BŸ����� ���ֶ�)
        if (missions.Mission1 == true && missions.Mission2 == true && missions.Mission3 == false && missions.Mission4 == false)
        {
            if (IsValidPass())
            {
                Debug.Log(MatchTagString + ">�ش� Ÿ���� ������Ʈ ��� ���Žÿ��� �̼� ���");
                missions.Mission3 = true;
            }
        }
    }
}