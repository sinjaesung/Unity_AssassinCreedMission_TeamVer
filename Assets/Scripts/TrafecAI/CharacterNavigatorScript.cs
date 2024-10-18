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

    public ParticleSystem hitEffect; // �ǰݽ� ����� ��ƼŬ ȿ��
    public AudioClip deathSound; // ����� ����� �Ҹ�
    public AudioClip hitSound; // �ǰݽ� ����� �Ҹ�

    public AudioSource audiosource;

    private Renderer enemyRenderer; // ������ ������Ʈ
    public Color OriginalColor;
    public bool IsPaused = false;//��������� �Ǵ� ���ϱ�
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
        Debug.Log("Enemy������Ʈ ���Ž� ��� �ڷ�ƾ ����");
        StopAllCoroutines();
    }
    //���°���
    public void AddStateList(string item)
    {
        StateList.Add(item);
    }
    public void DeleteStateListItem(string item)
    {
        for (int n = 0; n < StateList.Count; n++)
        {
            Debug.Log("EnemyŸ�� " + n + $"| {StateList[n]}");
        }
        Debug.Log("EnemyŸ�� ������Ʈ Freeze�Ӽ� �ִ°� ��� ����!" + transform.name);
        StateList.RemoveAll(e => e == "Freeze");
    }
    private IEnumerator UpdateStateCoroutine()
    {
        while (true)
        {
            if (StateList.Contains("Freeze"))
            {
                Debug.Log("�ش� ������Ʈ���� ����� ���°� �߰ߵȰ��, �����÷� �Ķ����ϴ°Ŷ�,������ ���߰Բ�");
                enemyRenderer.material.color = Color.blue;
            }
            else
            {
                Debug.Log("�ش� ������Ʈ���� ����� ���°� ����");
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
                Debug.Log("AICharacterNavigatorScript>> �÷��̾�κ��� �ѹ��̻� ������ �޾Ҿ���, " +
                    "�÷��̾ �ڱ� �ֺ� �����ݰ泻�� �ִ°�쿡 �÷��̾�κ��� ����ģ��");
                RunAwayFromPlayer();
            }
            else
            {
                Debug.Log("AICharacterNavigatorScript �÷��̾ �ڱ� �ֺ� �ݰ濡 ���� ������ �ٽ� �Ϲݰȱ�");
                animator.SetBool("Run", false);
                Walk();
            }
        }
        else
        {
            Debug.Log("AICharacter����� �ִ� ����>>");
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
            //���� �ʾ��� ������ �ǰ� ȿ�� �ߵ� => ȿ����,�ǰ� Ƣ�� ����Ʈ ȿ��
            audiosource.PlayOneShot(hitSound);

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
        movingSpeed = 0f;

        //AI �߰� ����
        navagent.isStopped = true;
        //navagent.enabled = false;

        //����Ʈ ����
        audiosource.PlayOneShot(deathSound);

        Object.Destroy(gameObject, 6.0f);
        player.currentkills += 1;
        player.playerMoney += 10;
    }
}
