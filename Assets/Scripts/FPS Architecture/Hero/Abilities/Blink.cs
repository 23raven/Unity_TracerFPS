using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Abilities/Blink")]
public class Blink : HeroAbility
{
    [SerializeField] private float distance = 7f;

    public override void Activate(PlayerManager player)
    {
        CharacterController controller = player.Controller;

        controller.enabled = false;

        player.transform.position +=
            player.transform.forward * distance;

        controller.enabled = true;
    }
}