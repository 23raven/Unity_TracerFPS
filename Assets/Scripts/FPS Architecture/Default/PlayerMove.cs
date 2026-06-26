using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private MovementSettings settings;

    private PlayerManager playerManager;
    private PlayerInput input;
    private CharacterController controller;

    private float verticalVelocity;
    private bool isCrouching;

  
    private void Update()
    {
        HandleCrouch();
        HandleGravity();
        HandleJump();
        HandleMovement();
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
        Debug.Log(input == null);
        Debug.Log(controller == null);
        Debug.Log(settings == null);

        if (input.CrouchHeld)
        {
            isCrouching = true;
            controller.height = settings.CrouchingHeight;
        }
        else
        {
            isCrouching = false;
            controller.height = settings.StandingHeight;
        }
    }

    public void Initialize(PlayerManager manager)
    {
        playerManager = manager;

        input = manager.Input;
        controller = manager.Controller;
    }

}