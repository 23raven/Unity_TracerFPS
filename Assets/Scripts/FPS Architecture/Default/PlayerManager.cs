using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInput Input { get; private set; }
    public PlayerMove Move { get; private set; }
    public PlayerCamera Camera { get; private set; }
    public CharacterController Controller { get; private set; }
    public PlayerShoot Shoot { get; private set; }

    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
        Move = GetComponent<PlayerMove>();
        Camera = GetComponent<PlayerCamera>();
        Controller = GetComponent<CharacterController>();
        Shoot = GetComponent<PlayerShoot>();

        Move.Initialize(this);
        Camera.Initialize(this);
        Shoot.Initialize(this);
    }
}