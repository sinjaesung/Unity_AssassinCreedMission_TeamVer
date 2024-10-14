using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] float originHealth = 120f;
    [SerializeField] float bossHealth = 120f;
    public Animator animator;
    public Missions missions;

    public ParticleSystem hitEffect; // 피격시 재생할 파티클 효과
    public AudioClip deathSound; // 사망시 재생할 소리
    public AudioClip hitSound; // 피격시 재생할 소리

    private AudioSource enemyAudioPlayer; // 오디오 소스 컴포넌트

    public bool IsDied = false;

    private Renderer enemyRenderer; // 렌더러 컴포넌트
    public Color OriginalColor;
    public bool IsPaused = false;//얼려졌을때 또는 스턴기
    public List<string> StateList = new List<string>();

    public Image healthbar;

    public Timer hitTimer;

    private void Awake()
    {
        missions = FindObjectOfType<Missions>();
        bossHealth = originHealth;
        healthbar.fillAmount = originHealth / bossHealth;

        OriginalColor = Color.white;
        enemyAudioPlayer = GetComponent<AudioSource>();
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
        if (bossHealth < 120)
        {
            //animation
            //animator.SetBool("Shooting", true);
        }
        if (bossHealth <= 0)
        {
            if (!IsDied)
            {
                //pass mission
                if (missions.Mission1 == true && missions.Mission2 == true && missions.Mission3 == true && missions.Mission4 == false)
                {
                    //미션4
                    missions.Mission4 = true;
                }

                Object.Destroy(gameObject, 8.0f);

                //animation
                animator.SetBool("Died", true);
                //animator.SetBool("Shooting", false);
                gameObject.GetComponent<CapsuleCollider>().enabled = false;

                //이펙트 실행
                enemyAudioPlayer.PlayOneShot(deathSound);
            }
            IsDied = true;
        }
    }

    public bool IsValid()
    {
        if (missions.Mission1 == true && missions.Mission2 == true && missions.Mission3 == true && missions.Mission4 == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void characterHitDamage(float takeDamage)
    {
        if (IsValid())
        {
            bossHealth -= takeDamage;
            healthbar.fillAmount = originHealth / bossHealth;
            //animator.SetBool("Shooting", true);
        }
        else
        {
            Debug.Log("BOSS>>데미지 무효");
        }
    }
    public void characterHitDamage(float takeDamage,Vector3 hitPoint,Vector3 hitNormal)
    {
        if (IsValid())
        {
            bossHealth -= takeDamage;
            healthbar.fillAmount = originHealth / bossHealth;
            //animator.SetBool("Shooting", true);

            if(bossHealth > 0)
            {
                //죽지 않았을 떄에만 피격 효과 발동 => 효과음,피가 튀는 이펙트 효과
                enemyAudioPlayer.PlayOneShot(hitSound);

                //이펙트의 위치 : 맞은 위치
                //이펙트가 튀는 방향: 맞은 방향
                hitEffect.transform.position = hitPoint;
                //바라보는 방향을 일치시킨다.
                hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
                //위치 선정 완료 후 재생
                hitEffect.Play();
            }
        }
        else
        {
            Debug.Log("BOSS>>데미지 무효");
        }
    }
}