using UnityEngine;

public class AbilitySlot
{
    public System.Action OnStateChanged;
    public HeroAbility Ability { get; private set; }
    public int CurrentCharges => currentCharges;
    public int MaxCharges => Data == null ? 0 : Data.MaxCharges;
    public bool IsReady => currentCharges > 0;
    private int currentCharges;
    private float rechargeTimer;
    public float RechargeProgress =>
    Data == null || MaxCharges == CurrentCharges
        ? 1f
        : rechargeTimer / Data.Cooldown;
    public float RechargeRemaining =>
    Mathf.Max(0f, Data.Cooldown - rechargeTimer);

    private AbilityData Data => Ability?.GetData();
    public System.Action<HeroAbility> OnAbilityActivated;
    public AbilitySlotType SlotType { get; private set; }
    public bool IsActive { get; private set; }

    public void SetAbility(
    AbilitySlotType slotType,
    HeroAbility ability)
    {
        SlotType = slotType;
        Ability = ability;

        if (Data == null)
        {
            Debug.LogError($"{ability?.name} has no AbilityData assigned!");
            return;
        }

        currentCharges = Data.MaxCharges;
        rechargeTimer = 0f;
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
        if (!CanActivate())
            return;

        Ability.Activate(player);

        ConsumeCharge();
        OnAbilityActivated?.Invoke(Ability);
    }

    private bool CanActivate()
    {
        if (Ability == null)
            return false;

        if (Data == null)
            return false;

        if (!IsReady)
            return false;

        return true;
    }

    private void ConsumeCharge()
    {
        currentCharges--;

        NotifyStateChanged();

        Debug.Log($"{Ability.name}: {currentCharges}/{MaxCharges}");
    }

    private void UpdateCooldown()
    {
        if (Data == null)
            return;

        if (currentCharges >= Data.MaxCharges)
        {
            rechargeTimer = 0f;
            return;
        }

        rechargeTimer += Time.deltaTime;

        NotifyStateChanged();

        if (rechargeTimer < Data.Cooldown)
            return;

        currentCharges++;

        rechargeTimer = 0f;

        NotifyStateChanged();

        Debug.Log($"{Ability.name}: {currentCharges}/{MaxCharges}");
    }

    public void Deactivate(PlayerManager player)
    {
        Ability?.Deactivate(player);
    }

    private void NotifyStateChanged()
    {
        OnStateChanged?.Invoke();
    }
}