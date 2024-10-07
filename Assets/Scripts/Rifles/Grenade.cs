using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float grenadeTimer = 3f;
    public float radius = 10f;
    float countDown;
    public float giveDamage = 120f;
    bool hasExploded = false;

    public GameObject explosionEffect;

    private void Start()
    {
        countDown = grenadeTimer;
    }

    private void Update()
    {
        countDown -= Time.deltaTime;

        if(countDown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        //Show Effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Get nearby Objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
            Debug.Log("Grenade [[Hitinfo]]:" + nearbyObject.transform.name);
            //Add Force
            //Damage
            Object obj = nearbyObject.GetComponent<Object>();
            KnightAI knightAI = nearbyObject.GetComponent<KnightAI>();
            KnightAI2 knightAI2 = nearbyObject.GetComponent<KnightAI2>();
            Gangster ganster = nearbyObject.GetComponent<Gangster>();
            PoliceMan policeman = nearbyObject.GetComponent<PoliceMan>();

            if (obj != null)
            {
                obj.objectHitDamage(giveDamage);
            }
            if(knightAI != null)
            {
                knightAI.TakeDamage(giveDamage);
            }
            if (knightAI2 != null)
            {
                knightAI2.TakeDamage(giveDamage);
            }
            if (ganster != null)
            {
                ganster.characterHitDamage(giveDamage);
            }
            if (policeman != null)
            {
                policeman.characterHitDamage(giveDamage);
            }
        }

        Destroy(gameObject);
    }
}
