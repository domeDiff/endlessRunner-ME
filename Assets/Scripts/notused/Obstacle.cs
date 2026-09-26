using UnityEngine;

public class Obstacle : MonoBehaviour
{
    internal static Vector3 localPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           // Debug.Log("GAME OVER");
        }
    }
}
