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

        float x;

        switch (pattern)
        {
            case ChunkPattern.LeftObstacle:
                x = -laneDistance;
                break;
            case ChunkPattern.RightObstacle:
                x = laneDistance;
                break;
            case ChunkPattern.CenterObstacle:
                x = 0f;
                break;
            default:
                x = 0f;
                break;
        }

        obstacle.localPosition = new Vector3(
            x,
            obstacle.localPosition.y,
            obstacle.localPosition.z
        );

        Debug.Log(
            "CHUNK: " + gameObject.name +
            " | PATTERN: " + pattern +
            " | OBSTACLE X: " + obstacle.localPosition.x
        );
    }
}
