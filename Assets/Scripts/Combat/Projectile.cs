using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject particleInstance;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�ε��� ��� ������ �ݶ��̴� ��ü�� ���ؼ� �ش� ��ġ���� ����Ʈ �߻�>>" + other.transform.name + "," + transform.position);

        GameObject ParticleObject = Instantiate(particleInstance, transform.position, Quaternion.identity);
        // Step 2: Start the coroutine to destroy the particle system after playback
    }
}
