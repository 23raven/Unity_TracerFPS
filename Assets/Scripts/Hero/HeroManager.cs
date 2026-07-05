using UnityEngine;

public class HeroManager : MonoBehaviour
{
    [SerializeField] private HeroDefinition currentHero;

    public HeroDefinition CurrentHero => currentHero;

    public MovementSettings Movement => currentHero.Movement;
    public WeaponData Weapon => currentHero.Weapon;

    public HeroAbility PassiveAbility => currentHero.PassiveAbility;
    public HeroAbility ShiftAbility => currentHero.ShiftAbility;
    public HeroAbility EAbility => currentHero.EAbility;
    public HeroAbility UltimateAbility => currentHero.UltimateAbility;
}