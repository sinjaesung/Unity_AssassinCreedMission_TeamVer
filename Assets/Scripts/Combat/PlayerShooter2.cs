/*using UnityEngine;

// �־��� Gun ������Ʈ�� ��ų� ������
// �˸��� �ִϸ��̼��� ����ϰ� IK�� ����� ĳ���� ����� �ѿ� ��ġ�ϵ��� ����
public class PlayerShooter2 : MonoBehaviour
{
    //������
    public Gun_Sniper gun; // ����� ��
    public Transform gunPivot; // �� ��ġ�� ������

    private Animator playerAnimator; // �ִϸ����� ������Ʈ


    public bool isMoving;

    public float Timer = 0f;//��� ��ȯ ����

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
        
        //����,����(������ ������)
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
            playerAnimator.SetBool("RifleActive", true);
            playerAnimator.SetBool("Shooting", true);
            //���� �߻��� �� �ִ��� üũ�ϴ� �Լ� ���� ( gun ��ũ��Ʈ�� Fire)
            Debug.Log("RifleActive Mode On:���콺����Ŭ��down�ø��� Timer=0�Ǹ� �������On");
            gun.Fire();
            Timer = 0f;
        }
        else if (!Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetBool("Shooting", false);
            Timer += Time.deltaTime;
        }


        //���� �������Ѵٴ� �Է��� �������� ��
        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("�� ���ε�");
            //������
            if (gun.Reload() == true)//�� Ÿ�ֿ̹� �̹� ���ε� �Լ��� ����ƴ�.
            {
                // playerAnimator.SetTrigger("Reload");
            }
        }
        //UpdateUI();
        //������
        //�������� �������� �� ���� �ִϸ��̼��� ����
        if (Timer > 5f)
        {
            Debug.Log("RifleActive Mode Off, ���콺�� �� ���ķ� 5���̻����� ������ �������Off");
            playerAnimator.SetBool("RifleActive", false);
        }
    }
}*/