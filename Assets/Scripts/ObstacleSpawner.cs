using System.Runtime.CompilerServices;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnZ = 15f;
    [SerializeField] private float spawnDistance = 20f;
    [SerializeField] private Transform player;

    private float nextSpawnZ;
    void Start()
    {
        nextSpawnZ = spawnZ;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z >= nextSpawnZ)
        {
            SpawnObstacle();

            nextSpawnZ += player.position.z + spawnDistance;
        }
    }

    private void SpawnObstacle()
    {
        Vector3 spawnPosition = new Vector3(0f,1f,nextSpawnZ);

        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
