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

        //hit의 물체가 IDamagable을 갖고 있으면 어떤 정보가 저장될 것이고,
        //갖고 있지 않다면 target의 정보는 null이 될 것이다.

        if (other != null)
        {
            Debug.Log("IceStrike Freezing Target>>" + other.transform.name);
            //부딪힌 물체가 데미지를 입을 수 있는 대상일 때
            //IDamagable을 갖고 있는 물체는 무조건 OnDamage 함수를 갖고 있다.

            //맞은 데미지: 총의 대미지
            //맞은 지점: 총알이 다은 지점
            //맞은 회전값: 부딪힌 장소의 회전값 - hit.normal
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
        target.IsPaused = true;//내부적으로 Update문에서 모든 동작 정지
        Debug.Log("Object frozen for 3 seconds." + target.transform.name);

        yield return new WaitForSeconds(iceTime);

        target.DeleteStateListItem("Freeze");
        target.IsPaused = false; //Update문에서 상태값 변경에 따라 다시 모든 동작 재개
        Debug.Log("Object frozen thawed." + target.transform.name);

        Destroy(gameObject);
    }
    private IEnumerator FreezeObject(CharacterNavigatorScript target)
    {
        target.AddStateList("Freeze");
        target.IsPaused = true;//내부적으로 Update문에서 모든 동작 정지
        Debug.Log("Object frozen for 3 seconds." + target.transform.name);

        yield return new WaitForSeconds(iceTime);

        target.DeleteStateListItem("Freeze");
        target.IsPaused = false; //Update문에서 상태값 변경에 따라 다시 모든 동작 재개
        Debug.Log("Object frozen thawed." + target.transform.name);

        Destroy(gameObject);
    }
    private IEnumerator FreezeObject(Boss target)
    {
        target.AddStateList("Freeze");
        target.IsPaused = true;//내부적으로 Update문에서 모든 동작 정지
        Debug.Log("Object frozen for 3 seconds." + target.transform.name);

        yield return new WaitForSeconds(iceTime);

        target.DeleteStateListItem("Freeze");
        target.IsPaused = false; //Update문에서 상태값 변경에 따라 다시 모든 동작 재개
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
            Debug.Log("얼음 생성된지 8초이상 지난 경우라면 삭제처리");
            Destroy(gameObject);
        }
    }
}
