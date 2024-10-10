using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStrike : MonoBehaviour
{
    [SerializeField] private float iceTime = 3;
    [SerializeField] private float DestroyTime = 8;
    [SerializeField] private float StartTIme;
    private void Awake()
    {
        StartTIme = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        //IDamageable target = other.GetComponent<IDamageable>();

        //hit�� ��ü�� IDamagable�� ���� ������ � ������ ����� ���̰�,
        //���� ���� �ʴٸ� target�� ������ null�� �� ���̴�.

        if (other != null)
        {
            Debug.Log("IceStrike Freezing Target>>" + other.transform.name);
            //�ε��� ��ü�� �������� ���� �� �ִ� ����� ��
            //IDamagable�� ���� �ִ� ��ü�� ������ OnDamage �Լ��� ���� �ִ�.

            //���� ������: ���� �����
            //���� ����: �Ѿ��� ���� ����
            //���� ȸ����: �ε��� ����� ȸ���� - hit.normal
            PoliceMan policeman = other.GetComponent<PoliceMan>();
            CharacterNavigatorScript character = other.GetComponent<CharacterNavigatorScript>();
            Boss boss = other.GetComponent<Boss>();

            if (policeman != null)
            {
                StartCoroutine(FreezeObject(policeman));
            }
            if (character != null)
            {
                StartCoroutine(FreezeObject(character));
            }
            if (boss != null)
            {
                StartCoroutine(FreezeObject(boss));
            }
        }
    }

    //Coroutine to freeze the object for 3 seconds
    private IEnumerator FreezeObject(PoliceMan target)
    {
        target.AddStateList("Freeze");
        target.IsPaused = true;//���������� Update������ ��� ���� ����
        Debug.Log("Object frozen for 3 seconds." + target.transform.name);

        yield return new WaitForSeconds(iceTime);

        target.DeleteStateListItem("Freeze");
        target.IsPaused = false; //Update������ ���°� ���濡 ���� �ٽ� ��� ���� �簳
        Debug.Log("Object frozen thawed." + target.transform.name);

        Destroy(gameObject);
    }
    private IEnumerator FreezeObject(CharacterNavigatorScript target)
    {
        target.AddStateList("Freeze");
        target.IsPaused = true;//���������� Update������ ��� ���� ����
        Debug.Log("Object frozen for 3 seconds." + target.transform.name);

        yield return new WaitForSeconds(iceTime);

        target.DeleteStateListItem("Freeze");
        target.IsPaused = false; //Update������ ���°� ���濡 ���� �ٽ� ��� ���� �簳
        Debug.Log("Object frozen thawed." + target.transform.name);

        Destroy(gameObject);
    }
    private IEnumerator FreezeObject(Boss target)
    {
        target.AddStateList("Freeze");
        target.IsPaused = true;//���������� Update������ ��� ���� ����
        Debug.Log("Object frozen for 3 seconds." + target.transform.name);

        yield return new WaitForSeconds(iceTime);

        target.DeleteStateListItem("Freeze");
        target.IsPaused = false; //Update������ ���°� ���濡 ���� �ٽ� ��� ���� �簳
        Debug.Log("Object frozen thawed." + target.transform.name);

        Destroy(gameObject);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void Update()
    {
       if (Time.time - StartTIme >= DestroyTime)
        {
            Debug.Log("���� �������� 8���̻� ���� ����� ����ó��");
            Destroy(gameObject);
        }
    }
}
