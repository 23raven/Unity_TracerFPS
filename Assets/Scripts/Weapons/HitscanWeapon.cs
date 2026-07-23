using UnityEngine;

public class HitscanWeapon : Weapon
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem muzzleFlashTwo;
    [SerializeField] private ParticleSystem impactEffect;
    [SerializeField] private ParticleSystem criticalImpactEffect;
    [SerializeField] private ViewModelMotion viewModelMotion;

    public override void Shoot()
    {
        if (!HasAmmo())
            return;

        if (!CanShoot())
            return;

        Ray ray = new Ray(
        playerCamera.transform.position,
        GetShootDirection());

        if (!Physics.Raycast(ray, out RaycastHit hit, data.range))
            return;

        ConsumeAmmo();

        Hitbox hitbox = hit.collider.GetComponent<Hitbox>();

        PlayMuzzleFlash();
        PlayImpactEffect(hit, hitbox);
        DealDamage(hit, hitbox);
        viewModelMotion.PlayRecoil();
        InvokeShot();
    }

    private void DealDamage(RaycastHit hit, Hitbox hitbox)
    {
        IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();

        if (damageable == null)
            return;

        float damage = data.damage;

        if (hitbox != null)
            damage *= hitbox.DamageMultiplier;

        DamageInfo damageInfo = new DamageInfo(
            damage,
            playerManager);

        damageable.TakeDamage(damageInfo);

        

        if (hitbox != null && hitbox.Critical)
        {
            playerManager.AudioManager.PlayCritical();
        }   else    playerManager.AudioManager.PlayHit();
    }

    private void PlayMuzzleFlash()
    {

        if (muzzleFlash == null)
        {
            Debug.LogError("MuzzleFlash is NULL");
            return;
        }

        if (muzzleFlashTwo == null)
        {
            Debug.LogError("MuzzleFlashTwo is NULL");
            return;
        }

        muzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        muzzleFlash.Play();
        muzzleFlashTwo.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        muzzleFlashTwo.Play();
    }

    private void PlayImpactEffect(RaycastHit hit, Hitbox hitbox)
    {
        ParticleSystem effect = impactEffect;

        if (hitbox != null && hitbox.Critical)
        {
            effect = criticalImpactEffect;
        }

        if (effect == null)
            return;

        Instantiate(
            effect,
            hit.point,
            Quaternion.LookRotation(hit.normal));
    }

    private Vector3 GetShootDirection()
    {
        Vector3 direction = playerCamera.transform.forward;

        direction += playerCamera.transform.right * Random.Range(-data.spread, data.spread);
        direction += playerCamera.transform.up * Random.Range(-data.spread, data.spread);

        return direction.normalized;
    }
}