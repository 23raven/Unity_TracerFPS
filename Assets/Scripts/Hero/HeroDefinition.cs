using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Hero Definition")]
public class HeroDefinition : ScriptableObject
{
    [Header("Movement")]
    public MovementSettings Movement;

    [Header("Weapon")]
    public WeaponData Weapon;

    [Header("Abilities")]
    public HeroAbility PassiveAbility;
    public HeroAbility ShiftAbility;
    public HeroAbility EAbility;
    public HeroAbility UltimateAbility;
    public HeroAbility SecondaryAbility;
}