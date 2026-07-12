using UnityEngine;

public abstract class ProjectileMovement : MonoBehaviour
{
    public abstract void Launch(
        Vector3 direction,
        float speed);
}