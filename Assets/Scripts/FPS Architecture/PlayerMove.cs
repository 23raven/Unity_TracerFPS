using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private MovementSettings settings;

    private PlayerManager playerManager;
    private float verticalVelocity;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        HandleGravity();
        HandleJump();
        Move();
    }

  
    private void Move()
    {
        Vector2 input = playerManager.Input.Move;

        Vector3 moveDirection =
            transform.forward * input.y +
            transform.right * input.x;

        moveDirection.Normalize();

        Vector3 velocity = moveDirection * settings.MoveSpeed;
        velocity.y = verticalVelocity;

        playerManager.Controller.Move(velocity * Time.deltaTime);
    }

    private void HandleGravity()
    {
        if (playerManager.Controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += settings.Gravity * Time.deltaTime;
    }

    private void HandleJump()
    {
        if (!playerManager.Controller.isGrounded)
            return;

        if (!playerManager.Input.JumpPressed)
            return;

        verticalVelocity = settings.JumpForce;
    }
}