using UnityEngine;

public class HitscanWeapon : Weapon
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem muzzleFlashTwo;
    [SerializeField] private ParticleSystem impactEffect;

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

        PlayMuzzleFlash();
        PlayImpactEffect(hit);
        DealDamage(hit);
        InvokeShot();
    }

    private void DealDamage(RaycastHit hit)
    {
        IDamageable damageable = hit.collider.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        DamageInfo damageInfo = new DamageInfo(
            data.damage,
            playerManager);

        damageable.TakeDamage(damageInfo);

        playerManager.AudioManager.PlayHit();
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

    private void PlayImpactEffect(RaycastHit hit)
    {
        if (impactEffect == null)
            return;

        Instantiate(
            impactEffect,
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