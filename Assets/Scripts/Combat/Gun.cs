/*using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

// ���� �����Ѵ�
public class Gun : MonoBehaviour
{
    // ���� ���¸� ǥ���ϴµ� ����� Ÿ���� �����Ѵ�
    public enum State
    {
        Ready, // �߻� �غ��
        Empty, // źâ�� ��
        Reloading // ������ ��
    }

    public State state { get; protected set; } // ���� ���� ����

    public Transform fireTransform; // �Ѿ��� �߻�� ��ġ

    public ParticleSystem muzzleFlashEffect; // �ѱ� ȭ�� ȿ��
    public ParticleSystem shellEjectEffect; // ź�� ���� ȿ��

    private LineRenderer bulletLineRenderer; // �Ѿ� ������ �׸��� ���� ������(var)

    protected AudioSource gunAudioPlayer; // �� �Ҹ� �����
    [SerializeField] public AudioClip shotClip; // �߻� �Ҹ�(var)
    public AudioClip reloadClip; // ������ �Ҹ�

    [SerializeField] public float damage = 25; // ���ݷ�(var)
    [SerializeField] protected float fireDistance = 50f; // �����Ÿ�(var)

    [SerializeField] public int ammoRemain = 100; // ���� ��ü ź��(var)
    [SerializeField] public int magCapacity = 25; // źâ �뷮(var)
    [SerializeField] public int magAmmo; // ���� źâ�� �����ִ� ź��(var)


    [SerializeField] public float timeBetFire = 0.12f; // �Ѿ� �߻� ����(var)
    public float reloadTime = 1.8f; // ������ �ҿ� �ð�
    public float lastFireTime; // ���� ���������� �߻��� ����

    public Vector3 HitPos;

    public Animator PlayerAnimator;

    public Camera maincamera;

    public CombatActionUI combatactionui;
    private void Awake()
    {
        maincamera = Camera.main;
        PlayerAnimator = GetComponentInParent<Animator>();

        // ����� ������Ʈ���� ������ ��������
        bulletLineRenderer = GetComponent<LineRenderer>();
        gunAudioPlayer = GetComponent<AudioSource>();

        //������Ʈ ���� ������ �����ϱ� ����
        //���η����� ������ �̰����� ����.
        //���η����� �� ���� ���� ����
        bulletLineRenderer.positionCount = 2;

        //���η����� ��� ��Ȱ��ȭ
        bulletLineRenderer.enabled = false;
    }
    private void Start()
    {
        // �� ���� �ʱ�ȭ

        //���� źâ�� ���� ä���.
        //���� ���¸� '�غ� ����'�� �����.
        //���� �� ������ 0���� �ʱ�ȭ.

        //���� źâ�� ���� ä���.
        magAmmo = magCapacity;
        //���� ���¸� �غ���·� �����.
        state = State.Ready;
        //���� �� �������� 0���� �ʱ�ȭ.
        lastFireTime = 0;
    }
    private void OnEnable()
    {
        
    }

    // �߻� �õ�
    public void Fire()
    {
        //���� üũ: ���� �߻縦 �� �� �ִ� �����ΰ�?
        //�Ѿ� �߻� ���ݸ�ŭ �̻��� �ð��� �귶����
        //�Ѵ� ������ ��쿡 �߻� ó���� �����Ѵ�.

        //�߻� �ð� üũ : ������ �߻� �ð� + �߻� ������ �� �ð��� ���� �ð����� �۴�
        //�ð��� �� �귶�ٴ� ���̴� �߻簡 �����ϵ���.
        Debug.Log($"�� ��� �õ�{Time.time} >= {lastFireTime} + {timeBetFire}({lastFireTime + timeBetFire}>>");

        if (state == State.Ready && Time.time >= lastFireTime + timeBetFire)
        {
            Debug.Log("�� ��� ����>>");
            Shot();
            //���� ������� ������ ���� �� �ð��� ����� �����Ѵ�.
            lastFireTime = Time.time;
        }
        else
        {
            Debug.Log("�� ��Ÿ��");
        }
    }
    private void Update()
    {
        if(magAmmo <= 0)
        {
            combatactionui.GunReloadAction.SetActive(true);
            state = State.Empty;
        }
        else
        {
            combatactionui.GunReloadAction.SetActive(false);
        }
    }

    // ���� �߻� ó��
    protected virtual void Shot()
    {
        Debug.Log("��");

        //���� ��� -> ���� �´´� -> ���� �¾Ҵ��� üũ -> ���ʹ��� ��� �����

        //���� ������ ��Ҵ��� ����ϴ� ��� -> �����ɽ�Ʈ(���� ����)

        RaycastHit hit;

        Vector3 hitPosition = Vector3.zero;

        //�����ɽ�Ʈ ����
        //Physics.Raycast(������ ���� ����,������ ���� ������� ����,�浹 ����(hit�� ����),�ִ뱤������)
        //��������:�ѱ�, ����:��(�ѱ�)�� �ٶ󺸴� ����
        //out~ : out �ڿ� ���� ������ � ���� �����Ѵ�.

        //�����ɽ�Ʈ �Լ��� ����� true Ȥ�� false�� ����ȴ�.
        //true : �����Ÿ� ������ ������ �ε����� �� ��ȯ.
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            //������ ���𰡿� �ε����� �� ���

            //���� ���� ���� ����� �����ߴ��� üũ
            //���� ���� ����̶�? : IDamagable�� �����ϴ� ��ü. => �ش� ��ü�κ��� IDamagable �Ӽ��� ������ �� �ִ�.
            //IDamageable target = hit.collider.GetComponent<IDamageable>();

            //hit�� ��ü�� IDamagable�� ���� ������ � ������ ����� ���̰�,
            //���� ���� �ʴٸ� target�� ������ null�� �� ���̴�.
            KnightAI knightAI = hit.transform.GetComponent<KnightAI>();
            KnightAI2 knightAI2 = hit.transform.GetComponent<KnightAI2>();
            PoliceMan policeman = hit.transform.GetComponent<PoliceMan>();
            CharacterNavigatorScript character = hit.transform.GetComponent<CharacterNavigatorScript>();
            Boss boss = hit.transform.GetComponent<Boss>();

            *//*if (target != null)
            {
                //�ε��� ��ü�� �������� ���� �� �ִ� ����� ��
                //IDamagable�� ���� �ִ� ��ü�� ������ OnDamage �Լ��� ���� �ִ�.

                //���� ������: ���� �����
                //���� ����: �Ѿ��� ���� ����
                //���� ȸ����: �ε��� ����� ȸ���� - hit.normal
                target.OnDamage(damage, hit.point, hit.normal);
            }*//*
            if (knightAI != null)
            {
                knightAI.TakeDamage(damage, hit.point, hit.normal);
            }
            *//*if (knightAI2 != null)
            {
                knightAI2.TakeDamage(damage, hit.point, hit.normal);
            }*//*
            if (character != null)
            {
                character.characterHitDamage(damage, hit.point, hit.normal);
            }
            if (policeman != null)
            {
                policeman.characterHitDamage(damage, hit.point, hit.normal);
            }
            if (boss != null)
            {
                boss.characterHitDamage(damage, hit.point, hit.normal);
            }

            //���� ������ ��󿡰� �Ѿ��� ��ҵ�, ���� �������� ���� ��󿡰� ��ҵ�(��,��Ÿ��)
            //������ ���� ��ġ���� �����Ѵ�.
            hitPosition = hit.point;
        }
        else
        {
            //�����ɽ�Ʈ ������ �����Ÿ� ������ �ƹ��͵� �ε����� �ʾ��� �� ������ �ڵ�

            //�浹 ��ġ: ���� �߻��� ��ġ�κ��� �ִ� �����Ÿ� ������ ��ġ��
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        //��ü�� �¾ҵ�,���� �ʾҵ� ���� �߻��ϸ� ����Ǵ� �ൿ�� �Ʒ��� ����

        //�� �߻� ����Ʈ
        StartCoroutine(ShotEffect(hitPosition));

        HitPos = hitPosition;

        //źâ �Ҹ�
        //���� źâ ���� - 1
        magAmmo--;
        //���� źâ�� 0���� -> ���� ���¸� 'źâ ��'���� �����.
        if (magAmmo <= 0)
        {
            combatactionui.GunReloadAction.SetActive(true);
            state = State.Empty;
        }

        //UIManager.instance.UpdateAmmoText(magAmmo, ammoRemain);
    }

    // �߻� ����Ʈ�� �Ҹ��� ����ϰ� �Ѿ� ������ �׸���
    protected IEnumerator ShotEffect(Vector3 hitPosition)
    {

        //�Ѿ� �߻� �÷��� ����Ʈ
        muzzleFlashEffect.Play();
        //źâ ����Ʈ
        shellEjectEffect.Play();
        //�Ѱ� �Ҹ�
        //�Ѿ� ���� �׷��ֱ�

        //�Ѱ� �Ҹ��� �� ���� ����Ѵ�.
        gunAudioPlayer.PlayOneShot(shotClip);

        //�Ѿ� ���� �׷��ֱ�

        //���� �߱� ���ؼ� ������,������ �˾ƾ� �Ѵ�.
        //������ : �ѱ�
        //������ �� : �ε��� ��ġ
        //SetPosition(�� ��° ������, ��)

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);

        //������,������ ������ ���Ŀ�
        //���� ���̵��� Ȱ��ȭ�Ѵ�,.
        bulletLineRenderer.enabled = true;

        //0.03�ʸ� ���� ���̰�, ���� �ٷ� ���� �������.
        yield return new WaitForSeconds(1.2f);

        bulletLineRenderer.enabled = false;
    }// ������ �õ�
    public bool Reload()
    {

        //�������� �����ϸ� true,�Ұ����ϸ� false

        //������ ���� Ÿ�̹�x, ������ ���϶�, źâ�� �̹� ���� �� ������, ��ü �Ѿ� ������ 0���� ���� ��
        //�� ���� ��쿡�� �������� �����ϴ�.

        if (state == State.Reloading || magAmmo >= magCapacity || ammoRemain <= 0)
        {
            return false;
        }

        //�Ʒ��� ������ ���� ������ ������ ���.
        //���� ����
        //������ �����ϴٰ� ���°� ��ȯ
        StartCoroutine(ReloadRoutine());
        return true;
    }
    // ���� ������ ó���� ����
    protected virtual IEnumerator ReloadRoutine()
    {

        //�ڷ�ƾ ��� ����: ������ �ϴ� ������ �ð� �����̸� ���ؼ� ���.
        Debug.Log("������");

        //���� ���¸� '������ ��'���� �ٲ۴�. => �ߺ� ������ ����
        state = State.Reloading;
        PlayerAnimator.SetBool("ReloadRifle", true);

        //������ ȿ���� ���
        gunAudioPlayer.PlayOneShot(reloadClip);

        //������ �ҿ�ð���ŭ �ð� ������
        yield return new WaitForSeconds(reloadTime);

        //�ð��� ������
        //źâ�� �Ѿ��� ä���.

        //ä��� �Ѿ� ����: ���� źâ�� �ִ� źâ ������ �Ƿ��� ��� �� �ʿ����� ���
        int ammoToFill = magCapacity - magAmmo;

        //�ʿ��� ��ŭ�� ä���, ���� �� ���� �Ѿ� ������ ä���� �� �Ѿ� �������� ���� ��
        //���� �Ѿ��� ���� ��ŭ�� ���� �������Ѵ�.
        if (ammoRemain < ammoToFill)
        {
            //ä���� �� ź�� ������ ���� ź�� ������ ��ġ�ϵ��� �����Ѵ�.
            ammoToFill = ammoRemain;
        }
        PlayerAnimator.SetBool("ReloadRifle", false);

        //���� ź�˿� ź�� ä���
        magAmmo += ammoToFill;
        //��ü ź�˿��� ä�� ź�˸�ŭ ���� ����
        ammoRemain -= ammoToFill;

        //��� �۾��� ������ ���� ���¸� �ٽ� '�غ�' ���·� ��ȯ.
        state = State.Ready;

       // UIManager.instance.UpdateAmmoText(magAmmo, ammoRemain);
        yield return null;
    }
}*/