using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun_Sniper : Gun
{
    public LayerMask targetLayer;
    public GameObject explodePrefab; //Assign the ParticleSystem prefab in the inspector

    [Header("Aim UI")]
    [SerializeField]
    private Crosshair imageAim; //default/aim 모드에 따라 Aim 이미지 활성,비활성

    public float snipingFOV = 20f;
    // How fast the camera transitions to and from sniping mode
    public float zoomSpeed = 10f;
    public bool isSniping = false;

    public float normalFOV;

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

        normalFOV = maincamera.fieldOfView;
    }
    void Update()
    {
        if (magAmmo <= 0)
        {
            combatactionui.GunReloadAction.SetActive(true);
            state = State.Empty;
        }
        else
        {
            combatactionui.GunReloadAction.SetActive(false);
        }

        // Check if the right mouse button is being held down
        if (Input.GetMouseButton(1))
        {
            isSniping = true;
            combatactionui.AllCombatClear();
            combatactionui.SnipermodeAction.SetActive(false);
        }
        else
        {
            isSniping = false;
            combatactionui.AllCombatClear();
            combatactionui.SnipermodeAction.SetActive(true);
        }

        // Smoothly transition between normal FOV and sniping FOV
        if (isSniping)
        {
            // Zoom in (sniping mode)
            maincamera.fieldOfView = Mathf.Lerp(maincamera.fieldOfView, snipingFOV, zoomSpeed * Time.deltaTime);
            maincamera.GetComponent<MainCameraController>().framingBalance = new Vector3(0, 2, 0);
            maincamera.GetComponent<MainCameraController>().gap = 0;
            imageAim.gameObject.SetActive(true);
        }
        else
        {
            // Zoom out (normal mode)
            maincamera.fieldOfView = Mathf.Lerp(maincamera.fieldOfView, normalFOV, zoomSpeed * Time.deltaTime);
            maincamera.GetComponent<MainCameraController>().framingBalance = new Vector3(0, 1, 0);
            maincamera.GetComponent<MainCameraController>().gap = 3;
            imageAim.gameObject.SetActive(false);
        }
    }
    protected override void Shot()
    {
        Debug.Log("Gun_Sniper 탕");

        //총을 쏜다 -> 무언가 맞는다 -> 누가 맞았는지 체크 -> 에너미일 경우 대미지

        //총이 무엇을 쏘았는지 계산하는 방법 -> 레이케스트(직선 광선)
        Vector3 hitPosition = Vector3.zero;

        if (maincamera != null)
        {
            //Create a ray from the center of the screen (0.5,0.5, in Viewport coordinates)
              Ray ray = maincamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

              //Store hit information
              RaycastHit hitInfo;

              //Perform the raycast the check for a hit int the scene
              if(Physics.Raycast(ray,out hitInfo, fireDistance, targetLayer))
              {
                //Log the world coordinates where the ray hits an object
                Debug.Log($"Sniper World coordinates at the center of the screen and object name {hitInfo.point},{hitInfo.transform.name}");


                //hit의 물체가 IDamagable을 갖고 있으면 어떤 정보가 저장될 것이고,
                //갖고 있지 않다면 target의 정보는 null이 될 것이다.
                KnightAI knightAI = hitInfo.transform.GetComponent<KnightAI>();
                KnightAI2 knightAI2 = hitInfo.transform.GetComponent<KnightAI2>();
                PoliceMan policeman = hitInfo.transform.GetComponent<PoliceMan>();
                CharacterNavigatorScript character = hitInfo.transform.GetComponent<CharacterNavigatorScript>();
                Boss boss = hitInfo.transform.GetComponent<Boss>();

                if (knightAI != null)
                {
                    knightAI.TakeDamage(damage, hitInfo.point, hitInfo.normal);
                }
                /*if (knightAI2 != null)
                {
                    knightAI2.TakeDamage(damage, hit.point, hit.normal);
                }*/
                if (character != null)
                {
                    character.characterHitDamage(damage, hitInfo.point, hitInfo.normal);
                }
                if (policeman != null)
                {
                    policeman.characterHitDamage(damage, hitInfo.point, hitInfo.normal);
                }
                if (boss != null)
                {
                    boss.characterHitDamage(damage, hitInfo.point, hitInfo.normal);
                }

                //공격 가능한 대상에게 총알이 닿았든, 공격 가능하지 않은 대상에게 닿았든(벽,울타리)
                //무조건 닿은 위치값을 저장한다.
                hitPosition = hitInfo.point;
              }
              else
              {
                  //If nothing is hit,calculate the point in world space at maxDistance along the ray
                  Vector3 pointAtMaxDistance = ray.GetPoint(fireDistance);
                  Debug.Log("No object hit. Point at max distance : " + pointAtMaxDistance);

                hitPosition = pointAtMaxDistance;
              }
            //총 발사 이펙트
            StartCoroutine(ShotEffect(hitPosition));
            SpawnExplodesEffect(hitPosition);
            HitPos = hitPosition;

            //탄창 소모
            //현재 탄창 갯수 - 1
            magAmmo--;
            //만약 탄창이 0개면 -> 총의 상태를 '탄창 빔'으로 만든다.
            if (magAmmo <= 0)
            {
                state = State.Empty;
            }
        }
        //UIManager.instance.UpdateAmmoText(magAmmo, ammoRemain);
    }
    public void SpawnExplodesEffect(Vector3 hitPosition)
    {
        // Step 1: Instantiate the particle system at the specified position
        GameObject particleInstance = Instantiate(explodePrefab, hitPosition, Quaternion.identity);
        // Step 2: Start the coroutine to destroy the particle system after playback
        StartCoroutine(DestroyParticleWhenFinished(particleInstance));
    }
    // Coroutine to check if the particle system is finished and destroy it
    private System.Collections.IEnumerator DestroyParticleWhenFinished(GameObject particleObject)
    {
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();

        // Wait until the particle system has completely stopped
        while (particleSystem != null && particleSystem.isPlaying)
        {
            yield return null; // Wait for the next frame
        }

        // Destroy the particle system object after playback ends
        Destroy(particleObject);
    }
}