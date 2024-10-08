using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject particleInstance;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("부딪힌 모든 물리형 콜라이더 객체에 대해서 해당 위치에서 이펙트 발생>>" + other.transform.name + "," + transform.position);

        GameObject ParticleObject = Instantiate(particleInstance, transform.position, Quaternion.identity);
        // Step 2: Start the coroutine to destroy the particle system after playback
    }
}
