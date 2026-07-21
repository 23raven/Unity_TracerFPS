
using UnityEngine;

public class PulseBombProjectile : Projectile
{
    private PulseBombData data;
    private bool isAttached;


    public void Initialize(PlayerManager owner, PulseBombData data)
    {
        base.Initialize(owner);

        this.data = data;
    }
    public override void Launch(Vector3 direction)
    {
        movement.Launch(direction, data.Speed);

        Destroy(gameObject, data.Lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isAttached)
            return;

        isAttached = true;

        OnHit(collision.collider);
    }

    protected override void OnHit(Collider other)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;

        Transform target = other.GetComponentInParent<TrainingBot>().transform;

        transform.SetParent(target, false);
        transform.localPosition = new Vector3(0, 1.5f, 0);
        transform.localRotation = Quaternion.identity;

        Invoke(nameof(Explode), data.FuseTime);
    }

    private void Explode()
    {

        Explosion.Explode(
            transform.position,
            data.Radius,
            new DamageInfo(
                data.Damage,
                owner));

        Destroy(gameObject);
    }
}