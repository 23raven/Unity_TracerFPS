using UnityEngine;

public abstract class HeroAbility : ScriptableObject
{
    public abstract void Activate(PlayerManager player);
}