using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public NavMeshAgent navagent;
    public bool isDied = false;
    public float runDistance = 15f;//How far the enemy runs away from the player

    public ParticleSystem hitEffect; // �ǰݽ� ����� ��ƼŬ ȿ��
    public AudioClip deathSound; // ����� ����� �Ҹ�
    public AudioClip hitSound; // �ǰݽ� ����� �Ҹ�
    private AudioSource enemyAudioPlayer; // ����� �ҽ� ������Ʈ

    private void Start()
    {
        presentHealth = characterHealth;
        //playerBody = FindObjectOfType<PlayerScript>().gameObject;
        player = GameObject.FindObjectOfType<Player>();

        navagent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (FindObjectOfType<PlayerScript>() != null)
        {
            playerBody = FindObjectOfType<PlayerScript>().gameObject;
        }

        player = GameObject.FindObjectOfType<Player>();

        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);

        if(playerInvisionRadius && IsAttacked == true)
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

    public void characterHitDamage(float takeDamage, Vector3 hitPoint, Vector3 hitNormal)
    {
        presentHealth -= takeDamage;
        IsAttacked = true;

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
        movingSpeed = 0f;

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
