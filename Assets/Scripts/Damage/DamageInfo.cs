using UnityEngine;

public struct DamageInfo
{
    public readonly float Damage;
    public readonly PlayerManager Attacker;

    public DamageInfo(float damage, PlayerManager attacker)
    {
        Damage = damage;
        Attacker = attacker;
    }
}