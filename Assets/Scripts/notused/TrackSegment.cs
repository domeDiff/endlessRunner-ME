using System;
using Unity.Mathematics;
using UnityEngine;

public class TrackSegment : MonoBehaviour
{

    [SerializeField] private GameObject obstaclePrefab;
    //[SerializeField] private Transform obstacleSpawnPoint;

    [SerializeField] private float laneDistance = 3f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SpawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void RandomizeObstacleLane()
    {
        int lane = UnityEngine.Random.Range(-1, 2);

        Vector3 localPosition = Obstacle.localPosition;

        localPosition.x =  lane * laneDistance;

        Obstacle.localPosition = localPosition;
    }
}
