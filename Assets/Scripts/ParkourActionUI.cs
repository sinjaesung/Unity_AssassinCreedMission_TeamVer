using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourActionUI : MonoBehaviour
{
    public Transform BasicAction;
    public Transform SubAction;
    public GameObject ParkourAction;
    public GameObject ClimbIdleAction;
    public GameObject ClimbJumpAction;
    public GameObject ClimbMoveAction;
    public GameObject JumpDownAction;
    public GameObject JumpwallAction;
    public GameObject DrophangAction;

    public void BasicActionClear()
    {
        for(int e=0; e < BasicAction.childCount; e++)
        {
            var child_obj = BasicAction.GetChild(e);
            child_obj.gameObject.SetActive(false);
        }
    }
    public void SubActionClear()
    {
        for (int e = 0; e < SubAction.childCount; e++)
        {
            var child_obj = SubAction.GetChild(e);
            child_obj.gameObject.SetActive(false);
        }
    }
}
