using System.Collections;
using UnityEngine;

public class ShootingBot : TrainingBot
{
    [Header("Combat")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private BotAnimation botAnimation;

    [Header("Settings")]
    [SerializeField] private float fireDelay = 3f;
    [SerializeField] private float projectileSpeed = 35f;

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
        Projectile projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation);

        projectile.Initialize(null);
        projectile.Launch(firePoint.forward * projectileSpeed);
    }
}