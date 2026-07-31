using UnityEngine;

public class BulletProjectile : Projectile
{
    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float lifeTime;

    private bool hasHit;

    public void Configure(
    float speed,
    float damage,
    float lifeTime)
    {
        this.speed = speed;
        this.damage = damage;
        this.lifeTime = lifeTime;
    }

    public override void Launch(Vector3 direction)
    {
        movement.Launch(direction, speed);

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit)
            return;

        if (other.isTrigger)
            return;

        OnHit(other);
    }

    protected override void OnHit(Collider other)
    {
        hasHit = true;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(new DamageInfo(damage, owner));
        }

        Destroy(gameObject);
    }
}