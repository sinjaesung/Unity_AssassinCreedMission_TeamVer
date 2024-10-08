using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class SingleMeleeAttack : MonoBehaviour
{
    public float Timer = 0f;
    public int SingleMeleeVal;
    public Animator anim;
    public PlayerScript playerScript;

    public Transform attackArea;
    public float giveDamage = 10f;
    public float attackRadius;
    public LayerMask knightLayer;

    public AudioSource SwordAudioPlayer; //  소리 재생기
    [SerializeField] public AudioClip SwordClip; // 소리(var)
    [SerializeField] public AudioClip SwordClip2; // 소리(var)
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            Timer += Time.deltaTime;
        }
        else
        {
            Debug.Log("SingleMeleeAttack Mode On:마우스왼쪽클릭down시마다 Timer=0되며 대전모드On");
            anim.SetBool("SingleHandAttackActive", true);
            Timer = 0f;
        }

        if (Timer > 5f)
        {
            Debug.Log("FistSingleMeleeAttack Mode Off, 마우스를 뗀 이후로 5초이상지난 시점에 대전모드Off");
            anim.SetBool("SingleHandAttackActive", false);
        }

        SingleMeleeModes();
    }

    void SingleMeleeModes()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SingleMeleeVal = Random.Range(1, 7);

            if (SingleMeleeVal == 1)
            {        
                Attack();
                //Animation
                StartCoroutine(SingleAttack1());
            }

            if (SingleMeleeVal == 2)
            {               
                Attack();
                //Animation
                StartCoroutine(SingleAttack2());
            }

            if (SingleMeleeVal == 3)
            {             
                Attack();
                //Animation
                StartCoroutine(SingleAttack3());
            }

            if (SingleMeleeVal == 4)
            {            
                Attack();
                //Animation
                StartCoroutine(SingleAttack4());
            }

            if (SingleMeleeVal == 5)
            {            
                Attack();
                //Animation
                StartCoroutine(SingleAttack5());
            }
        }
    }

    void Attack()
    {
        Collider[] hitKnight = Physics.OverlapSphere(attackArea.position, attackRadius, knightLayer);

        foreach (Collider knight in hitKnight)
        {
            Debug.Log("SingleMeleeAttack [[MeleeHitinfo]]:" + knight.transform.name);

            KnightAI knightAI = knight.GetComponent<KnightAI>();
            KnightAI2 knightAI2 = knight.GetComponent<KnightAI2>();
            PoliceMan policeman = knight.GetComponent<PoliceMan>();
            CharacterNavigatorScript character = knight.GetComponent<CharacterNavigatorScript>();
            Boss boss = knight.GetComponent<Boss>();

            if (knightAI != null)
            {
                knightAI.TakeDamage(giveDamage,knight.ClosestPoint(attackArea.position),(knight.transform.position - attackArea.position));
            }
            if (knightAI2 != null)
            {
                knightAI2.TakeDamage(giveDamage, knight.ClosestPoint(attackArea.position), (knight.transform.position - attackArea.position));
            }
            if (character != null)
            {
                character.characterHitDamage(giveDamage, knight.ClosestPoint(attackArea.position), (knight.transform.position - attackArea.position));
            }
            if (policeman != null)
            {
                policeman.characterHitDamage(giveDamage, knight.ClosestPoint(attackArea.position), (knight.transform.position - attackArea.position));
            }
            if (boss != null)
            {
                boss.characterHitDamage(giveDamage, knight.ClosestPoint(attackArea.position), (knight.transform.position - attackArea.position));
            }
            SwordAudioPlayer.PlayOneShot(SwordClip);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackArea == null)
            return;

        Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }

    IEnumerator SingleAttack1()
    {
        anim.SetBool("SingleAttack1", true);
        playerScript.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0f);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("SingleAttack1", false);
        playerScript.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0f);
    }

    IEnumerator SingleAttack2()
    {
        anim.SetBool("SingleAttack2", true);
        playerScript.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0f);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("SingleAttack2", false);
        playerScript.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0f);
    }

    IEnumerator SingleAttack3()
    {
        anim.SetBool("SingleAttack3", true);
        playerScript.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0f);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("SingleAttack3", false);
        playerScript.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0f);
    }

    IEnumerator SingleAttack4()
    {
        anim.SetBool("SingleAttack4", true);
        playerScript.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0f);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("SingleAttack4", false);
        playerScript.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0f);
    }

    IEnumerator SingleAttack5()
    {
        anim.SetBool("SingleAttack5", true);
        SwordAudioPlayer.PlayOneShot(SwordClip2);
        playerScript.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0f);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("SingleAttack5", false);
        playerScript.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0f);
        SwordAudioPlayer.PlayOneShot(SwordClip2);
    }
}
