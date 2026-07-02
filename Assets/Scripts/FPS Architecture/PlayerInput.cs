using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }

    public bool SprintHeld => inputActions.Player.Sprint.IsPressed();

    public bool JumpPressed => inputActions.Player.Jump.WasPressedThisFrame();

    public bool CrouchHeld => inputActions.Player.Crouch.IsPressed();

    public bool FirePressed => inputActions.Player.Fire.WasPressedThisFrame();

    private PlayerInputActions inputActions;

    public bool FireHeld => inputActions.Player.Fire.IsPressed();

    public bool ReloadPressed => inputActions.Player.Reload.WasPressedThisFrame();

    private void Awake()
    {
        inputActions = new PlayerInputActions();

    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        Move = inputActions.Player.Move.ReadValue<Vector2>();
        Look = inputActions.Player.Look.ReadValue<Vector2>();
    }

}