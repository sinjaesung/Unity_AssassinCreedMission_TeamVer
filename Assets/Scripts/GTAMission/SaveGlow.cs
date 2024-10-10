using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGlow : MonoBehaviour
{
    public Player player;
    public Missions missions;

    public string MatchTagString = "TARGET_A";
    public int MatchTagObjCnt = 0;
    //public GameObject SaveUIgameObject;

    public void SetData(Player player_)
    {
        player = player_;
    }

    private void Update()
    {
        var targetTagObjects = GameObject.FindGameObjectsWithTag(MatchTagString);
        MatchTagObjCnt = targetTagObjects.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        /* if(other.gameObject.tag == "Player")
         {
             player.SavePlayer();
             //UI
             StartCoroutine(SaveUI());
         }*/
        Debug.Log("SaveGlow OnTriggerEnter>>" + other.transform.name);
        //미션1(A타깃들을 없애라)
        if (other.CompareTag("Player"))
        {
            if (missions.Mission1 == false && missions.Mission2 == false && missions.Mission3 == false && missions.Mission4 == false)
            {
                if (IsValidPass())
                {
                    Debug.Log(MatchTagString + ">해당 타입의 오브젝트 모두 제거시에만 미션 통과");
                    missions.Mission1 = true;
                    player.playerMoney += 400;

                    Destroy(gameObject, 3f);
                }
            }
        }
    }

    private bool IsValidPass()
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
