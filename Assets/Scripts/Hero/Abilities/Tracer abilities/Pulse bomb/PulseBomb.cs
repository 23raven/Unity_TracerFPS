using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Abilities/Pulse Bomb")]
public class PulseBomb : HeroAbility
{
    [SerializeField] private PulseBombData data;

    public override void Activate(PlayerManager player)
    {
        if (!player.UltimateCharge.TryConsume())
            return;

        Transform camera = player.Camera.CameraHandle;

        Vector3 spawnPosition =
            camera.position +
            camera.forward * 0.5f;

        PulseBombProjectile projectile = Instantiate(
            data.ProjectilePrefab,
            spawnPosition,
            camera.rotation);

        projectile.Initialize(player, data);
        projectile.Launch(camera.forward);
    }

    public override AbilityData GetData()
    {
        return data;
    }
}