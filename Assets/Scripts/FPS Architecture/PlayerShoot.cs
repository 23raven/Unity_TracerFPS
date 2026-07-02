using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private PlayerManager playerManager;
    private WeaponHolder weaponHolder;

    public void Initialize(PlayerManager manager)
    {
        playerManager = manager;
        weaponHolder = manager.WeaponHolder;
    }

    private void Update()
    {
        HandleShoot();
        HandleReload();
    }

    private void HandleShoot()
    {
        if (!weaponHolder.CurrentWeapon.WantsToShoot(playerManager.Input))
            return;

        weaponHolder.CurrentWeapon.Shoot();
    }

    private void HandleReload()
    {
        if (!playerManager.Input.ReloadPressed)
            return;

        weaponHolder.CurrentWeapon.Reload();
    }
}