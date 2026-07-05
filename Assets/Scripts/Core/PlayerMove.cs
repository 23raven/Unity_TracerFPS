using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private MovementSettings settings;

    private PlayerManager playerManager;
    private PlayerInput input;
    private CharacterController controller;
    private float targetHeight;

    private float verticalVelocity;
    private bool isCrouching;


  

    private void Update()
    {
        HandleCrouch();
        HandleGravity();
        HandleJump();
        HandleMovement();
        UpdateControllerHeight();
    }

    private void HandleMovement()
    {
        Vector2 moveInput = input.Move;

        Vector3 moveDirection =
            transform.forward * moveInput.y +
            transform.right * moveInput.x;

        moveDirection.Normalize();

        float moveSpeed = isCrouching
            ? settings.CrouchSpeed
            : settings.MoveSpeed;

        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleGravity()
    {
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += settings.Gravity * Time.deltaTime;
    }

    private void HandleJump()
    {
        if (!controller.isGrounded)
            return;

        if (!input.JumpPressed)
            return;

        verticalVelocity = settings.JumpForce;
    }

    private void HandleCrouch()
    {
        if (input.CrouchHeld)
        {
            isCrouching = true;
            targetHeight = settings.CrouchingHeight;
            playerManager.Camera.SetCrouch(true);
            return;
        }

        if (!CanStandUp())
            return;

        isCrouching = false;
        targetHeight = settings.StandingHeight;

        playerManager.Camera.SetCrouch(false);
    }

    public void Initialize(PlayerManager manager)
    {
        playerManager = manager;
        settings = manager.Movement;
        input = manager.Input;
        controller = manager.Controller;
        targetHeight = settings.StandingHeight;
    }
      

    private void UpdateControllerHeight()
    {
        controller.height = Mathf.Lerp(
            controller.height,
            targetHeight,
            settings.HeightLerpSpeed * Time.deltaTime);

        controller.center = Vector3.up * controller.height * 0.5f;
    }

    private bool CanStandUp()
    {
        float radius = controller.radius;

        Vector3 bottom = transform.position + Vector3.up * radius;
        Vector3 top = transform.position + Vector3.up * (settings.StandingHeight - radius);

        return !Physics.CheckCapsule(
            bottom,
            top,
            radius,
            settings.GroundLayer);
    }

    public void SetMovementSettings(MovementSettings settings)
    {
        this.settings = settings;
    }

}