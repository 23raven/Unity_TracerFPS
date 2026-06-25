using UnityEngine;

public class PlayerMovementBasic : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;

    public Transform orientation;

    [Header("Surface check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public float groundDrag;
    bool grounded;

    [Header("Jumps")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Binds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Crouch")]
    public float crouchYScale = 0.5f;
    public float crouchSpeed = 3f;
    private bool isCrouching;
    private float startYScale;
    private float defaultMoveSpeed; // <-- сохраняем исходную скорость

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        startYScale = transform.localScale.y;
        defaultMoveSpeed = moveSpeed; // <-- запоминаем один раз
    }

    private void Update()
    {
        groundCheck();
        myInput();
        speedControl();
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        jumpKeyPressed();

        // GetKeyDown / GetKeyUp — срабатывают ровно один раз
        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartCrouch();

        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();
    }

    private void movePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void groundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        rb.linearDamping = grounded ? groundDrag : 0f;
    }

    private void speedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void resetJump()
    {
        readyToJump = true;
    }

    public void jumpKeyPressed()
    {
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(resetJump), jumpCooldown);
        }
    }

    private void StartCrouch()
    {
        isCrouching = true;
        transform.localScale = new Vector3(
            transform.localScale.x,
            crouchYScale,
            transform.localScale.z);
        moveSpeed = crouchSpeed;
    }

    private void StopCrouch()
    {
        isCrouching = false;
        transform.localScale = new Vector3(
            transform.localScale.x,
            startYScale,
            transform.localScale.z);
        moveSpeed = defaultMoveSpeed; // <-- возвращаем сохранённую скорость
    }
}