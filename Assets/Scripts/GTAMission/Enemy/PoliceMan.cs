using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.AI;
//using UnityEditor.PackageManager;
using UnityEngine;

public class PoliceMan : MonoBehaviour
{
    [Header("Character Info")]
    public float movingSpeed;
    public float runningSpeed;
    private float CurrentmovingSpeed;
    public float turningSpeed = 300f;
    public float stopSpeed = 1f;
    [SerializeField] private float characterHealth = 200f;
    public float presentHealth;


    [Header("Destination Var")]
    public Vector3 destination;
    public bool destinationReached;
    public Animator animator;

    [Header("Police AI")]
    public GameObject playerBody;
    public LayerMask PlayerLayer;
    public float visionRadius;
    public float shootingRadius;
    public bool playerInvisionRadius;
    public bool playerInshootingRadius;

    [Header("Police Shooting Var")]
    public float giveDamageOf = 3f;
    public float shootingRange = 20f;
    public GameObject ShootingRaycastArea;
    public float timebtwShoot;
    bool previouslyShoot;
    public WantedLevel wantedlevelScript;
    public Player player;
    public GameObject bloodEffect;

    public AudioSource audiosource;

    public NavMeshAgent navagent;

    public bool isDied = false;

    public Status nowStatus = Status.Walk;

    public ParticleSystem hitEffect; // �ǰݽ� ����� ��ƼŬ ȿ��
    public AudioClip deathSound; // ����� ����� �Ҹ�
    public AudioClip hitSound; // �ǰݽ� ����� �Ҹ�
    private AudioSource enemyAudioPlayer; // ����� �ҽ� ������Ʈ


    public enum Status
    {
        Walk = 0,
        Chase,
        Shoot
    }
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();

        presentHealth = characterHealth;
        playerBody = FindObjectOfType<PlayerScript>().gameObject;
        wantedlevelScript = GameObject.FindObjectOfType<WantedLevel>();
        CurrentmovingSpeed = movingSpeed;
        player = GameObject.FindObjectOfType<Player>();
        StartCoroutine(StartSetup());

