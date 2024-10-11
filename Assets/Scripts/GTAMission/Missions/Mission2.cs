using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Mission2 : MonoBehaviour
{
    public Missions missions;

    public string MatchTagString = "TARGET_B";
    public int MatchTagObjCnt = 0;

    private void Update()
    {
        var targetTagObjects = GameObject.FindGameObjectsWithTag(MatchTagString);
        MatchTagObjCnt = targetTagObjects.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //미션2
            if (missions.Mission1 == true && missions.Mission2 == false && missions.Mission3 == false && missions.Mission4 == false)
            {
                if (IsValidPass())
                {
                    Debug.Log(MatchTagString + ">해당 타입의 오브젝트 모두 제거시에만 미션 통과");
                    missions.Mission2 = true;

                    //Destroy(gameObject, 3f);
                }
            }
        }
    }
    private bool IsValidPass()
    {
        var targetTagObjects = GameObject.FindGameObjectsWithTag(MatchTagString);
        MatchTagObjCnt = targetTagObjects.Length;

        if (MatchTagObjCnt <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}