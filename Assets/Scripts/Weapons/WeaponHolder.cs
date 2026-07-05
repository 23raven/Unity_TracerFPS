using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;

    public Weapon CurrentWeapon => currentWeapon;

    public void Initialize(PlayerManager manager)
    {
        currentWeapon.Initialize(manager);
    }
}