using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Chunks")]
    [SerializeField] private Transform[] chunks;

    [Header("Chunk Prefabs")]
    [SerializeField] private Transform[] chunkPrefabs;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Settings")]
    [SerializeField] private float chunklength = 20f;
    [SerializeField] private float recycleDistance = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RecycleChunks();
    }

    private Transform GetFurthestBehindChunk()
    {
        Transform furthest = chunks[0];

        foreach (Transform chunk in chunks)
        {
            if(chunk.position.z < furthest.position.z)
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

        if(player.position.z - furthestBehind.position.z > recycleDistance)
        {
            Transform furthestAhead = GetFurthestAheadChunk();

            float newZ = furthestAhead.position.z + chunklength;

            furthestBehind.position = new Vector3(furthestBehind.position.x, furthestAhead.position.y, newZ);
        }

        ChangeChunkPattern(furthestBehind);
    }

    private void ChangeChunkPattern(Transform chunk)
    {
        int randomIndex = Random.Range(0, chunkPrefabs.Length);

        Debug.Log("selected chunk patter: " + chunkPrefabs[randomIndex].name);
    }
}
