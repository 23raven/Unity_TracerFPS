using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    private PlayerManager playerManager;

    private AbilitySlot shiftSlot = new();
    private AbilitySlot eSlot = new();
    private AbilitySlot ultimateSlot = new();
    public void Initialize(PlayerManager manager)
    {
        playerManager = manager;

        shiftSlot.SetAbility(playerManager.CurrentHero.ShiftAbility);

        eSlot.SetAbility(playerManager.CurrentHero.EAbility);

        ultimateSlot.SetAbility(playerManager.CurrentHero.UltimateAbility);

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
}