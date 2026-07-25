using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Abilities/Blink")]
public class Blink : HeroAbility
{
    [SerializeField] private BlinkData data;

    public override void Activate(PlayerManager player)
    {
        player.StartCoroutine(BlinkRoutine(player));
        player.Camera.PlayChromatic(
    1.0f,
    0.01f,
    0.03f,
    0.05f);
    }

    private IEnumerator BlinkRoutine(PlayerManager player)
    {
        player.AudioManager.PlayBlink();

        if (player.BlinkEffect != null)
        {
            player.BlinkEffect.Play();
        }

        Vector2 moveInput = player.Input.Move;

        Vector3 direction =
            player.transform.forward * moveInput.y +
            player.transform.right * moveInput.x;

        if (direction.sqrMagnitude < 0.01f)
        {
            direction = player.transform.forward;
        }

        direction.Normalize();

        yield return new WaitForSeconds(data.EffectDelay);

        CharacterController controller = player.Controller;

        controller.enabled = false;
        player.transform.position += direction * data.Distance;
        controller.enabled = true;
    }

    public override AbilityData GetData()
    {
        return data;
    }
}