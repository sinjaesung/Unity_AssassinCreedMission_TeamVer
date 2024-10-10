using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class FistFight : MonoBehaviour
{
    public float Timer = 0f;
    public int FistFightVal;
    public Animator anim;
    public PlayerScript playerScript;

    public Transform attackArea;
    public float giveDamage = 10f;
    public float attackRadius;
    public LayerMask knightLayer;
    public Inventory inventory;

    [SerializeField] Transform LeftHandPunch;
    [SerializeField] Transform RightHandPunch;
    [SerializeField] Transform LeftLegKick;

    //Hit Effect
    [SerializeField] GameObject HitEffect1_singlefist;
    [SerializeField] GameObject HitEffect2_doublefist;
    [SerializeField] GameObject HitEffect3_handkick;
    [SerializeField] GameObject HitEffect4_kickcombo;
    [SerializeField] GameObject HitEffect5_leftkick;

    public CombatActionUI combatactionui;

    public LayerMask enemyLayer;
    public float attackRange;
    public bool enemyInvisionRadius;
    private void Update()
    {
        enemyInvisionRadius = Physics.CheckSphere(transform.position, attackRange, enemyLayer);

        if (enemyInvisionRadius)
        {
            combatactionui.AllCombatClear();
            combatactionui.FistAttackAction.SetActive(true);
        }
        else
        {
            combatactionui.AllCombatClear();
            combatactionui.FistAttackAction.SetActive(false);
        }

        if (!Input.GetMouseButtonDown(0))
        {
            Timer += Time.deltaTime;
        }
        else
        {
            Debug.Log("Fist Fight Mode On:���콺����Ŭ��down�ø��� Timer=0�Ǹ� �������On");
            playerScript.movementSpeed = 3f;
            anim.SetBool("FistFightActive", true);
            Timer = 0f;
        }

        if(Timer > 5f)
        {
            Debug.Log("Fist Fight Mode Off, ���콺�� �� ���ķ� 5���̻����� ������ �������Off");
            playerScript.movementSpeed = 5f;
            anim.SetBool("FistFightActive", false);
            inventory.fistFightMode = false;
            Timer = 0f;
            this.gameObject.GetComponent<FistFight>().enabled = false;
        }

        FistFightModes();

        
    }

    void FistFightModes()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("FistFightModes�� ���콺����Ŭ���� ��쿡�� �������εǸ�,���� �ִϸ��̼�1~5 ��������");
            FistFightVal = Random.Range(1, 7);

            if(FistFightVal == 1)
            {
                //Attack
                attackArea = LeftHandPunch;
                attackRadius = 0.5f;
                Attack();
                //Animation
                StartCoroutine(SingleFist());
            }

            if(FistFightVal == 2)
            {
                //Attack
                attackArea = RightHandPunch;
                attackRadius = 0.6f;
                Attack();
                //Animation
                StartCoroutine(DoubleFist());
            }

            if(FistFightVal == 3)
            {
                //Attack
                attackArea = RightHandPunch;
                attackArea = LeftLegKick;
                attackRadius = 0.7f;
                Attack();
                //Animation
                StartCoroutine(FirstFistKick());
            }

            if (FistFightVal == 4)
            {
                //Attack
                attackArea = LeftLegKick;
                attackRadius = 0.9f;
                Attack();
                //Animation
                StartCoroutine(KickCombo());
            }

            if(FistFightVal == 5)
            {
                //Attack
                attackArea = LeftLegKick;
                attackRadius = 0.9f;
                Attack();
                //Animation
                StartCoroutine(LeftKick());
            }
        }
    }

    void Attack()
    {
        Collider[] hitKnight = Physics.OverlapSphere(attackArea.position, attackRadius, knightLayer);

        foreach(Collider knight in hitKnight)
        {
            Debug.Log("FistFight [[MeleeHitinfo]]:" + knight.transform.name);

            KnightAI knightAI = knight.GetComponent<KnightAI>();
            KnightAI2 knightAI2 = knight.GetComponent<KnightAI2>();
            PoliceMan policeman = knight.GetComponent<PoliceMan>();
            CharacterNavigatorScript character = knight.GetComponent<CharacterNavigatorScript>();
            Boss boss = knight.GetComponent<Boss>();

            if (knightAI != null)
            {
                knightAI.TakeDamage(giveDamage);
            }
            /*f(knightAI2 != null)
            {
                knightAI2.TakeDamage(giveDamage);
            }*/
            if (character != null)
            {
                character.characterHitDamage(giveDamage);
            }
            if (policeman != null)
            {
                policeman.characterHitDamage(giveDamage);
            }
            if (boss != null)
            {
                boss.characterHitDamage(giveDamage);
            }

            if (FistFightVal == 1)
            {
                Instantiate(HitEffect1_singlefist, attackArea.transform.position, Quaternion.identity);
            }
            else if (FistFightVal == 2)
            {
                Instantiate(HitEffect2_doublefist, attackArea.transform.position, Quaternion.identity);
            }
            else if (FistFightVal == 3)
            {
                Instantiate(HitEffect3_handkick, attackArea.transform.position, Quaternion.identity);
            }
            else if (FistFightVal == 4)
            {
                Instantiate(HitEffect4_kickcombo, attackArea.transform.position, Quaternion.identity);
            }
            else if (FistFightVal == 5)
            {
                Instantiate(HitEffect5_leftkick, attackArea.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackArea == null)
            return;

        Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }

    IEnumerator SingleFist()
    {
        anim.SetBool("SingleFist", true);
        playerScript.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0f);
        yield return new WaitForSeconds(0.7f);
        anim.SetBool("SingleFist", false);
        playerScript.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0f);
    }

    IEnumerator DoubleFist()
    {
        anim.SetBool("DoubleFist", true);
        playerScript.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0f);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("DoubleFist", false);
        playerScript.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0f);
    }

    IEnumerator FirstFistKick()
    {
        anim.SetBool("FirstFistKick", true);
        playerScript.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0f);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("FirstFistKick", false);
        playerScript.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0f);
    }

    IEnumerator KickCombo()
    {
        anim.SetBool("KickCombo", true);
        playerScript.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0f);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("KickCombo", false);
        playerScript.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0f);
    }

    IEnumerator LeftKick()
    {
        anim.SetBool("LeftKick", true);
        playerScript.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0f);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("LeftKick", false);
        playerScript.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0f);
    }
}
