using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private Transform player;

    [SerializeField] private float spawnDistance = 20f;
    [SerializeField] private float firstSpawnDistance = 60f;
    [SerializeField] private float spawnAheadDistance = 100f;

    private float nextSpawnZ;

    private void Start()
    {
        nextSpawnZ = player.position.z + firstSpawnDistance;

        SpawnUntilAhead();
    }

    private void Update()
    {
        SpawnUntilAhead();
    }

    private void SpawnUntilAhead()
    {
        while (nextSpawnZ <= player.position.z + spawnAheadDistance)
        {
            SpawnObstacle();

            nextSpawnZ += spawnDistance;
        }
    }

    private void SpawnObstacle()
    {
        Vector3 spawnPosition = new Vector3(
            0f,
            1f,
            nextSpawnZ
        );

        Instantiate(
            obstaclePrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}