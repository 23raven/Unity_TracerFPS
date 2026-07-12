using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Abilities/Pulse Bomb")]
public class PulseBomb : HeroAbility
{
    [SerializeField] private PulseBombData data;

    public override void Activate(PlayerManager player)
    {
        Vector3 spawnPosition =
         player.Camera.transform.position +
         player.Camera.transform.forward * 10f;

        PulseBombProjectile projectile = Instantiate(
            data.ProjectilePrefab,
            spawnPosition,
            player.Camera.transform.rotation);

        projectile.Initialize(player, data);

        projectile.Launch(player.Camera.transform.forward);
    }

    public override AbilityData GetData()
    {
        return data;
    }
}