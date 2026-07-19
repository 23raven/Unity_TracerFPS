using UnityEngine;

[CreateAssetMenu(menuName = "Hero/Abilities/Recall")]
public class Recall : HeroAbility
{
    [SerializeField] private RecallData data;

    public override void Activate(PlayerManager player)
    {
        player.AudioManager.PlayRecall();
        HistorySystem history =
            player.GetComponent<HistorySystem>();

        if (history == null)
            return;

        HistorySnapshot snapshot =
            history.GetOldestSnapshot();

        snapshot.Apply(player);
    }

    public override AbilityData GetData()
    {
        return data;
    }
}