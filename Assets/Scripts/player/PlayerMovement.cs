
using UnityEngine;
using UnityEngine.InputSystem; 
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

    [Header("Swipe Controls")]
    [SerializeField] private float swipeThresholdPercent = 0.05f;
    private Vector2 swipeStart;
    private bool isSwiping;
    private bool swipeJump;
    [SerializeField] private ScoreManager scoreManager;
    private int currentLane = 0;
    private RunnerInput input;
    private CharacterController controller;
    private float verticalVelocity;
    private bool isGameOver;
    [SerializeField] private GameObject panel; 

    private int lastMilestone = 0;

    private Animator animator;

    private void Awake()
    { 
        animator = GetComponent<Animator>();
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

    void Update()
    {
        HandleRestart();
        if (isGameOver)
        {
            return;
        }
        HandleSwipeInput();
        HandleLaneInput();
        HandleJump();
        ApplyGravity();
        MovePlayer();
        IncreaseDiff();
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
        bool jumpPressed = input.Player.Jump.WasPressedThisFrame() || swipeJump;
        swipeJump = false;

        if (jumpPressed && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    
    private void HandleSwipeInput()
    {
        Pointer pointer = Pointer.current;
        if (pointer == null) return;

        if (pointer.press.wasPressedThisFrame)
        {
            swipeStart = pointer.position.ReadValue();
            isSwiping = true;
            return;
        }

        if (!isSwiping) return;

        if (pointer.press.wasReleasedThisFrame)
        {
            isSwiping = false;
            return;
        }

        Vector2 delta = pointer.position.ReadValue() - swipeStart;
        float thrshold = Screen.height * swipeThresholdPercent;

        if (delta.magnitude < thrshold) return;

        if(Mathf.Abs(delta.x) > Mathf.Abs(delta.y)){
            ChangeLane(delta.x > 0 ? 1 : -1);
        }

        else if (delta.y> 0)
        {
            swipeJump = true;
        }

        isSwiping = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("GAMER OVER");
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        animator.SetBool("isDead", true);
        panel.SetActive(true);
    }

    private void HandleRestart()
    {
        if (isGameOver && input.Player.Restart.WasPressedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void IncreaseDiff()
    {
        {
            int milestone = (int)(ScoreManager.score / 100) * 100;

            if (milestone > lastMilestone)
            {
                if (forwardSpeed > 45)
                    return;

                lastMilestone = milestone;
                forwardSpeed += 3f;
            }
        }
    }
}