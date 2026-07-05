using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    [Header("Cooldown")]
    public float Cooldown = 0f;

    [Header("Charges")]
    public int MaxCharges = 1;
}