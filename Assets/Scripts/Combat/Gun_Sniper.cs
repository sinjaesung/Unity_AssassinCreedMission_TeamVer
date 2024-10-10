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
    private Crosshair imageAim; //default/aim ��忡 ���� Aim �̹��� Ȱ��,��Ȱ��

    public float snipingFOV = 20f;
    // How fast the camera transitions to and from sniping mode
    public float zoomSpeed = 10f;
    public bool isSniping = false;

    public float normalFOV;

    private void Start()
    {
        // �� ���� �ʱ�ȭ

        //���� źâ�� ���� ä���.
        //���� ���¸� '�غ� ����'�� �����.
        //���� �� ������ 0���� �ʱ�ȭ.

        //���� źâ�� ���� ä���.
        magAmmo = magCapacity;
        //���� ���¸� �غ���·� �����.
        state = State.Ready;
        //���� �� �������� 0���� �ʱ�ȭ.
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
        Debug.Log("Gun_Sniper ��");

        //���� ��� -> ���� �´´� -> ���� �¾Ҵ��� üũ -> ���ʹ��� ��� �����

        //���� ������ ��Ҵ��� ����ϴ� ��� -> �����ɽ�Ʈ(���� ����)
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


                //hit�� ��ü�� IDamagable�� ���� ������ � ������ ����� ���̰�,
                //���� ���� �ʴٸ� target�� ������ null�� �� ���̴�.
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

                //���� ������ ��󿡰� �Ѿ��� ��ҵ�, ���� �������� ���� ��󿡰� ��ҵ�(��,��Ÿ��)
                //������ ���� ��ġ���� �����Ѵ�.
                hitPosition = hitInfo.point;
              }
              else
              {
                  //If nothing is hit,calculate the point in world space at maxDistance along the ray
                  Vector3 pointAtMaxDistance = ray.GetPoint(fireDistance);
                  Debug.Log("No object hit. Point at max distance : " + pointAtMaxDistance);

                hitPosition = pointAtMaxDistance;
              }
            //�� �߻� ����Ʈ
            StartCoroutine(ShotEffect(hitPosition));
            SpawnExplodesEffect(hitPosition);
            HitPos = hitPosition;

            //źâ �Ҹ�
            //���� źâ ���� - 1
            magAmmo--;
            //���� źâ�� 0���� -> ���� ���¸� 'źâ ��'���� �����.
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