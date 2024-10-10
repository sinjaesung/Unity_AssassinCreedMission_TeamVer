using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float DestroyTime;

    [SerializeField] private float StartTIme;
    [SerializeField] public bool isUpdateDeleted = false;
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
            Debug.Log("ExplosionDamage Damaged Target>>" + other.transform.name);
            //�ε��� ��ü�� �������� ���� �� �ִ� ����� ��
            //IDamagable�� ���� �ִ� ��ü�� ������ OnDamage �Լ��� ���� �ִ�.

            //���� ������: ���� �����
            //���� ����: �Ѿ��� ���� ����
            //���� ȸ����: �ε��� ����� ȸ���� - hit.normal
            //target.OnDamage(damage);
            KnightAI knightAI = other.GetComponent<KnightAI>();
            KnightAI2 knightAI2 = other.GetComponent<KnightAI2>();
            PoliceMan policeman = other.GetComponent<PoliceMan>();
            CharacterNavigatorScript character = other.GetComponent<CharacterNavigatorScript>();
            Boss boss = other.GetComponent<Boss>();

            if (knightAI != null)
            {
                knightAI.TakeDamage(damage);
            }
            /*f(knightAI2 != null)
            {
                knightAI2.TakeDamage(giveDamage);
            }*/
            if (character != null)
            {
                character.characterHitDamage(damage);
            }
            if (policeman != null)
            {
                policeman.characterHitDamage(damage);
            }
            if (boss != null)
            {
                boss.characterHitDamage(damage);
            }
        }
    }

    private void Update()
    {
        if (isUpdateDeleted)
        {
            if (Time.time - StartTIme >= DestroyTime)
            {
                Debug.Log("������Ʈ �������� n���̻� ���� ����� ����ó��");
                Destroy(gameObject);
            }
        }
    }
}
