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
        Transform spawn = player.ProjectileSpawn;

        PulseBombProjectile projectile = Instantiate(
            data.ProjectilePrefab,
            spawn.position,
            Quaternion.LookRotation(camera.forward));

        projectile.Initialize(player, data);
        projectile.Launch(camera.forward);
    }

    public override AbilityData GetData()
    {
        return data;
    }
}