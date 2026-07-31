using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletMovement : ProjectileMovement
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Launch(
        Vector3 direction,
        float speed)
    {
        rb.linearVelocity = direction.normalized * speed;
    }

}