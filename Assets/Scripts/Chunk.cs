using System;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Transform obstacle;
    [SerializeField] float laneDistance = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPattern(ChunkPattern pattern)
    {
        if (pattern == ChunkPattern.Straight)
        {
            obstacle.gameObject.SetActive(false);
            return;
        }
        obstacle.gameObject.SetActive(true);

        float xPosition = 0f;

        switch (pattern)
        {
            case ChunkPattern.LeftObstacle:
                xPosition = -0.3333f;
                break;
            case ChunkPattern.CenterObstacle:
                xPosition = 0f;
                break;
            case ChunkPattern.RightObstacle:
                xPosition = 0.3333f;
                break;
        }
        Vector3 position = obstacle.localPosition;

        position.x = xPosition;

        obstacle.localPosition = position;
    }
}
