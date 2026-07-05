using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Abilities/Blink")]
public class Blink : HeroAbility
{
    [SerializeField] private BlinkData data;

    public override void Activate(PlayerManager player)
    {
        CharacterController controller = player.Controller;

        controller.enabled = false;

        player.transform.position +=
            player.transform.forward * data.Distance;

        controller.enabled = true;
    }
    public override AbilityData GetData()
    {
        return data;
    }
}