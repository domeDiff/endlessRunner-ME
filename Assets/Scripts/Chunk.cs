using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Transform obstacle;
    [SerializeField] private float laneDistance = 3f;

    public void SetPattern(ChunkPattern pattern)
    {
        if (obstacle == null)
        {
            Debug.LogError("OBSTACLE IS NULL on " + gameObject.name);
            return;
        }

        // Hide on straight
        if (pattern == ChunkPattern.Straight)
        {
            obstacle.gameObject.SetActive(false);
            return;
        }

        obstacle.gameObject.SetActive(true);

        // RANDOM lane
        int lane = Random.Range(0, 3);

        float x;

        if (lane == 0)
            x = -laneDistance;
        else if (lane == 1)
            x = 0f;
        else
            x = laneDistance;

        // IMPORTANT: directly set local position
        obstacle.localPosition = new Vector3(
            x,
            obstacle.localPosition.y,
            obstacle.localPosition.z
        );

        Debug.Log(
            "CHUNK: " + gameObject.name +
            " | PATTERN: " + pattern +
            " | RANDOM LANE: " + lane +
            " | OBSTACLE X: " + obstacle.localPosition.x
        );
    }
}
