using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    protected PlayerManager playerManager;
    [SerializeField] protected WeaponData data;

    protected int currentAmmo;
    protected bool isReloading;
    public bool IsReloading => isReloading;
    private float nextFireTime;

    public int CurrentAmmo => currentAmmo;
    public int MagazineSize => data.magazineSize;

    public virtual void Initialize(PlayerManager manager)
    {
        playerManager = manager;
        currentAmmo = data.magazineSize;
        data = manager.Weapon;
    }

    public abstract void Shoot();

    protected bool CanShoot()
    {
        if (isReloading)
            return false;

        if (Time.time < nextFireTime)
            return false;

        nextFireTime = Time.time + 1f / data.fireRate;
        return true;
    }

    public bool WantsToShoot(PlayerInput input)
    {
        return data.fireMode switch
        {
            FireMode.SemiAuto => input.FirePressed,
            FireMode.FullAuto => input.FireHeld,
            _ => false
        };
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }

    protected void ConsumeAmmo()
    {
        currentAmmo--;
        Debug.Log($"{CurrentAmmo}/{MagazineSize}");
    }

    public void Reload()
    {
        if (isReloading)
            return;

        if (currentAmmo == data.magazineSize)
            return;

        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(data.reloadTime);

        currentAmmo = data.magazineSize;
        isReloading = false;

        Debug.Log($"Reload complete: {CurrentAmmo}/{MagazineSize}");
    }

    public void SetWeaponData(WeaponData data)
    {
        this.data = data;
    }

}