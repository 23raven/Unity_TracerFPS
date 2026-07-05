using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInput Input { get; private set; }
    public PlayerMove Move { get; private set; }
    public PlayerCamera Camera { get; private set; }
    public CharacterController Controller { get; private set; }
    public PlayerShoot Shoot { get; private set; }
    public WeaponHolder WeaponHolder { get; private set; }
    public HeroManager Hero { get; private set; }
    public AbilitySystem AbilitySystem { get; private set; }

    private void Awake()
    {
        Debug.Log("PlayerManager.isAwaking");
        Input = GetComponent<PlayerInput>();
        Debug.Log("Input OK");

        Move = GetComponent<PlayerMove>();
        Debug.Log("Move OK");

        Camera = GetComponent<PlayerCamera>();
        Debug.Log("Camera OK");

        Controller = GetComponent<CharacterController>();
        Debug.Log("Controller OK");

        Shoot = GetComponent<PlayerShoot>();
        Debug.Log("Shoot OK");

        WeaponHolder = GetComponentInChildren<WeaponHolder>(true);
        Debug.Log("WeaponHolder OK");

        Hero = GetComponent<HeroManager>();
        Debug.Log("Hero OK");

        AbilitySystem = GetComponent<AbilitySystem>();
        Debug.Log("AbilitySystem OK");

        Move.Initialize(this);
        Debug.Log("Move Initialized");

        Camera.Initialize(this);
        Debug.Log("Camera Initialized");

        WeaponHolder.Initialize(this);
        Debug.Log("WeaponHolder Initialized");

        Shoot.Initialize(this);
        Debug.Log("Shoot Initialized");

        AbilitySystem.Initialize(this);
        Debug.Log("AbilitySystem Initialized");
    }
}