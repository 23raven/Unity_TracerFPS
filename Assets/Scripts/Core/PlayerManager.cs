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
    public HistorySystem HistorySystem { get; private set; }
    public Health Health { get; private set; }
    public UltimateCharge UltimateCharge { get; private set; }

    public AudioManager AudioManager;

    public WeaponAnimation WeaponAnimation;

    [SerializeField] private ParticleSystem recallParticles;

    public ParticleSystem RecallParticles => recallParticles;

    public HeroDefinition CurrentHero => Hero.CurrentHero;
    public MovementSettings Movement => Hero.Movement;
    public WeaponData Weapon => Hero.Weapon;

    [SerializeField] private BlinkUI blinkUI;

    private void Awake()
    {
        Debug.Log("PlayerManager.isAwaking");

        Input = GetComponent<PlayerInput>();
        Move = GetComponent<PlayerMove>();
        Camera = GetComponent<PlayerCamera>();
        Controller = GetComponent<CharacterController>();
        Shoot = GetComponent<PlayerShoot>();
        WeaponHolder = GetComponentInChildren<WeaponHolder>(true);
        Hero = GetComponent<HeroManager>();
        AbilitySystem = GetComponent<AbilitySystem>();
        Health = GetComponent<Health>();
        HistorySystem = GetComponent<HistorySystem>();
        UltimateCharge = GetComponent<UltimateCharge>();
        WeaponAnimation = GetComponentInChildren<WeaponAnimation>(true);

        Move.Initialize(this);
        Camera.Initialize(this);
        WeaponHolder.Initialize(this);
        Shoot.Initialize(this);
        AbilitySystem.Initialize(this);
        blinkUI.Initialize(AbilitySystem.ShiftSlot);
    }
}