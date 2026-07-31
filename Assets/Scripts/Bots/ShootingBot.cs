using System.Collections;
using UnityEngine;

public class ShootingBot : TrainingBot
{
    [Header("Combat")]
    [SerializeField] private BulletProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private BotAnimation botAnimation;

    [Header("Settings")]
    [SerializeField] private float fireDelay = 3f;

    [Header("Projectile")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileLifeTime;

    private Coroutine shootRoutine;

    protected override void Awake()
    {
        base.Awake();

        shootRoutine = StartCoroutine(ShootRoutine());
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (shootRoutine != null)
            StopCoroutine(shootRoutine);
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireDelay);

            Shoot();
        }
    }

    /// <summary>
    /// Запускает анимацию стрельбы.
    /// Пуля создается Animation Event'ом.
    /// </summary>
    private void Shoot()
    {
        botAnimation.PlayShoot();
    }

    /// <summary>
    /// Вызывается Animation Event.
    /// </summary>
   
    public void SpawnBullet()
    {
        BulletProjectile projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation * projectilePrefab.transform.localRotation);

        projectile.Configure(
            projectileSpeed,
             projectileDamage,
             projectileLifeTime);

        projectile.Launch(firePoint.forward);
    }

}