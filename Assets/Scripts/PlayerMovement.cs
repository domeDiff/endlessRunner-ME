using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [Header("Forward Movement")]
    [SerializeField] private float forwardSpeed = 10f;

    [Header("lane movement")]
    [SerializeField] private float laneDistance = 3f;
    [SerializeField] private float laneChangeSpeed = 10f;

    [Header("jump")]
    [SerializeField] private float gravity = -20f;
    //private float previousInputX = 0f;

    private int currentLane = 0;
    private RunnerInput input;
    private CharacterController controller;
    private float verticalVelocity;

    private void Awake()
    {
        input = new RunnerInput();
        controller = GetComponent<CharacterController>();
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

        HandleLaneInput();
        ApplyGravity();
        MovePlayer();

    }

    private void MovePlayer()
    {
        float targetX = currentLane * laneDistance;

        float newX = Mathf.Lerp(
            transform.position.x,
            targetX,
            laneChangeSpeed * Time.deltaTime
        );

        float horizontalMovement = newX - transform.position.x;

        float forwardMovement = forwardSpeed * Time.deltaTime;

        Vector3 movement = new Vector3(
            horizontalMovement,
            verticalVelocity * Time.deltaTime,
            forwardMovement
        );

        controller.Move(movement);
    }

    private void HandleLaneInput()
    {
        //Vector2 moveInput = input.Player.Move.ReadValue<Vector2>();

        if (input.Player.MoveLeft.WasPressedThisFrame())
        {
            Debug.Log("left");
            ChangeLane(-1);
        }

        if (input.Player.MoveRight.WasPressedThisFrame())
        {
            ChangeLane(1);
            Debug.Log("Rigt");
        }
    }
    private void ChangeLane(int direction)
    {
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, -1, 1);
    }
    private void ApplyGravity()
    {
        if(controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
    }
}
