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
            //�̼�3
            if (missions.Mission1 == true && missions.Mission2 == true && missions.Mission3 == false && missions.Mission4 == false)
            {
                if (IsValidPass())
                {
                    Debug.Log(MatchTagString + ">�ش� Ÿ���� ������Ʈ ��� ���Žÿ��� �̼� ���");
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