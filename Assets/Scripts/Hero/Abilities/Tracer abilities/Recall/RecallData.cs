using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Abilities/Recall Data")]
public class RecallData : AbilityData
{
    [Header("Recall")]
    public float RecallDuration = 0.9f;

    public bool RestoreHealth = true;
}