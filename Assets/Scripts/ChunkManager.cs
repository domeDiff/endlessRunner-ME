
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Chunk Prefabs (drag prefabs from Project, not scene objects)")]
    [SerializeField] private GameObject[] chunkPrefabs;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Settings")]
    [SerializeField] private float chunkLength = 20f;
    [SerializeField] private float recycleDistance = 20f;
    [SerializeField] private int activeChunks = 4;
    [SerializeField] private float startZ = 0f;
    [SerializeField] private float xPosition = 0f;
    [SerializeField] private float yPosition = 0f;

    private readonly Queue<GameObject> active = new Queue<GameObject>();
    private float nextZ;
    private int lastIndex = -1;

    private void Start()
    {
        nextZ = startZ;
        for (int i = 0; i < activeChunks; i++)
            SpawnChunk();
    }

    private void Update()
    {
        if (player == null || active.Count == 0) return;

        GameObject oldest = active.Peek();
        if (player.position.z - oldest.transform.position.z > recycleDistance)
        {
            Destroy(active.Dequeue());
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        int index = GetRandomIndex();
        GameObject chunk = Instantiate(
            chunkPrefabs[index],
            new Vector3(xPosition, yPosition, nextZ),
            Quaternion.identity,
            transform);

        active.Enqueue(chunk);
        nextZ += chunkLength;
    }

    // Random, but never the same prefab twice in a row
    private int GetRandomIndex()
    {
        if (chunkPrefabs.Length == 1) return 0;

        int index;
        do { index = Random.Range(0, chunkPrefabs.Length); }
        while (index == lastIndex);

        lastIndex = index;
        return index;
    }
}