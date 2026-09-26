using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Forward Movement")]
    [SerializeField] private float forwardSpeed = 10f;

    [Header("lane movement")]
    [SerializeField] private float laneDistance = 3f;
    [SerializeField] private float laneChangeSpeed = 10f;

    [Header("jump")]
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jumpHeight = 2f;
    //private float previousInputX = 0f;

    private int currentLane = 0;
    private RunnerInput input;
    private CharacterController controller;
    private float verticalVelocity;
    private bool isGameOver;

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

    void Start() { }

    void Update()
    {
        HandleRestart();
        if (isGameOver)
        {
            return;
        }
        HandleLaneInput();
        HandleJump();
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
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;
    }

    private void HandleJump()
    {
        if (input.Player.Jump.WasPressedThisFrame() && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("GAME OVER!!");
    }

    private void HandleRestart()
    {
        if (isGameOver && input.Player.Restart.WasPressedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}