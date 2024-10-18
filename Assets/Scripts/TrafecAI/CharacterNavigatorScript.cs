using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CharacterNavigatorScript : MonoBehaviour
{
    [Header("Character Info")]
    public float movingSpeed;
    public float runningSpeed;
    public float turningSpeed = 300f;
    public float stopSpeed = 1f;
    [SerializeField] private float characterHealth = 120f;
    public float presentHealth;

    [Header("Destination Var")]
    public Vector3 destination;
    public bool destinationReached;
    public Animator animator;

    [Header("General AI")]
    public GameObject playerBody;
    public LayerMask PlayerLayer;
    public Player player;
    public float visionRadius;
    public bool IsAttacked = false;
    public bool playerInvisionRadius;
    public float NightVisionReduce;
    public float originVisionRadius;

    public NavMeshAgent navagent;
    public bool isDied = false;
    public float runDistance = 15f;//How far the enemy runs away from the player

    public Image healthbar;

    public GameManager gameManager;

    public enum Status
    {
        Walk = 0,
        Chase,
        Shoot
    }
    public Status nowStatus = Status.Walk;

    public ParticleSystem hitEffect; // 피격시 재생할 파티클 효과
    public AudioClip deathSound; // 사망시 재생할 소리
    public AudioClip hitSound; // 피격시 재생할 소리

    public AudioSource audiosource;

    private Renderer enemyRenderer; // 렌더러 컴포넌트
    public Color OriginalColor;
    public bool IsPaused = false;//얼려졌을때 또는 스턴기
    public List<string> StateList = new List<string>();

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        OriginalColor = Color.white;

        presentHealth = characterHealth;
        if(healthbar!=null)
             healthbar.fillAmount = presentHealth / characterHealth;

        //playerBody = FindObjectOfType<PlayerScript>().gameObject;
        player = GameObject.FindObjectOfType<Player>();

        navagent = GetComponent<NavMeshAgent>();

        audiosource = GetComponent<AudioSource>();
        enemyRenderer = GetComponentInChildren<Renderer>();
        StartCoroutine(UpdateStateCoroutine());
    }
    private void OnDisable()
    {
        Debug.Log("Enemy오브젝트 제거시 모든 코루틴 종료");
        StopAllCoroutines();
    }
    //상태관련
    public void AddStateList(string item)
    {
        StateList.Add(item);
    }
    public void DeleteStateListItem(string item)
    {
        for (int n = 0; n < StateList.Count; n++)
        {
            Debug.Log("Enemy타깃 " + n + $"| {StateList[n]}");
        }
        Debug.Log("Enemy타깃 오브젝트 Freeze속성 있는거 모두 제거!" + transform.name);
        StateList.RemoveAll(e => e == "Freeze");
    }
    private IEnumerator UpdateStateCoroutine()
    {
        while (true)
        {
            if (StateList.Contains("Freeze"))
            {
                Debug.Log("해당 오브젝트에서 얼려진 상태가 발견된경우, 색상컬러 파랗게하는거랑,움직임 멈추게끔");
                enemyRenderer.material.color = Color.blue;
            }
            else
            {
                Debug.Log("해당 오브젝트에서 얼려진 상태가 해제");
                enemyRenderer.material.color = OriginalColor;
            }

            yield return null;
        }
    }
    private void Update()
    {
        if (gameManager.isNight)
        {
            visionRadius = originVisionRadius - NightVisionReduce;
            visionRadius = Mathf.Max(0, visionRadius);
        }
        else
        {
            visionRadius = originVisionRadius;
        }

        if (FindObjectOfType<PlayerScript>() != null)
        {
            playerBody = FindObjectOfType<PlayerScript>().gameObject;
        }

        player = GameObject.FindObjectOfType<Player>();

        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);

        if(IsPaused == false)
        {
            if (playerInvisionRadius && IsAttacked == true)
            {
                Debug.Log("AICharacterNavigatorScript>> 플레이어로부터 한번이상 공격을 받았었고, " +
                    "플레이어가 자기 주변 인지반경내에 있는경우에 플레이어로부터 도망친다");
                RunAwayFromPlayer();
            }
            else
            {
                Debug.Log("AICharacterNavigatorScript 플레이어가 자기 주변 반경에 있지 않으면 다시 일반걷기");
                animator.SetBool("Run", false);
                Walk();
            }
        }
        else
        {
            Debug.Log("AICharacter얼려져 있는 상태>>");
        }
    }
    void RunAwayFromPlayer()
    {
        navagent.speed = runningSpeed;

        animator.SetBool("Run", true);

        //Get the direction away from the player
        Vector3 directionAwayFromPlayer = (transform.position - playerBody.transform.position).normalized;

        //Calculate the new destination for the enemy to move to
        Vector3 runToPosition = transform.position + directionAwayFromPlayer * runDistance;

        //Ensure the destination is on the NavMesh
        NavMeshHit navHit;
        if(NavMesh.SamplePosition(runToPosition,out navHit, runDistance, -1))
        {
            //Set the agents' destination to the calculated position
            navagent.SetDestination(navHit.position);
        }
    }

    public void Walk()
    {  
        if(transform.position != destination)
        {
            animator.SetBool("Run", false);

            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;

            navagent.speed = movingSpeed;

            if (destinationDistance >= stopSpeed)
            {
                //Turning
                destinationReached = false;
                //Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningSpeed * Time.deltaTime);

                //Moving AI
                // transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
                navagent.SetDestination(destination);
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
    public void characterHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthbar.fillAmount = presentHealth / characterHealth;
        IsAttacked = true;

        if (presentHealth <= 0f)
        {
            if (!isDied)
            {
                characterDie();
            }
        }
    }
    public void characterHitDamage(float takeDamage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Debug.Log("Character HitDamage>>" + hitPoint);
        presentHealth -= takeDamage;
        healthbar.fillAmount = presentHealth / characterHealth;
        IsAttacked = true;

        if (!isDied)
        {
            //죽지 않았을 때에만 피격 효과 발동 => 효과음,피가 튀는 이펙트 효과
            audiosource.PlayOneShot(hitSound);

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
        movingSpeed = 0f;

        //AI 추격 중지
        navagent.isStopped = true;
        //navagent.enabled = false;

        //이펙트 실행
        audiosource.PlayOneShot(deathSound);

        Object.Destroy(gameObject, 6.0f);
        player.currentkills += 1;
        player.playerMoney += 10;
    }
}
