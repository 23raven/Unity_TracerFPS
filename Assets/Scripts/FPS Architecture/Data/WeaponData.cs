using UnityEngine;

[CreateAssetMenu(menuName = "Player/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Combat")]
    public float damage = 20f;
    public float range = 100f;
    public float spread = 0.005f;

    [Header("Fire")]
    public FireMode fireMode = FireMode.SemiAuto;
    public float fireRate = 10f;

    [Header("Ammo")]
    public int magazineSize = 30;
    public float reloadTime = 1.5f;
}