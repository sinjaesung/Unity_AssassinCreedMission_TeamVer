/*using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

// 총을 구현한다
public class Gun : MonoBehaviour
{
    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum State
    {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    public State state { get; protected set; } // 현재 총의 상태

    public Transform fireTransform; // 총알이 발사될 위치

    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem shellEjectEffect; // 탄피 배출 효과

    private LineRenderer bulletLineRenderer; // 총알 궤적을 그리기 위한 렌더러(var)

    protected AudioSource gunAudioPlayer; // 총 소리 재생기
    [SerializeField] public AudioClip shotClip; // 발사 소리(var)
    public AudioClip reloadClip; // 재장전 소리

    [SerializeField] public float damage = 25; // 공격력(var)
    [SerializeField] protected float fireDistance = 50f; // 사정거리(var)

    [SerializeField] public int ammoRemain = 100; // 남은 전체 탄약(var)
    [SerializeField] public int magCapacity = 25; // 탄창 용량(var)
    [SerializeField] public int magAmmo; // 현재 탄창에 남아있는 탄약(var)


    [SerializeField] public float timeBetFire = 0.12f; // 총알 발사 간격(var)
    public float reloadTime = 1.8f; // 재장전 소요 시간
    public float lastFireTime; // 총을 마지막으로 발사한 시점

    public Vector3 HitPos;

    public Animator PlayerAnimator;

    public Camera maincamera;

    public CombatActionUI combatactionui;
    private void Awake()
    {
        maincamera = Camera.main;
        PlayerAnimator = GetComponentInParent<Animator>();

        // 사용할 컴포넌트들의 참조를 가져오기
        bulletLineRenderer = GetComponent<LineRenderer>();
        gunAudioPlayer = GetComponent<AudioSource>();

        //컴포넌트 설정 누락을 방지하기 위해
        //라인렌더러 설정을 이곳에서 진행.
        //라인렌더러 점 갯수 설정 이후
        bulletLineRenderer.positionCount = 2;

        //라인렌더를 잠시 비활성화
        bulletLineRenderer.enabled = false;
    }
    private void Start()
    {
        // 총 상태 초기화

        //현재 탄창을 가득 채운다.
        //총의 상태를 '준비 상태'로 만든다.
        //총을 쏜 시점을 0으로 초기화.

        //현재 탄창을 가득 채운다.
        magAmmo = magCapacity;
        //총의 상태를 준비상태로 만든다.
        state = State.Ready;
        //총을 쏜 시점으로 0으로 초기화.
        lastFireTime = 0;
    }
    private void OnEnable()
    {
        
    }

    // 발사 시도
    public void Fire()
    {
        //상태 체크: 현재 발사를 할 수 있는 상태인가?
        //총알 발사 간격만큼 이상의 시간이 흘렀느냐
        //둘다 만족할 경우에 발사 처리를 진행한다.

        //발사 시간 체크 : 마지막 발사 시간 + 발사 간격을 한 시간이 현재 시간보다 작다
        //시간이 더 흘렀다는 뜻이니 발사가 가능하도록.
        Debug.Log($"총 쏘기 시도{Time.time} >= {lastFireTime} + {timeBetFire}({lastFireTime + timeBetFire}>>");

        if (state == State.Ready && Time.time >= lastFireTime + timeBetFire)
        {
            Debug.Log("총 쏘기 가능>>");
            Shot();
            //총을 쏘았으니 마지막 총을 쏜 시간을 현재로 갱신한다.
            lastFireTime = Time.time;
        }
        else
        {
            Debug.Log("총 쿨타임");
        }
    }
    private void Update()
    {
        if(magAmmo <= 0)
        {
            combatactionui.GunReloadAction.SetActive(true);
            state = State.Empty;
        }
        else
        {
            combatactionui.GunReloadAction.SetActive(false);
        }
    }

    // 실제 발사 처리
    protected virtual void Shot()
    {
        Debug.Log("탕");

        //총을 쏜다 -> 무언가 맞는다 -> 누가 맞았는지 체크 -> 에너미일 경우 대미지

        //총이 무엇을 쏘았는지 계산하는 방법 -> 레이케스트(직선 광선)

        RaycastHit hit;

        Vector3 hitPosition = Vector3.zero;

        //레이케스트 공식
        //Physics.Raycast(광선의 시작 지점,광선이 어디로 뻗어나갈지 방향,충돌 정보(hit로 고정),최대광선길이)
        //시작지점:총구, 방향:총(총구)이 바라보는 방향
        //out~ : out 뒤에 적은 변수에 어떤 값을 저장한다.

        //레이케스트 함수의 결과는 true 혹은 false로 도출된다.
        //true : 사정거리 내에서 뭔가에 부딪혔을 때 반환.
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            //광선이 무언가에 부딪혔을 때 사용

            //총이 공격 가능 대상을 가격했는지 체크
            //공격 가능 대상이란? : IDamagable을 참조하는 객체. => 해당 객체로부터 IDamagable 속성을 가져올 수 있다.
            //IDamageable target = hit.collider.GetComponent<IDamageable>();

            //hit의 물체가 IDamagable을 갖고 있으면 어떤 정보가 저장될 것이고,
            //갖고 있지 않다면 target의 정보는 null이 될 것이다.
            KnightAI knightAI = hit.transform.GetComponent<KnightAI>();
            KnightAI2 knightAI2 = hit.transform.GetComponent<KnightAI2>();
            PoliceMan policeman = hit.transform.GetComponent<PoliceMan>();
            CharacterNavigatorScript character = hit.transform.GetComponent<CharacterNavigatorScript>();
            Boss boss = hit.transform.GetComponent<Boss>();

            *//*if (target != null)
            {
                //부딪힌 물체가 데미지를 입을 수 있는 대상일 때
                //IDamagable을 갖고 있는 물체는 무조건 OnDamage 함수를 갖고 있다.

                //맞은 데미지: 총의 대미지
                //맞은 지점: 총알이 다은 지점
                //맞은 회전값: 부딪힌 장소의 회전값 - hit.normal
                target.OnDamage(damage, hit.point, hit.normal);
            }*//*
            if (knightAI != null)
            {
                knightAI.TakeDamage(damage, hit.point, hit.normal);
            }
            *//*if (knightAI2 != null)
            {
                knightAI2.TakeDamage(damage, hit.point, hit.normal);
            }*//*
            if (character != null)
            {
                character.characterHitDamage(damage, hit.point, hit.normal);
            }
            if (policeman != null)
            {
                policeman.characterHitDamage(damage, hit.point, hit.normal);
            }
            if (boss != null)
            {
                boss.characterHitDamage(damage, hit.point, hit.normal);
            }

            //공격 가능한 대상에게 총알이 닿았든, 공격 가능하지 않은 대상에게 닿았든(벽,울타리)
            //무조건 닿은 위치값을 저장한다.
            hitPosition = hit.point;
        }
        else
        {
            //레이케스트 상으로 사정거리 내에서 아무것도 부딪히지 않았을 떄 실행할 코드

            //충돌 위치: 총을 발사한 위치로부터 최대 사정거리 이후의 위치값
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        //물체에 맞았든,맞지 않았든 총을 발사하면 실행되는 행동을 아래에 기입

        //총 발사 이펙트
        StartCoroutine(ShotEffect(hitPosition));

        HitPos = hitPosition;

        //탄창 소모
        //현재 탄창 갯수 - 1
        magAmmo--;
        //만약 탄창이 0개면 -> 총의 상태를 '탄창 빔'으로 만든다.
        if (magAmmo <= 0)
        {
            combatactionui.GunReloadAction.SetActive(true);
            state = State.Empty;
        }

        //UIManager.instance.UpdateAmmoText(magAmmo, ammoRemain);
    }

    // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    protected IEnumerator ShotEffect(Vector3 hitPosition)
    {

        //총알 발사 플래시 이펙트
        muzzleFlashEffect.Play();
        //탄창 이펙트
        shellEjectEffect.Play();
        //총격 소리
        //총알 궤적 그려주기

        //총격 소리는 한 번만 재생한다.
        gunAudioPlayer.PlayOneShot(shotClip);

        //총알 궤적 그려주기

        //선을 긋기 위해선 시작점,끝점을 알아야 한다.
        //시작점 : 총구
        //끝나는 점 : 부딪힌 위치
        //SetPosition(몇 번째 점인지, 값)

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);

        //시작점,끝점을 지정한 이후에
        //선이 보이도록 활성화한다,.
        bulletLineRenderer.enabled = true;

        //0.03초만 선이 보이고, 이후 바로 선이 사라진다.
        yield return new WaitForSeconds(1.2f);

        bulletLineRenderer.enabled = false;
    }// 재장전 시도
    public bool Reload()
    {

        //재장전이 가능하면 true,불가능하면 false

        //재장전 가능 타이밍x, 재장전 중일때, 탄창이 이미 가득 차 있을때, 전체 총알 갯수가 0개가 됐을 때
        //그 외의 경우에만 재장전이 가능하다.

        if (state == State.Reloading || magAmmo >= magCapacity || ammoRemain <= 0)
        {
            return false;
        }

        //아래로 내려올 경우는 장전이 가능한 경우.
        //장전 진행
        //장전이 가능하다고 상태값 반환
        StartCoroutine(ReloadRoutine());
        return true;
    }
    // 실제 재장전 처리를 진행
    protected virtual IEnumerator ReloadRoutine()
    {

        //코루틴 사용 이유: 재장전 하는 동안의 시간 딜레이를 위해서 사용.
        Debug.Log("재장전");

        //총의 상태를 '재장전 중'으로 바꾼다. => 중복 재장전 방지
        state = State.Reloading;
        PlayerAnimator.SetBool("ReloadRifle", true);

        //재장전 효과음 재생
        gunAudioPlayer.PlayOneShot(reloadClip);

        //재장전 소요시간만큼 시간 딜레이
        yield return new WaitForSeconds(reloadTime);

        //시간이 끝나면
        //탄창에 총알을 채운다.

        //채우는 총알 갯수: 현재 탄창이 최대 탄창 갯수가 되려면 몇개가 더 필요한지 계산
        int ammoToFill = magCapacity - magAmmo;

        //필요한 만큼만 채운다, 만약 총 남은 총알 갯수가 채워야 할 총알 갯수보다 적을 때
        //남은 총알의 갯수 만큼만 총을 재장전한다.
        if (ammoRemain < ammoToFill)
        {
            //채워야 할 탄알 갯수를 남은 탄알 갯수와 일치하도록 수정한다.
            ammoToFill = ammoRemain;
        }
        PlayerAnimator.SetBool("ReloadRifle", false);

        //현재 탄알에 탄알 채우기
        magAmmo += ammoToFill;
        //전체 탄알에서 채운 탄알만큼 갯수 감소
        ammoRemain -= ammoToFill;

        //모든 작업이 끝나면 총의 상태를 다시 '준비' 상태로 전환.
        state = State.Ready;

       // UIManager.instance.UpdateAmmoText(magAmmo, ammoRemain);
        yield return null;
    }
}*/