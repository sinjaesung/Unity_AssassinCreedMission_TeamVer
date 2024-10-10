using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject particleInstance;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("PlayerExplodes"))
        {
            Debug.Log("�ε��� ��� ������ �ݶ��̴� ��ü�� ���ؼ� �ش� ��ġ���� ����Ʈ �߻�>>" + other.transform.name + "," + transform.position);
            Debug.Log("ExplodesSpawn Collide" + transform.position);
            GameObject ParticleObject = Instantiate(particleInstance, transform.position, Quaternion.identity);
            // Step 2: Start the coroutine to destroy the particle system after playback
        }
    }
    public void ExplodesSpawnDirect(Vector3 spawnpos)
    {
        Debug.Log("ExplodesSpawn Direct" + spawnpos);
        GameObject ParticleObject = Instantiate(particleInstance, spawnpos, Quaternion.identity);
        // Step 2: Start the coroutine to destroy the particle system after playback
    }
}
