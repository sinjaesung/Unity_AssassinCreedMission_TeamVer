/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Bazooka : Gun
{
    public GameObject explodePrefab; //Assign the ParticleSystem prefab in the inspector

    protected override void Shot()
    {
        Debug.Log("Gun_Bazooka ��");

        //���� ��� -> ���� �´´� -> ���� �¾Ҵ��� üũ -> ���ʹ��� ��� �����

        //���� ������ ��Ҵ��� ����ϴ� ��� -> �����ɽ�Ʈ(���� ����)

        RaycastHit hit;

        Vector3 hitPosition = Vector3.zero;

        //�����ɽ�Ʈ ����
        //Physics.Raycast(������ ���� ����,������ ���� ������� ����,�浹 ����(hit�� ����),�ִ뱤������)
        //��������:�ѱ�, ����:��(�ѱ�)�� �ٶ󺸴� ����
        //out~ : out �ڿ� ���� ������ � ���� �����Ѵ�.

        //�����ɽ�Ʈ �Լ��� ����� true Ȥ�� false�� ����ȴ�.
        //true : �����Ÿ� ������ ������ �ε����� �� ��ȯ.
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            //������ ���𰡿� �ε����� �� ���

            //���� ���� ���� ����� �����ߴ��� üũ
            //���� ���� ����̶�? : IDamagable�� �����ϴ� ��ü. => �ش� ��ü�κ��� IDamagable �Ӽ��� ������ �� �ִ�.
            //hit�� ��ü�� IDamagable�� ���� ������ � ������ ����� ���̰�,
            //���� ���� �ʴٸ� target�� ������ null�� �� ���̴�.
            KnightAI knightAI = hit.transform.GetComponent<KnightAI>();
            KnightAI2 knightAI2 = hit.transform.GetComponent<KnightAI2>();
            PoliceMan policeman = hit.transform.GetComponent<PoliceMan>();
            CharacterNavigatorScript character = hit.transform.GetComponent<CharacterNavigatorScript>();
            Boss boss = hit.transform.GetComponent<Boss>();

            *//*if (target != null)
            {
                //�ε��� ��ü�� �������� ���� �� �ִ� ����� ��
                //IDamagable�� ���� �ִ� ��ü�� ������ OnDamage �Լ��� ���� �ִ�.

                //���� ������: ���� �����
                //���� ����: �Ѿ��� ���� ����
                //���� ȸ����: �ε��� ����� ȸ���� - hit.normal
                target.OnDamage(damage, hit.point, hit.normal);
            }*//*
            //hit�� ��ü�� IDamagable�� ���� ������ � ������ ����� ���̰�,
            //���� ���� �ʴٸ� target�� ������ null�� �� ���̴�.

            if (knightAI != null || knightAI != null || character != null ||
                policeman != null || boss != null)
            {
                //�ε��� ��ü�� �������� ���� �� �ִ� ����� ��
                //IDamagable�� ���� �ִ� ��ü�� ������ OnDamage �Լ��� ���� �ִ�.

                //���� ������: ���� �����
                //���� ����: �Ѿ��� ���� ����
                //���� ȸ����: �ε��� ����� ȸ���� - hit.normal
                // target.OnDamage(damage, hit.point, hit.normal);
            }

            //���� ������ ��󿡰� �Ѿ��� ��ҵ�, ���� �������� ���� ��󿡰� ��ҵ�(��,��Ÿ��)
            //������ ���� ��ġ���� �����Ѵ�.
            hitPosition = hit.point;
        }
        else
        {
            //�����ɽ�Ʈ ������ �����Ÿ� ������ �ƹ��͵� �ε����� �ʾ��� �� ������ �ڵ�

            //�浹 ��ġ: ���� �߻��� ��ġ�κ��� �ִ� �����Ÿ� ������ ��ġ��
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        //��ü�� �¾ҵ�,���� �ʾҵ� ���� �߻��ϸ� ����Ǵ� �ൿ�� �Ʒ��� ����

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
    protected override IEnumerator ReloadRoutine()
    {

        //�ڷ�ƾ ��� ����: ������ �ϴ� ������ �ð� �����̸� ���ؼ� ���.
        Debug.Log("������");

        //���� ���¸� '������ ��'���� �ٲ۴�. => �ߺ� ������ ����
        state = State.Reloading;
        PlayerAnimator.SetBool("ReloadBazooka", true);
        //������ ȿ���� ���
        gunAudioPlayer.PlayOneShot(reloadClip);

        //������ �ҿ�ð���ŭ �ð� ������
        yield return new WaitForSeconds(reloadTime);

        //�ð��� ������
        //źâ�� �Ѿ��� ä���.

        //ä��� �Ѿ� ����: ���� źâ�� �ִ� źâ ������ �Ƿ��� ��� �� �ʿ����� ���
        int ammoToFill = magCapacity - magAmmo;

        //�ʿ��� ��ŭ�� ä���, ���� �� ���� �Ѿ� ������ ä���� �� �Ѿ� �������� ���� ��
        //���� �Ѿ��� ���� ��ŭ�� ���� �������Ѵ�.
        if (ammoRemain < ammoToFill)
        {
            //ä���� �� ź�� ������ ���� ź�� ������ ��ġ�ϵ��� �����Ѵ�.
            ammoToFill = ammoRemain;
        }
        PlayerAnimator.SetBool("ReloadBazooka", false);

        //���� ź�˿� ź�� ä���
        magAmmo += ammoToFill;
        //��ü ź�˿��� ä�� ź�˸�ŭ ���� ����
        ammoRemain -= ammoToFill;

        //��� �۾��� ������ ���� ���¸� �ٽ� '�غ�' ���·� ��ȯ.
        state = State.Ready;

        //UIManager.instance.UpdateAmmoText(magAmmo, ammoRemain);
        yield return null;
    }
}*/