using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TutorialMovement : TutorialBase
{
    [SerializeField]
    private NavMeshAgent navAgent;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private bool isCompleted = false;
    [SerializeField]
    public GameObject movementCharacter;
    [SerializeField]
    public Transform playerTarget;
    [SerializeField]
    private Animator anim;

    public void SetData(Transform player_)
    {
        playerTarget = player_;
    }
    public override void Enter()
    {
        playerTarget = FindObjectOfType<PlayerScript>().transform;
        for (int e = 0; e < FindObjectsOfType<TutorialMovement>().Length; e++)
        {
            var item = FindObjectsOfType<TutorialMovement>()[e];
            if (item != this)
            {
                item.gameObject.SetActive(false);
            }
        }

        navAgent.speed = moveSpeed;
        gameObject.SetActive(true);

        Debug.Log("TutorialMovement Enter>>");
        Movement();//플레이어에게 다가간다.
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted == true)
        {
            controller.SetNextTutorial();
        }
    }
    private void Movement()
    {
        anim.SetBool("Run", true);
        navAgent.SetDestination(playerTarget.position);
    }

    public override void Exit()
    {
        Debug.Log("TutorialMovement Exit>>");
    }
    private void Update()
    {
        if (!isCompleted)
        {
            navAgent.SetDestination(playerTarget.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isCompleted)
        {
            if (other.CompareTag("Player"))
            {
                isCompleted = true;
                anim.SetBool("Run", false);
                anim.SetBool("Idle", true);
            }
        }
    }
}
