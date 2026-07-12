using UnityEngine;

public static class Explosion
{
    public static void Explode(
        Vector3 position,
        float radius,
        DamageInfo damageInfo)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damageInfo);
            }
        }
    }
}