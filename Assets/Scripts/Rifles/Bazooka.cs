using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour
{
    [Header("Bazooka Things")]
    public Transform shootingArea;
    public float giveDamage = 120f;
    public float shootingRange = 100f;
    public Animator animator;
    public bool isMoving;
    public PlayerScript playerScript;

    [Header("Bazooka Ammunition and reloading")]
    private int maximumAmmunition = 1;
    public int presentAmmunition;
    public int mag;
    public float reloadingTime;
    private bool setReloading;
    public GameObject crosshair;

    public float Timer = 0f;

    private void Awake()
    {
        crosshair = FindObjectOfType<Crosshair>().gameObject;
    }
    private void Start()
    {
        presentAmmunition = maximumAmmunition;
    }

    private void Update()
    {
        if (animator.GetFloat("movementValue") > 0.001f)
        {
            isMoving = true;
        }
        else if (animator.GetFloat("mvovementValue") < 0.0999999f)
        {
            isMoving = false;
        }

        if (setReloading)
            return;

        if (presentAmmunition <= 0 && mag > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0) && isMoving == false)
        {
            animator.SetBool("BazookaActive", true);
            animator.SetBool("BazookaShooting", true);
            Shoot();
            Timer = 0f;
        }
        else if (!Input.GetMouseButtonDown(0))
        {
            animator.SetBool("BazookaShooting", false);
            Timer += Time.deltaTime;
        }

        if (Input.GetMouseButton(1))
        {
           // animator.SetBool("BazookaAim", true);
            //crosshair.SetActive(true);
        }
        else if (!Input.GetMouseButton(1))
        {
            //animator.SetBool("RifleAim", false);
            //crosshair.SetActive(false);
        }

        if(Timer > 5f)
        {
            Debug.Log("Bazooka mode off,���콺�� �� ���ķ� 5���̻� ���������� �������off");
            animator.SetBool("BazookaActive", false);
        }
    }

    void Shoot()
    {
        if (mag <= 0)
        {
            //show out UI
            return;
        }

        presentAmmunition--;

        if (presentAmmunition == 0)
        {
            mag--;
        }
        RaycastHit hitInfo;

        if (Physics.Raycast(shootingArea.position, shootingArea.forward, out hitInfo, shootingRange))
        {
            Debug.Log("Bazooka [[Hitinfo]]:" + hitInfo.transform.name);
            KnightAI knightAI = hitInfo.transform.GetComponent<KnightAI>();
            KnightAI2 knightAI2 = hitInfo.transform.GetComponent<KnightAI2>();
            PoliceMan policeman = hitInfo.transform.GetComponent<PoliceMan>();
            CharacterNavigatorScript character = hitInfo.transform.GetComponent<CharacterNavigatorScript>();
            Boss boss = hitInfo.transform.GetComponent<Boss>();

            if (knightAI != null)
            {
                Debug.Log("Bazooka knight1 Damage");
                knightAI.TakeDamage(giveDamage);
            }
            if (knightAI2 != null)
            {
                Debug.Log("Bazooka knight2 Damage");
                knightAI2.TakeDamage(giveDamage);
            }
            if (character != null)
            {
                Debug.Log("Bazooka character Damage");
                character.characterHitDamage(giveDamage);
            }
            if (policeman != null)
            {
                Debug.Log("Bazooka policeman Damage");
                policeman.characterHitDamage(giveDamage);
            }
            if (boss != null)
            {
                Debug.Log("Bazooka boss Damage");
                boss.characterHitDamage(giveDamage);
            }
        }
    }

    IEnumerator Reload()
    {
        setReloading = true;
        animator.SetFloat("movementValue", 0f);
        //playerScript.movementSpeed = 0f;
        animator.SetBool("ReloadBazooka", true);
        //reloading anim
        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("ReloadBazooka", false);
        presentAmmunition = maximumAmmunition;
        setReloading = false;
        animator.SetFloat("movementValue", 0f);
        //playerScript.movementSpeed = 5f;
    }
}
