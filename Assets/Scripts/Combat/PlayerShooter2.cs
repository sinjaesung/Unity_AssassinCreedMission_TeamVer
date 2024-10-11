/*using UnityEngine;

// 주어진 Gun 오브젝트를 쏘거나 재장전
// 알맞은 애니메이션을 재생하고 IK를 사용해 캐릭터 양손이 총에 위치하도록 조정
public class PlayerShooter2 : MonoBehaviour
{
    //저격총
    public Gun_Sniper gun; // 사용할 총
    public Transform gunPivot; // 총 배치의 기준점

    private Animator playerAnimator; // 애니메이터 컴포넌트


    public bool isMoving;

    public float Timer = 0f;//모드 전환 관련

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
        
        //권총,샷건(라이플 공통모드)
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
            playerAnimator.SetBool("RifleActive", true);
            playerAnimator.SetBool("Shooting", true);
            //총을 발사할 수 있는지 체크하는 함수 실행 ( gun 스크립트의 Fire)
            Debug.Log("RifleActive Mode On:마우스왼쪽클릭down시마다 Timer=0되며 대전모드On");
            gun.Fire();
            Timer = 0f;
        }
        else if (!Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetBool("Shooting", false);
            Timer += Time.deltaTime;
        }


        //총을 재장전한다는 입력을 감지했을 때
        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("건 리로드");
            //재장전
            if (gun.Reload() == true)//이 타이밍에 이미 리로드 함수는 실행됐다.
            {
                // playerAnimator.SetTrigger("Reload");
            }
        }
        //UpdateUI();
        //재장전
        //재장전에 성공했을 떄 장전 애니메이션을 실행
        if (Timer > 5f)
        {
            Debug.Log("RifleActive Mode Off, 마우스를 뗀 이후로 5초이상지난 시점에 대전모드Off");
            playerAnimator.SetBool("RifleActive", false);
        }
    }
}*/