using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    private PlayerManager playerManager;
    private HeroManager heroManager;

    public void Initialize(PlayerManager manager)
    {
        playerManager = manager;
        heroManager = manager.Hero;
    }

    private void Update()
    {
        HandleShift();
        HandleE();
        HandleUltimate();
    }

    private void HandleShift()
    {
        if (!playerManager.Input.ShiftPressed)
            return;

        if (heroManager.CurrentHero.ShiftAbility == null)
            return;

        heroManager.CurrentHero.ShiftAbility.Activate(playerManager);
    }

    private void HandleE()
    {
        if (!playerManager.Input.EPressed)
            return;

        if (heroManager.CurrentHero.EAbility == null)
            return;

        heroManager.CurrentHero.EAbility.Activate(playerManager);
    }

    private void HandleUltimate()
    {
        if (!playerManager.Input.UltimatePressed)
            return;

        if (heroManager.CurrentHero.UltimateAbility == null)
            return;

        heroManager.CurrentHero.UltimateAbility.Activate(playerManager);
    }
}