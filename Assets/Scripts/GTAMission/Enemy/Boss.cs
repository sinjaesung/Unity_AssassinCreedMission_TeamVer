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

    public ParticleSystem hitEffect; // �ǰݽ� ����� ��ƼŬ ȿ��
    public AudioClip deathSound; // ����� ����� �Ҹ�
    public AudioClip hitSound; // �ǰݽ� ����� �Ҹ�

    private AudioSource enemyAudioPlayer; // ����� �ҽ� ������Ʈ

    public bool IsDied = false;

    private Renderer enemyRenderer; // ������ ������Ʈ
    public Color OriginalColor;
    public bool IsPaused = false;//��������� �Ǵ� ���ϱ�
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
                    //�̼�4
                    missions.Mission4 = true;
                }

                Object.Destroy(gameObject, 8.0f);

                //animation
                animator.SetBool("Died", true);
                //animator.SetBool("Shooting", false);
                gameObject.GetComponent<CapsuleCollider>().enabled = false;

                //����Ʈ ����
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
            Debug.Log("BOSS>>������ ��ȿ");
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
        }
        else
        {
            Debug.Log("BOSS>>������ ��ȿ");
        }
    }
}