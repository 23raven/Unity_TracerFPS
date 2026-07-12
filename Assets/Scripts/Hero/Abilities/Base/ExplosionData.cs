using UnityEngine;

public abstract class ExplosionData : ScriptableObject
{
    [Header("Explosion")]
    public float Damage = 350f;

    public float Radius = 3f;
}