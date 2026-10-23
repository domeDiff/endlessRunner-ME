//using UnityEngine;

//public enum ChunkPattern
//{
//    Straight,
//    LeftObstacle,
//    RightObstacle,
//    CenterObstacle
//}

//public class ChunkManager : MonoBehaviour
//{
//    [Header("Chunks")]
//    [SerializeField] private Transform[] chunks;

//    [Header("Player")]
//    [SerializeField] private Transform player;

//    [Header("Settings")]
//    [SerializeField] private float chunkLength = 20f;
//    [SerializeField] private float recycleDistance = 20f;

//    private void Start()
//    {
//        GenerateInitialChunks();
//    }

//    private void Update()
//    {
//        RecycleChunks();
//    }

//    // --------------------------------------------------
//    // INITIAL CHUNKS
//    // --------------------------------------------------

//    private void GenerateInitialChunks()
//    {
//        Debug.Log("===== GENERATING INITIAL CHUNKS =====");

//        foreach (Transform chunk in chunks)
//        {
//            ChangeChunkPattern(chunk);
//        }

//        Debug.Log("===== INITIAL CHUNKS GENERATED =====");
//    }

//    // --------------------------------------------------
//    // FIND FURTHEST BEHIND CHUNK
//    // --------------------------------------------------

//    private Transform GetFurthestBehindChunk()
//    {
//        Transform furthest = chunks[0];

//        foreach (Transform chunk in chunks)
//        {
//            if (chunk.position.z < furthest.position.z)
//            {
//                furthest = chunk;
//            }
//        }

//        return furthest;
//    }

//    // --------------------------------------------------
//    // FIND FURTHEST AHEAD CHUNK
//    // --------------------------------------------------

//    private Transform GetFurthestAheadChunk()
//    {
//        Transform furthest = chunks[0];

//        foreach (Transform chunk in chunks)
//        {
//            if (chunk.position.z > furthest.position.z)
//            {
//                furthest = chunk;
//            }
//        }

//        return furthest;
//    }

//    // --------------------------------------------------
//    // RECYCLE CHUNKS
//    // --------------------------------------------------

//    private void RecycleChunks()
//    {
//        if (player == null || chunks.Length == 0)
//        {
//            return;
//        }

//        Transform furthestBehind = GetFurthestBehindChunk();

//        float distance =
//            player.position.z - furthestBehind.position.z;

//        if (distance > recycleDistance)
//        {
//            Transform furthestAhead = GetFurthestAheadChunk();

//            float newZ =
//                furthestAhead.position.z + chunkLength;

//            // Move chunk forward
//            furthestBehind.position = new Vector3(
//                furthestBehind.position.x,
//                furthestBehind.position.y,
//                newZ
//            );

//            // Give it a NEW random pattern
//            ChangeChunkPattern(furthestBehind);

//            Debug.Log(
//                "Recycled " +
//                furthestBehind.name +
//                " to Z = " +
//                newZ
//            );
//        }
//    }

//    // --------------------------------------------------
//    // RANDOM PATTERN
//    // --------------------------------------------------

//    private void ChangeChunkPattern(Transform chunk)
//    {
//        int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(ChunkPattern)).Length);

//        ChunkPattern pattern = (ChunkPattern)randomIndex;

//        Chunk chunkScript = chunk.GetComponent<Chunk>();

//        if (chunkScript != null)
//        {
//            chunkScript.SetPattern(pattern);

//            Debug.Log(
//                "RANDOM PATTERN: " + pattern +
//                " | CHUNK: " + chunk.name
//            );
//        }
//        else
//        {
//            Debug.LogError("Chunk component missing on " + chunk.name);
//        }
//    }
//}


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