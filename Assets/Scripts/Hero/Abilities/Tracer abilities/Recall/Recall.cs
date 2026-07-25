using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Abilities/Recall")]
public class Recall : HeroAbility
{
    [SerializeField] private RecallData data;

    public override void Activate(PlayerManager player)
    {
        player.AudioManager.PlayRecall();

        player.WeaponAnimation.PlayRecall();
        player.Camera.PlayFov(
            65f,
            0.15f,
             0.60f,
            0.15f);

        HistorySystem history = player.GetComponent<HistorySystem>();

        if (history == null)
            return;

        history.StartRecall();
    }

    public override AbilityData GetData()
    {
        return data;
    }
}