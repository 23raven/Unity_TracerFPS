using UnityEngine;

public class AbilitySlot
{
    public HeroAbility Ability { get; private set; }

    private int currentCharges;
    private float cooldownTimer;

    public void SetAbility(HeroAbility ability)
    {
        Ability = ability;

        if (Ability == null)
            return;

        currentCharges = Ability.GetData().MaxCharges;
        cooldownTimer = 0f;
    }

    public void Initialize(PlayerManager player)
    {
        Ability?.Initialize(player);
    }

    public void Tick(PlayerManager player)
    {
        Ability?.Tick(player);

        UpdateCooldown();
    }

    public void Activate(PlayerManager player)
    {
        if (Ability == null)
            return;

        if (!CanActivate())
            return;

        Ability.Activate(player);

        ConsumeCharge();
    }

    private bool CanActivate()
    {
        return currentCharges > 0;
    }

    private void ConsumeCharge()
    {
        currentCharges--;

        Debug.Log($"{Ability.name}: {currentCharges}/{Ability.GetData().MaxCharges}");
    }

    private void UpdateCooldown()
    {

    }
}