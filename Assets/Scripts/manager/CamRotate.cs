using UnityEngine;

public class CamRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1f;
    void Update()
    {
        transform.Rotate(0f, rotateSpeed, 0f);
    }
}
