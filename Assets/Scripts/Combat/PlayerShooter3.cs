using UnityEngine;

// 주어진 Gun 오브젝트를 쏘거나 재장전
// 알맞은 애니메이션을 재생하고 IK를 사용해 캐릭터 양손이 총에 위치하도록 조정
public class PlayerShooter3 : MonoBehaviour
{
    //바주카 스크립트(샷건,기본총 동일Gun사용):동일 레이케스트 타겟 방식 사용 + Gun_Bazooka
    public Gun_Bazooka gun; // 사용할 총(바주카)
    public Transform gunPivot; // 총 배치의 기준점

    private Animator playerAnimator; // 애니메이터 컴포넌트

    public float Timer = 0f;
    public bool isMoving;

    public CombatActionUI combatactionui;

    public LayerMask enemyLayer;
    public float attackRange;
    public bool enemyInvisionRadius;

    private void Start()
    {
        //playerinput,playeranimator 참조 받아오기
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        //슈터와 총이 항상 함께 있도록
        //슈터가 활성화되면,총도 활성화되도록 한다.
        gun.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        //슈터가 사라지면 총도 비활성화되도록 한다
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
        // 입력을 감지하고 총 발사하거나 재장전

        //총을 발사한다는 입력을 감지했을 때
        //총 발사 스크립트를 실행. (gun 스크립트의 Fire)
        if (Input.GetMouseButtonDown(0))
        {
            //총을 발사할 수 있는지 체크하는 함수 실행 ( gun 스크립트의 Fire)
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

        //총을 재장전한다는 입력을 감지했을 때
        if (Input.GetKey(KeyCode.R))
        {
            //재장전
            if (gun.Reload() == true)//이 타이밍에 이미 리로드 함수는 실행됐다.
            {
                //playerAnimator.SetBool("ReloadBazooka",true);
            }
        }
        //UpdateUI();
        //재장전
        //재장전에 성공했을 떄 장전 애니메이션을 실행
        if (Timer > 5f)
        {
            Debug.Log("Bazooka mode off,마우스를 뗀 이후로 5초이상 지난시점에 대전모드off");
            playerAnimator.SetBool("BazookaActive", false);
        }
    }
}