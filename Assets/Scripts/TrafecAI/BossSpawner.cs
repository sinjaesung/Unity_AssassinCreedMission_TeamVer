using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSpawner : MonoBehaviour
{
    public GameObject AiPrefab;
   

    private void Awake()
    {

    }
    private void Start()
    {
    }
    public void SpawnTarget()
    {
        GameObject obj = Instantiate(AiPrefab,transform.position,Quaternion.identity);
    }
}
