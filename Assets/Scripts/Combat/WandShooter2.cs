/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandShooter2 : MonoBehaviour
{
    public Wand wand; //����� �ϵ�(����ī)
    public Transform gunPivot; // �� ��ġ�� ������

    private Animator playerAnimator; // �ִϸ����� ������Ʈ

    public bool isMoving;
    public float Timer = 0f;

    public CombatActionUI combatactionui;

    public LayerMask enemyLayer;
    public float attackRange;
    public bool enemyInvisionRadius;

    private void Start()
    {
        //playerinput,playeranimator ���� �޾ƿ���
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        //���Ϳ� ���� �׻� �Բ� �ֵ���
        //���Ͱ� Ȱ��ȭ�Ǹ�,�ѵ� Ȱ��ȭ�ǵ��� �Ѵ�.
        wand.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        //���Ͱ� ������� �ѵ� ��Ȱ��ȭ�ǵ��� �Ѵ�
        wand.gameObject.SetActive(false);
    }

    private void Update()
    {
        enemyInvisionRadius = Physics.CheckSphere(transform.position, attackRange, enemyLayer);
        combatactionui.GunReloadAction.SetActive(false);

        if (enemyInvisionRadius)
        {
            combatactionui.AllCombatClear();
            combatactionui.FireStrikeAttackAction.SetActive(true);
        }
        else
        {
            combatactionui.AllCombatClear();
            combatactionui.FireStrikeAttackAction.SetActive(false);
        }

        if (playerAnimator.GetFloat("movementValue") > 0.001f)
        {
            isMoving = true;
        }
        else if (playerAnimator.GetFloat("mvovementValue") < 0.0999999f)
        {
            isMoving = false;
        }
        // �Է��� �����ϰ� �� �߻��ϰų� ������

        //���� �߻��Ѵٴ� �Է��� �������� ��
        //�� �߻� ��ũ��Ʈ�� ����. (gun ��ũ��Ʈ�� Fire)
        if (Input.GetMouseButtonDown(0))
        {
            //���� �߻��� �� �ִ��� üũ�ϴ� �Լ� ���� ( gun ��ũ��Ʈ�� Fire)
            wand.Fire();
            playerAnimator.SetBool("BazookaActive", true);
            playerAnimator.SetBool("BazookaShooting", true);
            Timer = 0;
        }
        else if (!Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetBool("BazookaShooting", false);
            Timer += Time.deltaTime;
        }

        if (Timer > 5f)
        {
            Debug.Log("Bazooka mode off,���콺�� �� ���ķ� 5���̻� ���������� �������off");
            playerAnimator.SetBool("BazookaActive", false);
        }
    }
}
*/