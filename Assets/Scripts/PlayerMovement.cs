using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Forward Movement")]
    [SerializeField] private float forwardSpeed = 10f;

    [Header("lane movement")]
    [SerializeField] private float laneDistance = 3f;
    [SerializeField] private float laneChangeSpeed = 10f;
    //private float previousInputX = 0f;

    private int currentLane = 0;
    private RunnerInput input;

    private void Awake()
    {
        input = new RunnerInput();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        MoveForward();
        HandleLaneInput();
        MoveToLane();

    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;
    }

    private void HandleLaneInput()
    {
        //Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();

        if (input.Player.MoveLeft.WasPressedThisFrame())
        {
            ChangeLane(-1);
        }

        if (input.Player.MoveRight.WasPressedThisFrame())
        {
            ChangeLane(1);
        }
    }

    private void ChangeLane(int direction)
    {
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, -1, 1);
    }

    private void MoveToLane()
    {
        float targetX = currentLane * laneDistance;

        float newX = Mathf.Lerp(
            transform.position.x,
            targetX,
            laneChangeSpeed * Time.deltaTime
        );

        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }
}
