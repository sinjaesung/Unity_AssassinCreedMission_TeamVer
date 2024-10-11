using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatActionUI : MonoBehaviour
{
    public GameObject FistAttackAction;
    public GameObject SwordAttackAction;
   /* public GameObject RifleAttackAction;
    public GameObject BazookaAttackAction;
    public GameObject IceStrikeAttackAction;
    public GameObject FireStrikeAttackAction;
    public GameObject SnipermodeAction;
    public GameObject GunReloadAction;*/

    public void AllCombatClear()
    {
        for(int e=0; e<transform.childCount; e++)
        {
            var child_transform = transform.GetChild(e);
            if (!child_transform.CompareTag("GunReload"))
            {
                child_transform.gameObject.SetActive(false);
            }
        }
    }
}
