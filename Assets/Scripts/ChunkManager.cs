using UnityEngine;

public enum ChunkPattern
{
    Straight,
    LeftObstacle,
    RightObstacle,
    CenterObstacle
}

public class ChunkManager : MonoBehaviour
{
    [Header("Chunks")]
    [SerializeField] private Transform[] chunks;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Settings")]
    [SerializeField] private float chunkLength = 20f;
    [SerializeField] private float recycleDistance = 20f;

    private void Start()
    {
        GenerateInitialChunks();
    }

    private void Update()
    {
        RecycleChunks();
    }

    // --------------------------------------------------
    // INITIAL CHUNKS
    // --------------------------------------------------

    private void GenerateInitialChunks()
    {
        Debug.Log("===== GENERATING INITIAL CHUNKS =====");

        foreach (Transform chunk in chunks)
        {
            ChangeChunkPattern(chunk);
        }

        Debug.Log("===== INITIAL CHUNKS GENERATED =====");
    }

    // --------------------------------------------------
    // FIND FURTHEST BEHIND CHUNK
    // --------------------------------------------------

    private Transform GetFurthestBehindChunk()
    {
        Transform furthest = chunks[0];

        foreach (Transform chunk in chunks)
        {
            if (chunk.position.z < furthest.position.z)
            {
                furthest = chunk;
            }
        }

        return furthest;
    }

    // --------------------------------------------------
    // FIND FURTHEST AHEAD CHUNK
    // --------------------------------------------------

    private Transform GetFurthestAheadChunk()
    {
        Transform furthest = chunks[0];

        foreach (Transform chunk in chunks)
        {
            if (chunk.position.z > furthest.position.z)
            {
                furthest = chunk;
            }
        }

        return furthest;
    }

    // --------------------------------------------------
    // RECYCLE CHUNKS
    // --------------------------------------------------

    private void RecycleChunks()
    {
        if (player == null || chunks.Length == 0)
        {
            return;
        }

        Transform furthestBehind = GetFurthestBehindChunk();

        float distance =
            player.position.z - furthestBehind.position.z;

        if (distance > recycleDistance)
        {
            Transform furthestAhead = GetFurthestAheadChunk();

            float newZ =
                furthestAhead.position.z + chunkLength;

            // Move chunk forward
            furthestBehind.position = new Vector3(
                furthestBehind.position.x,
                furthestBehind.position.y,
                newZ
            );

            // Give it a NEW random pattern
            ChangeChunkPattern(furthestBehind);

            Debug.Log(
                "Recycled " +
                furthestBehind.name +
                " to Z = " +
                newZ
            );
        }
    }

    // --------------------------------------------------
    // RANDOM PATTERN
    // --------------------------------------------------

    private void ChangeChunkPattern(Transform chunk)
    {
        // Pick random pattern
        ChunkPattern pattern =
            (ChunkPattern)Random.Range(
                0,
                System.Enum.GetValues(typeof(ChunkPattern)).Length
            );

        // Get Chunk script
        Chunk chunkScript =
            chunk.GetComponent<Chunk>();

        if (chunkScript == null)
        {
            Debug.LogError(
                "Chunk script NOT found on: " +
                chunk.name
            );

            return;
        }

        // Apply random pattern
        chunkScript.SetPattern(pattern);

        Debug.Log(
            "Chunk: " +
            chunk.name +
            " | Pattern: " +
            pattern
        );
    }
}
