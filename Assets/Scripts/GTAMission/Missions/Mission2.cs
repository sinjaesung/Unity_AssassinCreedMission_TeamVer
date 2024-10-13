using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Mission2 : MissionQuest
{

    private void Update()
    {
        var targetTagObjects = GameObject.FindGameObjectsWithTag(MatchTagString);
        MatchTagObjCnt = targetTagObjects.Length;
    }

    public override void MissionPassing()
    {
        //�̼�2(BŸ����� ���ֶ�)
        if (missions.Mission1 == true && missions.Mission2 == false && missions.Mission3 == false && missions.Mission4 == false)
        {
            if (IsValidPass())
            {
                Debug.Log(MatchTagString + ">�ش� Ÿ���� ������Ʈ ��� ���Žÿ��� �̼� ���");
                missions.Mission2 = true;
            }
        }
    }
}