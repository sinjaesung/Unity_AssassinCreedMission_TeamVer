/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandShooter2 : MonoBehaviour
{
    public Wand wand; //사용할 완드(바주카)
    public Transform gunPivot; // 총 배치의 기준점

    private Animator playerAnimator; // 애니메이터 컴포넌트

    public bool isMoving;
    public float Timer = 0f;

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
        wand.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        //슈터가 사라지면 총도 비활성화되도록 한다
        wand.gameObject.SetActive(false);
    }

    private void Update()
    {
        enemyInvisionRadius = Physics.CheckSphere(transform.position, attackRange, enemyLayer);
        combatactionui.GunReloadAction.SetActive(false);

        if (enemyInvisionRadius)
        {
            combatactionui.AllCombatClear();
            combatactionui.FireStrikeAttackAction.SetActive(true);
        }
        else
        {
            combatactionui.AllCombatClear();
            combatactionui.FireStrikeAttackAction.SetActive(false);
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
            wand.Fire();
            playerAnimator.SetBool("BazookaActive", true);
            playerAnimator.SetBool("BazookaShooting", true);
            Timer = 0;
        }
        else if (!Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetBool("BazookaShooting", false);
            Timer += Time.deltaTime;
        }

        if (Timer > 5f)
        {
            Debug.Log("Bazooka mode off,마우스를 뗀 이후로 5초이상 지난시점에 대전모드off");
            playerAnimator.SetBool("BazookaActive", false);
        }
    }
}
*/