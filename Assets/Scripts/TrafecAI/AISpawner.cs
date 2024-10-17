using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpawner : MonoBehaviour
{
    public GameObject[] AiPrefab;
    public int AiToSpawn;

    private void Awake()
    {
    }
    private void Start()
    {
        //StartCoroutine(Spawn());
    }
    public void SpawnEnemies(int spawnCnt)
    {
        StartCoroutine(Spawn_Dynamic(spawnCnt));
    }
    IEnumerator Spawn()
    {
        int count = 0;
        while (count < AiToSpawn)
        {
            int randomIndex = Random.Range(0, AiPrefab.Length);

            GameObject obj = Instantiate(AiPrefab[randomIndex]);

            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();

            //obj.transform.position = SpawnPoint.position + new Vector3(0, 6f, 0);
            obj.GetComponent<NavMeshAgent>().Warp(child.position);

            yield return null;

            count++;
        }
    }
    IEnumerator Spawn_Dynamic(int spawnCnt)
    {
        int count = 0;
        while ((count < spawnCnt))
        {
            int randomIndex = Random.Range(0, AiPrefab.Length);

            GameObject obj = Instantiate(AiPrefab[randomIndex]);

            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();

            //obj.transform.position = child.position + new Vector3(0, 6f, 0);
            obj.GetComponent<NavMeshAgent>().Warp(child.position);

            yield return null;

            count++;
        }
    }
}
