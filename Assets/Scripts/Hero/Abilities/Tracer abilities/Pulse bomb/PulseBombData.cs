using UnityEngine;
using UnityEngine.ProBuilder;

[CreateAssetMenu(menuName = "Hero/Abilities/Pulse Bomb Data")]
public class PulseBombData : AbilityData
{
    [Header("Projectile")]
    [SerializeField] private PulseBombProjectile projectilePrefab;
    [SerializeField] private float speed = 25f;
    [SerializeField] private float lifetime = 5f;
    [Header("Explosion")]
    [SerializeField] private float damage = 350f;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float fuseTime = 1f;

    public PulseBombProjectile ProjectilePrefab => projectilePrefab;
    public float Speed => speed;
    public float Lifetime => lifetime;
    public float Damage => damage;
    public float Radius => radius;
    public float FuseTime => fuseTime;

}