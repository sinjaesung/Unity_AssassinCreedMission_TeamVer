using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMeleeAttack2 : MonoBehaviour
{
    public float Timer = 0f;
    public int SingleMeleeVal;
    public Animator anim;
    public PlayerScript playerScript;

    public Transform attackArea;
    public float giveDamage = 10f;
    public float origingiveDamage = 10f;
    public float attackRadius;
    public LayerMask knightLayer;

    public AudioSource SwordAudioPlayer; //  �Ҹ� �����
    [SerializeField] public AudioClip SwordClip; // �Ҹ�(var)
    [SerializeField] public AudioClip SwordClip2; // �Ҹ�(var)

    [SerializeField] public AudioClip ThurstSwordClip1; // �Ҹ�(var)
    [SerializeField] public AudioClip ThurstSwordClip2; // �Ҹ�(var)

    public CombatActionUI combatactionui;

    public LayerMask enemyLayer;
    public float attackRange;
    public bool enemyInvisionRadius;

    [Header("ThurstSlash")]
    public float ThrustSlash_DamageMultipier = 1f;
    public float timeBetSkill = 6f;//��ų �ߵ� ����(var)
    public float lastSkillTime; //���������� ��ų�� �ߵ��� ����

    public Renderer[] skinrenderer;
    public GameObject swordobj;
    public GameObject SkillTrailRenderer;
    public GameObject NormalSlashVfx;
    public GameObject SkillSlashVfx;

    public float PowerTime;
    private void Awake()
    {
        SkillTrailRenderer.SetActive(false);
        skinrenderer = swordobj.GetComponentsInChildren<Renderer>();
    }
    private IEnumerator ThrustSkill_Effect()
    {
        giveDamage = giveDamage * ThrustSlash_DamageMultipier;
        SkillTrailRenderer.SetActive(true);
        for (int e = 0; e < skinrenderer.Length; e++)
        {
            var item = skinrenderer[e];
            item.material.EnableKeyword("_EMISSION");
        }

        yield return new WaitForSeconds(PowerTime);
        
        for (int e = 0; e < skinrenderer.Length; e++)
        {
            var item = skinrenderer[e];
            item.material.DisableKeyword("_EMISSION");
        }
        SkillTrailRenderer.SetActive(false);
        giveDamage = origingiveDamage;
    }

    private void Update()
    {
        enemyInvisionRadius = Physics.CheckSphere(transform.position, attackRange, enemyLayer);

        if (enemyInvisionRadius)
        {
            combatactionui.AllCombatClear();
            combatactionui.SwordAttackAction.SetActive(true);

            if (anim.GetBool("SingleHandAttackActive") == true)
            {
                //��Ÿ�� üũ��
                if (Time.time >= lastSkillTime + timeBetSkill)
                {
                    Debug.Log("�ϻ� ��ų ���� ����>>");
                    combatactionui.SwordAttackAction2.SetActive(true);
                }
                else
                {
                    Debug.Log("�ϻ� ��ų ��Ÿ��");
                    combatactionui.SwordAttackAction2.SetActive(false);
                }
            }   
        }
        else
        {
            combatactionui.AllCombatClear();
            combatactionui.SwordAttackAction.SetActive(false);
        }

        if (!Input.GetMouseButtonDown(0))
        {
            Timer += Time.deltaTime;
        }
        else
        {
            Debug.Log("SingleMeleeAttack Mode On:���콺����Ŭ��down�ø��� Timer=0�Ǹ� �������On");
            anim.SetBool("SingleHandAttackActive", true);
            Timer = 0f;
        }

        if (Timer > 5f)
        {
            Debug.Log("SingleMeleeAttack Mode Off, ���콺�� �� ���ķ� 5���̻����� ������ �������Off");
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

        if (Input.GetMouseButtonDown(1))
        {
            if (anim.GetBool("SingleHandAttackActive") == true)
            {
                Debug.Log("�ϻ� ��ų&�ִϸ��̼��� ��� ���ʸ��콺Ŭ������ " +
                   "SingleHandAttackActive ��� �� ��쿡�� ���డ��>>");

                if (Time.time >= lastSkillTime + timeBetSkill)
                {
                    Debug.Log("�ϻ� ��ų ���� ����>>");
                    //��ų�� ��������� ������ ��ų �� �ð��� ����� �����Ѵ�.
                    lastSkillTime = Time.time;

                    StartCoroutine(ThrustSkill_Effect());
                    Speical_Attack();
                    //Animation
                    StartCoroutine(ThrustSlashAttack());
                }
                else
                {
                    Debug.Log("�ϻ� ��ų ��Ÿ��");
                }
            }
            else
            {
                Debug.Log("SingleHandAttackActive��尡 �ƴ� ��쿣 �ϻ콺ų �������� ����");
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
            PoliceMan policeman = knight.GetComponent<PoliceMan>();
            CharacterNavigatorScript character = knight.GetComponent<CharacterNavigatorScript>();
            Boss boss = knight.GetComponent<Boss>();

            var HitImpact = Instantiate(NormalSlashVfx, knight.ClosestPoint(attackArea.position), Quaternion.LookRotation(knight.transform.position - attackArea.position));
            Destroy(HitImpact, 1.6f);

            if (knightAI != null)
            {
                knightAI.TakeDamage(giveDamage, knight.ClosestPoint(attackArea.position), (knight.transform.position - attackArea.position));
            }
            /* if (knightAI2 != null)
             {
                 knightAI2.TakeDamage(giveDamage, knight.ClosestPoint(attackArea.position), (knight.transform.position - attackArea.position));
             }*/
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
    void Speical_Attack()
    {
        Collider[] hitKnight = Physics.OverlapSphere(attackArea.position, attackRadius, knightLayer);

        foreach (Collider knight in hitKnight)
        {
            Debug.Log("SingleMeleeAttack Speical_Attack [[MeleeHitinfo]]:" + knight.transform.name);

            KnightAI knightAI = knight.GetComponent<KnightAI>();
            PoliceMan policeman = knight.GetComponent<PoliceMan>();
            CharacterNavigatorScript character = knight.GetComponent<CharacterNavigatorScript>();
            Boss boss = knight.GetComponent<Boss>();

            var HitImpact = Instantiate(SkillSlashVfx, knight.ClosestPoint(attackArea.position), Quaternion.LookRotation(knight.transform.position - attackArea.position));
            Destroy(HitImpact, 1.6f);

            if (knightAI != null)
            {
                knightAI.TakeDamage(giveDamage, knight.ClosestPoint(attackArea.position), (knight.transform.position - attackArea.position));
            }
            /* if (knightAI2 != null)
             {
                 knightAI2.TakeDamage(giveDamage, knight.ClosestPoint(attackArea.position), (knight.transform.position - attackArea.position));
             }*/
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
            SwordAudioPlayer.PlayOneShot(ThurstSwordClip2);
        }
    }
    IEnumerator ThrustSlashAttack()
    {
        anim.SetBool("ThrustSlash", true);
        SwordAudioPlayer.PlayOneShot(ThurstSwordClip1);
        playerScript.movementSpeed = 0f;
        anim.SetFloat("movementValue", 0f);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("ThrustSlash", false);
        playerScript.movementSpeed = 5f;
        anim.SetFloat("movementValue", 0f);
        SwordAudioPlayer.PlayOneShot(ThurstSwordClip1);
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
