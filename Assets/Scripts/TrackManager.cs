using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [SerializeField] private Transform[] trackSegments;
    [SerializeField] private Transform player;
    [SerializeField] private float segmentLength =20f;
    [SerializeField] private float recycleDistance = 20f;

    private void Update()
    {
        RecycleSegment();
    }
    private Transform GetFurthestSegment()
    {
        Transform furthest = trackSegments[0];

        foreach(Transform segement in trackSegments)
        {
            if (segement.position.z < furthest.position.z)
            {
                furthest = segement;
            }
        }

        return furthest;
    }

    private void RecycleSegment()
    {
        Transform furthest = GetFurthestSegment();

        if(player.position.z - furthest.position.z > recycleDistance)
        {
            float newZ = furthest.position.z;

            foreach(Transform segment in trackSegments)
            {
                if(segment.position.z > newZ)
                {
                    newZ = segment.position.z;
                }
            }

            newZ += segmentLength;

            furthest.position = new Vector3(furthest.position.x, furthest.position.y,newZ);

            furthest.GetComponent<TrackSegment>().RandomizeObstacleLane();  
        }
    }
}
