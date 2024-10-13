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
        //미션2(B타깃들을 없애라)
        if (missions.Mission1 == true && missions.Mission2 == false && missions.Mission3 == false && missions.Mission4 == false)
        {
            if (IsValidPass())
            {
                Debug.Log(MatchTagString + ">해당 타입의 오브젝트 모두 제거시에만 미션 통과");
                missions.Mission2 = true;
            }
        }
    }
}