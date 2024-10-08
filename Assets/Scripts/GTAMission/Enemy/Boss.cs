using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float bossHealth = 120f;
    public Animator animator;
    public Missions missions;

    private void Update()
    {
        if (bossHealth < 120)
        {
            //animation
            //animator.SetBool("Shooting", true);
        }
        if (bossHealth <= 0)
        {
            //pass mission
            if (missions.Mission1 == true && missions.Mission2 == true && missions.Mission3 == true && missions.Mission4 == false)
            {
                //미션4
                missions.Mission4 = true;
            }

            Object.Destroy(gameObject, 8.0f);
            //animation
            animator.SetBool("Died", true);
            animator.SetBool("Shooting", false);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    public bool IsValid()
    {
        if (missions.Mission1 == true && missions.Mission2 == true && missions.Mission3 == true && missions.Mission4 == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void characterHitDamage(float takeDamage)
    {
        if (IsValid())
        {
            bossHealth -= takeDamage;
            animator.SetBool("Shooting", true);
        }
        else
        {
            Debug.Log("BOSS>>데미지 무효");
        }
    }
}