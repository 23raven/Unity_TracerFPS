using UnityEngine;

public class HitscanWeapon : Weapon
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem impactEffect;
    [SerializeField] private float spread = 0.5f;

    public override void Shoot()
    {
        if (!HasAmmo())
            return;

        if (!CanShoot())
            return;

        Ray ray = new Ray(
        playerCamera.transform.position,
        GetShootDirection());

        if (!Physics.Raycast(ray, out RaycastHit hit, range))
            return;

        ConsumeAmmo();

        PlayMuzzleFlash();
        PlayImpactEffect(hit);
        DealDamage(hit);
    }

    private void DealDamage(RaycastHit hit)
    {
        IDamageable damageable = hit.collider.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        damageable.TakeDamage(damage);
    }

    private void PlayMuzzleFlash()
    {

        if (muzzleFlash == null)
        {
            Debug.LogError("MuzzleFlash is NULL");
            return;
        }

        muzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        muzzleFlash.Play();
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

        direction += playerCamera.transform.right * Random.Range(-spread, spread);
        direction += playerCamera.transform.up * Random.Range(-spread, spread);

        return direction.normalized;
    }
}