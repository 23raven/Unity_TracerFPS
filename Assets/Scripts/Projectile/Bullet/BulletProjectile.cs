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
        Debug.Log("Hit: " + other.name);

        if (hasHit)
            return;
             
        OnHit(other);
    }

    protected override void OnHit(Collider other)
    {
        hasHit = true;

        Debug.Log("Trying damage: " + other.name);

        if (other.TryGetComponent(out Health health))
        {
            Debug.Log("Damage applied");

            health.TakeDamage(new DamageInfo(damage, owner));
        }
        else
        {
            Debug.Log("Health NOT FOUND");
        }

        Destroy(gameObject);
    }
}