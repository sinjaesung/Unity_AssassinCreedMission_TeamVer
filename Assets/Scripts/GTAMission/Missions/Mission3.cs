using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Mission3 : MonoBehaviour
{
    public Missions missions;

    public string MatchTagString = "TARGET_C";
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
            //미션3
            if (missions.Mission1 == true && missions.Mission2 == true && missions.Mission3 == false && missions.Mission4 == false)
            {
                if (IsValidPass())
                {
                    Debug.Log(MatchTagString + ">해당 타입의 오브젝트 모두 제거시에만 미션 통과");
                    missions.Mission3 = true;

                   // Destroy(gameObject, 3f);
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