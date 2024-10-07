using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Mission2 : MonoBehaviour
{
    public Player player;
    public Missions missions;

    private void OnTriggerEnter(Collider other)
    {
        //¹Ì¼Ç2
        if (missions.Mission1 == true && missions.Mission2 == false && missions.Mission3 == false && missions.Mission4 == false)
        {
            missions.Mission2 = true;
            player.playerMoney += 600;
        }
    }
}