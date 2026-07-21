using UnityEngine;

public static class Explosion
{
    public static void Explode(
    Vector3 position,
    float radius,
    DamageInfo damageInfo)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);

        Debug.Log($"Explosion hit {hits.Length} colliders");

        foreach (Collider hit in hits)
        {
            IDamageable damageable = hit.GetComponentInParent<IDamageable>();

            if (damageable == null)
                continue;

            Debug.Log($"Damaged: {hit.name}");

            damageable.TakeDamage(damageInfo);
        }
    }
}