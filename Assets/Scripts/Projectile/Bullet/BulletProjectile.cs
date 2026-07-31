using UnityEngine;

[RequireComponent(typeof(ProjectileMovement))]
public class BulletProjectile : Projectile
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifeTime = 5f;

    public override void Launch(Vector3 direction)
    {
        movement.Launch(direction, speed);

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnHit(other);
    }

    protected override void OnHit(Collider other)
    {
        Debug.Log("Hit: " + other.name);

        if (other.TryGetComponent(out IDamageable damageable))
        {
            Debug.Log("Damage!");

            damageable.TakeDamage(
                new DamageInfo(
                    damage,
                    owner));
        }

        Destroy(gameObject);
    }
}