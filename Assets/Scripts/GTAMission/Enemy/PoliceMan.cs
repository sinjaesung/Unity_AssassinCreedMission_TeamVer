using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

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
    public bool playerInvisionRadius;
    public bool playerInattackRadius;

    [Header("Police Attack Var")]
    public int SingleMeleeVal;
    public Transform attackArea;
    public float giveDamage;
    public float attackingRadius;
    public bool previouslyAttack;
    public float timebtwAttack;

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

    private Renderer enemyRenderer; // ������ ������Ʈ
    public Color OriginalColor;
    public bool IsPaused = false;//��������� �Ǵ� ���ϱ�
    public List<string> StateList = new List<string>();

    public Image healthbar;
    public enum Status
    {
        Walk = 0,
        Chase,
        Attack
    }
    private void Start()
    {
        OriginalColor = Color.white;
        audiosource = GetComponent<AudioSource>();

        presentHealth = characterHealth;
        healthbar.fillAmount = presentHealth / characterHealth;

        playerBody = FindObjectOfType<PlayerScript>().gameObject;
        wantedlevelScript = GameObject.FindObjectOfType<WantedLevel>();
        CurrentmovingSpeed = movingSpeed;
        player = GameObject.FindObjectOfType<Player>();
        StartCoroutine(StartSetup());

        navagent = GetComponent<NavMeshAgent>();
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
        playerInattackRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if (IsPaused == false)
        {
            if (playerInvisionRadius && !playerInattackRadius && (wantedlevelScript.level1 == true || wantedlevelScript.level2 == true ||
                       wantedlevelScript.level3 == true || wantedlevelScript.level4 == true || wantedlevelScript.level5 == true))
            {
                //���谡 �������� �������� ������ ����
                //Debug.Log("PoliceOFficer ChasePlayer���� ����:");
                nowStatus = Status.Chase;
                ChasePlayer();
            }
            else if (playerInvisionRadius && playerInattackRadius && (wantedlevelScript.level1 == true || wantedlevelScript.level2 == true ||
                wantedlevelScript.level3 == true || wantedlevelScript.level4 == true || wantedlevelScript.level5 == true))
            {
                //���谡 �������� ��������,���ݹ������� ������ ����
                //  Debug.Log("PoliceOFficer ShootPlayer���� ����:");
                nowStatus = Status.Attack;
                //ShootPlayer();
                SingleMeleeModes();
            }
            else
            {
                //���谡 ���������� ����,�ν� ������ ���ų�,���ݹ����� �ִµ� ���谡 �ȳ��������� �ȴ´�.
                //Debug.Log("PoliceOFficer walk���� ����:");
                nowStatus = Status.Walk;
                Walk();
            }
        }
        else
        {
            Debug.Log("PoliceMan����� �ִ� ����>>");
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
                if (!playerInattackRadius)
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
                    animator.SetBool("SingleHandAttackActive", false);
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
        transform.LookAt(playerBody.transform.position);

        navagent.speed = runningSpeed;

        if (!playerInattackRadius)
        {
            //transform.position += transform.forward * CurrentmovingSpeed * Time.deltaTime;
            if (playerBody != null)
            {
                //transform.LookAt(playerBody.transform);
                navagent.SetDestination(playerBody.transform.position);
            }

            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
            animator.SetBool("SingleHandAttackActive", false);

            CurrentmovingSpeed = runningSpeed;
        }  
    }

   /* public void ShootPlayer()
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
    }*/
   void SingleMeleeModes()
    {
        playerBody = FindObjectOfType<PlayerScript>().gameObject;

        transform.LookAt(playerBody.transform.position);

        animator.SetBool("Walk", false);
        animator.SetBool("SingleHandAttackActive", true);

        if (!previouslyAttack)
        {
            Debug.Log("PoliceMan SingleMeleeModes�� ������ ������ ��쿡 ó���ȴ� �ִϸ��̼�1~5 ��������");
            SingleMeleeVal = Random.Range(1, 5);

            if(SingleMeleeVal == 1)
            {
                Attack();
                //Animation
                StartCoroutine(Attack1());
            }

            if (SingleMeleeVal == 2)
            {
                Attack();
                //Animation
                StartCoroutine(Attack2());
            }

            if (SingleMeleeVal == 3)
            {
                Attack();
                //Animation
                StartCoroutine(Attack3());
            }

            if (SingleMeleeVal == 4)
            {
                Attack();
                //Animation
                StartCoroutine(Attack4());
            }
        }
    }
    void Attack()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackArea.position, attackingRadius, PlayerLayer);

        foreach(Collider player in hitPlayer)
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();

            if(playerScript != null)
            {
                Debug.Log("Hitting.Player");
                playerScript.playerHitDamage(giveDamage);
            }
        }

        previouslyAttack = true;
        Invoke(nameof(ActiveAttack), timebtwAttack);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackArea == null)
            return;

        Gizmos.DrawWireSphere(attackArea.position, attackingRadius);
    }
    private void ActiveAttack()
    {
        previouslyAttack = false;
    }
    IEnumerator Attack1()
    {
        Debug.Log("Policeman Attack1");

        animator.SetBool("Attack1", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Attack1", false);
    }
    IEnumerator Attack2()
    {
        Debug.Log("Policeman Attack2");

        animator.SetBool("Attack2", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Attack2", false);
    }
    IEnumerator Attack3()
    {
        Debug.Log("Policeman Attack3");

        animator.SetBool("Attack3", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Attack3", false);
    }
    IEnumerator Attack4()
    {
        Debug.Log("Policeman Attack4");

        animator.SetBool("Attack4", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Attack4", false);
    }
    public void characterHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthbar.fillAmount = presentHealth / characterHealth;

        if (presentHealth <= 0)
        {
            if (!isDied)
            {
                characterDie();
            }
        }
    }
    public void characterHitDamage(float takeDamage, Vector3 hitPoint, Vector3 hitNormal)
    {
        presentHealth -= takeDamage;
        healthbar.fillAmount = presentHealth / characterHealth;

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
        animator.SetBool("isDead", true);
        CurrentmovingSpeed = 0f;

        //AI �߰� ����
        navagent.isStopped = true;
       // navagent.enabled = false;

        //����Ʈ ����
        audiosource.PlayOneShot(deathSound);

        Object.Destroy(gameObject, 6.0f);
        player.currentkills += 1;
        player.playerMoney += 10;
    }
}