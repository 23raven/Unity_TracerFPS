using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    private PlayerManager playerManager;

    private AbilitySlot shiftSlot = new();
    private AbilitySlot eSlot = new();
    private AbilitySlot ultimateSlot = new();
    public AbilitySlot ShiftSlot => shiftSlot;
    public void Initialize(PlayerManager manager)
    {
        playerManager = manager;

        shiftSlot.SetAbility(
            AbilitySlotType.Shift,
            playerManager.Hero.ShiftAbility);

        eSlot.SetAbility(
            AbilitySlotType.E,
            playerManager.Hero.EAbility);

        ultimateSlot.SetAbility(
            AbilitySlotType.Ultimate,
            playerManager.Hero.UltimateAbility);

        shiftSlot.Initialize(playerManager);
        eSlot.Initialize(playerManager);
        ultimateSlot.Initialize(playerManager);
    }

    private void Update()
    {
        shiftSlot.Tick(playerManager);
        eSlot.Tick(playerManager);
        ultimateSlot.Tick(playerManager);

        HandleShift();
        HandleE();
        HandleUltimate();
    }

    private void HandleShift()
    {
        if (!playerManager.Input.ShiftPressed)
            return;

        shiftSlot.Activate(playerManager);
    }

    private void HandleE()
    {
        if (!playerManager.Input.EPressed)
            return;

        eSlot.Activate(playerManager);
    }

    private void HandleUltimate()
    {
        if (!playerManager.Input.UltimatePressed)
            return;

         ultimateSlot.Activate(playerManager);
    }

    private void HandleSecondary() {
        if (!playerManager.Input.UltimatePressed)
            return;

        ultimateSlot.Activate(playerManager);
    }

}