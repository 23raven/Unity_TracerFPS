using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Abilities/Blink")]
public class Blink : HeroAbility
{
    [SerializeField] private BlinkData data;

    public override void Activate(PlayerManager player)
    {
        CharacterController controller = player.Controller;

        Vector2 moveInput = player.Input.Move;

        Vector3 direction =
            player.transform.forward * moveInput.y +
            player.transform.right * moveInput.x;

        // Если игрок стоит на месте — Blink вперед
        if (direction.sqrMagnitude < 0.01f)
        {
            direction = player.transform.forward;
        }

        direction.Normalize();

        // Временно отключаем CharacterController,
        // чтобы избежать конфликтов при телепортации
        controller.enabled = false;

        player.transform.position += direction * data.Distance;

        controller.enabled = true;
    }

    public override AbilityData GetData()
    {
        return data;
    }
}