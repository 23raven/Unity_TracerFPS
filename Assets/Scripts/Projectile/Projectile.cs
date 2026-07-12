using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected PlayerManager owner;

    protected ProjectileMovement movement;

    protected virtual void Awake()
    {
        movement = GetComponent<ProjectileMovement>();
    }

    public virtual void Initialize(PlayerManager owner)
    {
        this.owner = owner;
    }

    public abstract void Launch(Vector3 direction);

    protected abstract void OnHit(Collider other);
}