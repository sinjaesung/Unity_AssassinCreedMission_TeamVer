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

    public ParticleSystem hitEffect; // 피격시 재생할 파티클 효과
    public AudioClip deathSound; // 사망시 재생할 소리
    public AudioClip hitSound; // 피격시 재생할 소리
    private AudioSource enemyAudioPlayer; // 오디오 소스 컴포넌트


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
            //수배가 내려지고 범위내에 있으면 추적
            //Debug.Log("PoliceOFficer ChasePlayer조건 충족:");
            nowStatus = Status.Chase;
            ChasePlayer();
        }
        else if (playerInvisionRadius && playerInshootingRadius && (wantedlevelScript.level1 == true || wantedlevelScript.level2 == true ||
            wantedlevelScript.level3 == true || wantedlevelScript.level4 == true || wantedlevelScript.level5 == true))
        {
            //수배가 내려지고 범위내에,공격범위까지 있으면 공격
            //  Debug.Log("PoliceOFficer ShootPlayer조건 충족:");
            nowStatus = Status.Shoot;
            ShootPlayer();
        }
        else
        {
            //수배가 내려졌으나 공격,인식 범위에 없거나,공격범위에 있는데 수배가 안내려졌으면 걷는다.
            //Debug.Log("PoliceOFficer walk조건 충족:");
            nowStatus = Status.Walk;
            Walk();
        }
    }

    public void Walk()
    {
        if (transform.position != destination)
        {
            //일반시민,자동차와는 다른 형태로 좀 처리한다.하늘추적은 안되게 xz방향만 참고해서 쫓아오게해야함.(공중능력없는경우)
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;//대신에 리지드바디까지 처리하여 xz로만 y축 언덕형태의 slope지형또한 결과적으로 나아갈수있게끔처리.
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

        //transform.position += transform.forward * CurrentmovingSpeed * Time.deltaTime;//이동정지
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
            //죽지 않았을 때에만 피격 효과 발동 => 효과음,피가 튀는 이펙트 효과
            enemyAudioPlayer.PlayOneShot(hitSound);

            //이펙트의 위치 : 맞은 위치
            //이펙트가 튀는 방향: 맞은 방향
            hitEffect.transform.position = hitPoint;
            //바라보는 방향을 일치시킨다.
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            //위치 선정 완료 후 재생
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

        //AI 추격 중지
        navagent.isStopped = true;
        navagent.enabled = false;

        //이펙트 실행
        enemyAudioPlayer.PlayOneShot(deathSound);

        Object.Destroy(gameObject, 6.0f);
        player.currentkills += 1;
        player.playerMoney += 10;

    }
}