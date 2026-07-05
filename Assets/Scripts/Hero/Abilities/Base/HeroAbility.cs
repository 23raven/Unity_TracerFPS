using UnityEngine;

public abstract class HeroAbility : ScriptableObject
{
    public virtual void Initialize(PlayerManager player) { }

    public virtual void Tick(PlayerManager player) { }

    public abstract void Activate(PlayerManager player);

    public abstract AbilityData GetData();
}