        navagent = GetComponent<NavMeshAgent>();
    }
    private IEnumerator StartSetup()
    {
        yield return new WaitForSeconds(1f);

        playerBody = FindObjectOfType<PlayerScript>().gameObject;
        player = GameObject.FindObjectOfType<Player>();
    }
    private void Update()
    {
        playerBody = FindObjectOfType<PlayerScript>().gameObject;
        player = GameObject.FindObjectOfType<Player>();

        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInshootingRadius = Physics.CheckSphere(transform.position, shootingRadius, PlayerLayer);

       
        if (playerInvisionRadius && !playerInshootingRadius && (wantedlevelScript.level1 == true || wantedlevelScript.level2 == true ||
            wantedlevelScript.level3 == true || wantedlevelScript.level4 == true || wantedlevelScript.level5 == true))
        {
            //���谡 �������� �������� ������ ����
            //Debug.Log("PoliceOFficer ChasePlayer���� ����:");
            nowStatus = Status.Chase;
            ChasePlayer();
        }
        else if (playerInvisionRadius && playerInshootingRadius && (wantedlevelScript.level1 == true || wantedlevelScript.level2 == true ||
            wantedlevelScript.level3 == true || wantedlevelScript.level4 == true || wantedlevelScript.level5 == true))
        {
            //���谡 �������� ��������,���ݹ������� ������ ����
            //  Debug.Log("PoliceOFficer ShootPlayer���� ����:");
            nowStatus = Status.Shoot;
            ShootPlayer();
        }
        else
        {
            //���谡 ���������� ����,�ν� ������ ���ų�,���ݹ����� �ִµ� ���谡 �ȳ��������� �ȴ´�.
            //Debug.Log("PoliceOFficer walk���� ����:");
            nowStatus = Status.Walk;
            Walk();
        }
    }

    public void Walk()
    {
        if (transform.position != destination)
        {
            //�Ϲݽù�,�ڵ����ʹ� �ٸ� ���·� �� ó���Ѵ�.�ϴ������� �ȵǰ� xz���⸸ �����ؼ� �Ѿƿ����ؾ���.(���ߴɷ¾��°��)
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;//��ſ� ������ٵ���� ó���Ͽ� xz�θ� y�� ��������� slope�������� ��������� ���ư����ְԲ�ó��.
            float destinationDistance = destinationDirection.magnitude;

            navagent.speed = movingSpeed;

            if (destinationDistance >= stopSpeed)
            {
                if (!playerInshootingRadius)
                {
                    //Turning
                    destinationReached = false;
                    //Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                    //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningSpeed * Time.deltaTime);

                    //Debug.Log("PoliceMan Walk Rotation>>" + targetRotation);
                    //Move AI
                    //transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
                    navagent.SetDestination(destination);

                    animator.SetBool("Walk", true);
                    animator.SetBool("Shoot", false);
                    animator.SetBool("Run", false);
                }
            }
            else
            {
                destinationReached = true;
            }
        }
    }

    public void LocateDestination(Vector3 destination)
    {
        this.destination = destination;
        destinationReached = false;
    }

    public void ChasePlayer()
    {
        //Vector3 PlayerToDirection = playerBody.transform.position - transform.position;
        //PlayerToDirection.y = 0;
        playerBody = FindObjectOfType<PlayerScript>().gameObject;

        navagent.speed = runningSpeed;

        if (!playerInshootingRadius)
        {
            //transform.position += transform.forward * CurrentmovingSpeed * Time.deltaTime;
            if (playerBody != null)
            {
                //transform.LookAt(playerBody.transform);
                navagent.SetDestination(playerBody.transform.position);
            }

            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Shoot", false);

            CurrentmovingSpeed = runningSpeed;
        }
    }

    public void ShootPlayer()
    {
        CurrentmovingSpeed = 0f;

        //transform.position += transform.forward * CurrentmovingSpeed * Time.deltaTime;//�̵�����
        navagent.speed = 0;
        playerBody = FindObjectOfType<PlayerScript>().gameObject;
        //Vector3 shootingAreaWorldPos = ShootingRaycastArea.transform.TransformDirection(Vector3.forward);
        //Vector3 ShootingDirection = playerBody.transform.position - shootingAreaWorldPos;
        if (playerBody != null)
        {
            transform.LookAt(playerBody.transform);
        }

        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Shoot", true);

        if (!previouslyShoot)
        {
            RaycastHit hit;
            if (Physics.Raycast(ShootingRaycastArea.transform.position, ShootingRaycastArea.transform.forward, out hit, shootingRange))
            {
                Debug.Log("Policeman Shooting target>>" + hit.transform.name);

                PlayerScript playerBody = hit.transform.GetComponent<PlayerScript>();
                
                Debug.DrawLine(transform.position, ShootingRaycastArea.transform.forward);

                if (playerBody != null)
                {
                    playerBody.playerHitDamage(giveDamageOf);
                    GameObject bloodEffectGo = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(bloodEffectGo, 1f);
                }
            }

            previouslyShoot = true;
            Invoke(nameof(ActiveShooting), timebtwShoot);
        }
    }


    private void ActiveShooting()
    {
        previouslyShoot = false;
    }

    public void characterHitDamage(float takeDamage, Vector3 hitPoint, Vector3 hitNormal)
    {
        presentHealth -= takeDamage;

        if (!isDied)
        {
            //���� �ʾ��� ������ �ǰ� ȿ�� �ߵ� => ȿ����,�ǰ� Ƣ�� ����Ʈ ȿ��
            enemyAudioPlayer.PlayOneShot(hitSound);

            //����Ʈ�� ��ġ : ���� ��ġ
            //����Ʈ�� Ƣ�� ����: ���� ����
            hitEffect.transform.position = hitPoint;
            //�ٶ󺸴� ������ ��ġ��Ų��.
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            //��ġ ���� �Ϸ� �� ���
            hitEffect.Play();
        }
        if (presentHealth <= 0)
        {
            if (!isDied)
            {
                characterDie();
            }
        }
    }

    private void characterDie()
    {
        isDied = true;
       // audiosource.Play();
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        animator.SetBool("Die", true);
        CurrentmovingSpeed = 0f;
        shootingRange = 0f;

        //AI �߰� ����
        navagent.isStopped = true;
        navagent.enabled = false;

        //����Ʈ ����
        enemyAudioPlayer.PlayOneShot(deathSound);

        Object.Destroy(gameObject, 6.0f);
        player.currentkills += 1;
        player.playerMoney += 10;

    }
}