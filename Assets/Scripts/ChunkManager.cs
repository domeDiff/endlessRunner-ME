using UnityEngine; 

public enum ChunkPattern
{
    Straight,
    LeftObstacle,
    RightObstacle, 
    CenterObstacle,

}

public class ChunkManager : MonoBehaviour
{
    [Header("Chunks")]
    [SerializeField] private Transform[] chunks;

    //[Header("Chunk Prefabs")]
    //[SerializeField] private Transform[] chunkPrefabs;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Settings")]
    [SerializeField] private float chunkLength = 20f;
    [SerializeField] private float recycleDistance = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateInitialChunks();
    }

    // Update is called once per frame
    void Update()
    {
        RecycleChunks();
    }

    private void GenerateInitialChunks()
    {
        Debug.Log("========== INITIAL CHUNK GENERATION ==========");

        foreach (Transform chunk in chunks)
        {
            ChangeChunkPattern(chunk);
        }

        Debug.Log("========== INITIAL GENERATION COMPLETE ==========");
    }
    private Transform GetFurthestBehindChunk()
    {
        Transform furthest = chunks[0];

        foreach (Transform chunk in chunks)
        {
            if (chunk.position.z < furthest.position.z)
                furthest = chunk;
        }

        return furthest;
    }
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

    private void RecycleChunks()
    {
        Transform furthestBehind = GetFurthestBehindChunk();

        float distance = player.position.z - furthestBehind.position.z;

        Debug.Log(
            "Player Z: " + player.position.z +
            " | Behind Chunk: " + furthestBehind.name +
            " | Chunk Z: " + furthestBehind.position.z +
            " | Distance: " + distance
        );

        if (distance > recycleDistance)
        {
            Transform furthestAhead = GetFurthestAheadChunk();

            float newZ = furthestAhead.position.z + chunkLength;

            Debug.Log(
                "RECYCLING: " + furthestBehind.name +
                " from Z " + furthestBehind.position.z +
                " to Z " + newZ
            );

            furthestBehind.position = new Vector3(
                furthestBehind.position.x,
                furthestBehind.position.y,
                newZ
            );

            ChangeChunkPattern(furthestBehind);
        }
    }

    private void ChangeChunkPattern(Transform chunk)
    {
        ChunkPattern pattern = (ChunkPattern)Random.Range(0, System.Enum.GetValues(typeof(ChunkPattern)).Length);



        Chunk chunkScript = chunk.GetComponent<Chunk>();

        if (chunkScript != null)
        {
            chunkScript.SetPattern(pattern);
        }

        string obstacleStatus;

        if (pattern == ChunkPattern.Straight)
        {
            obstacleStatus = "NO OBSTACLE";
        }
        else
        {
            obstacleStatus = "OBSTACLE";
        }

        Debug.Log(
            $"[CHUNK SELECTED] " +
            $"Chunk: {chunk.name} | " +
            $"Pattern: {pattern} | " +
            $"Status: {obstacleStatus} | " +
            $"Z: {chunk.position.z:F1}"
        );
    }

}
