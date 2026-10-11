using UnityEngine;

public class Obstacle : MonoBehaviour
{
    internal static Vector3 localPosition;

    private Animator animator;
    private int index;

    private void Awake()
    {
        index = Random.Range(0, 5);
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        switch (index)
        {
            case 0: animator.SetInteger("index", 0);
                break;

            case 1:
                animator.SetInteger("index", 1);
                break;

            case 2:
                animator.SetInteger("index", 2);
                break;

            case 3:
                animator.SetInteger("index", 3);
                break;

            case 4:
                animator.SetInteger("index", 4);
                break;

            case 5:
                animator.SetInteger("index", 5);
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           // Debug.Log("GAME OVER");
        }
    }
}
