using UnityEngine;

// �־��� Gun ������Ʈ�� ��ų� ������
// �˸��� �ִϸ��̼��� ����ϰ� IK�� ����� ĳ���� ����� �ѿ� ��ġ�ϵ��� ����
public class PlayerShooter3 : MonoBehaviour
{
    //����ī ��ũ��Ʈ(����,�⺻�� ����Gun���):���� �����ɽ�Ʈ Ÿ�� ��� ��� + Gun_Bazooka
    public Gun_Bazooka gun; // ����� ��(����ī)
    public Transform gunPivot; // �� ��ġ�� ������

    private Animator playerAnimator; // �ִϸ����� ������Ʈ

    public float Timer = 0f;
    public bool isMoving;

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
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        //���Ͱ� ������� �ѵ� ��Ȱ��ȭ�ǵ��� �Ѵ�
        gun.gameObject.SetActive(false);
    }

    private void Update()
    {
        enemyInvisionRadius = Physics.CheckSphere(transform.position, attackRange, enemyLayer);

        if (enemyInvisionRadius)
        {
            combatactionui.AllCombatClear();
            combatactionui.BazookaAttackAction.SetActive(true);
        }
        else
        {
            combatactionui.AllCombatClear();
            combatactionui.BazookaAttackAction.SetActive(false);
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
            gun.Fire();
            playerAnimator.SetBool("BazookaActive", true);
            playerAnimator.SetBool("BazookaShooting", true);
            Timer = 0f;
        }
        else if (!Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetBool("BazookaShooting", false);
            Timer += Time.deltaTime;
        }

        //���� �������Ѵٴ� �Է��� �������� ��
        if (Input.GetKey(KeyCode.R))
        {
            //������
            if (gun.Reload() == true)//�� Ÿ�ֿ̹� �̹� ���ε� �Լ��� ����ƴ�.
            {
                //playerAnimator.SetBool("ReloadBazooka",true);
            }
        }
        //UpdateUI();
        //������
        //�������� �������� �� ���� �ִϸ��̼��� ����
        if (Timer > 5f)
        {
            Debug.Log("Bazooka mode off,���콺�� �� ���ķ� 5���̻� ���������� �������off");
            playerAnimator.SetBool("BazookaActive", false);
        }
    }
}