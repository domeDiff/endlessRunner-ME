using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Transform obstacle;
    [SerializeField] private float laneDistance = 3f;

    //public void SetPattern(ChunkPattern pattern)
    //{
    //    if (obstacle == null)
    //    {
    //        Debug.LogError("Obstacle is not assigned on " + gameObject.name);
    //        return;
    //    }

    //    if (pattern == ChunkPattern.Straight)
    //    {
    //        obstacle.gameObject.SetActive(false);
    //        return;
    //    }

    //    obstacle.gameObject.SetActive(true);

    //    float xPosition = 0f;

    //    switch (pattern)
    //    {
    //        case ChunkPattern.LeftObstacle:
    //            xPosition = -laneDistance;
    //            break;

    //        case ChunkPattern.CenterObstacle:
    //            xPosition = 0f;
    //            break;

    //        case ChunkPattern.RightObstacle:
    //            xPosition = laneDistance;
    //            break;
    //    }

    //    Vector3 position = obstacle.localPosition;
    //    position.x = xPosition;
    //    obstacle.localPosition = position;
    //}

    public void SetPattern(ChunkPattern pattern)
    {
        if (obstacle == null)
        {
            Debug.LogError("NO HUMAN MODEL ASSIGNED TO: " + gameObject.name);
            return;
        }

        obstacle.gameObject.SetActive(true);

        Vector3 position = obstacle.localPosition;
        position.x = 0f;
        obstacle.localPosition = position;
    }

}
