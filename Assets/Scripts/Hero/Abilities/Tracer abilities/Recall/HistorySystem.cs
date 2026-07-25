using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistorySystem : MonoBehaviour
{
    private const float RecordInterval = 0.05f;
    private const float MaxRecordTime = 3f;
    private const float RecallDuration = 0.9f;
    [SerializeField] private float recallRecoveryTime = 0.15f;

    private readonly List<HistorySnapshot> history = new();

    private float timer;

    private bool isRecalling;

    private PlayerManager player;

    public IReadOnlyList<HistorySnapshot> History => history;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (isRecalling)
            return;

        timer += Time.deltaTime;

        if (timer < RecordInterval)
            return;

        timer = 0f;

        Record();
    }

    private void Record()
    {
        history.Add(new HistorySnapshot(
            transform.position,
            transform.rotation,
            player.Health.CurrentHealth)); // если CurrentHealth есть

        int maxSnapshots = Mathf.CeilToInt(MaxRecordTime / RecordInterval);

        if (history.Count > maxSnapshots)
            history.RemoveAt(0);
    }

    public void StartRecall()
    {
        if (history.Count == 0 || isRecalling)
            return;

        StartCoroutine(RecallRoutine());
    }

    private IEnumerator RecallRoutine()
    {
        isRecalling = true;

        PlayerInput input = player.GetComponent<PlayerInput>();

        if (input != null)
            input.IgnoreMovement = true;

        if (player.RecallParticles != null)
            player.RecallParticles.Play();

        CharacterController controller = player.Controller;
        controller.enabled = false;

        float stepDuration = RecallDuration / (history.Count - 1);

        for (int i = history.Count - 1; i > 0; i--)
        {
            HistorySnapshot from = history[i];
            HistorySnapshot to = history[i - 1];

            float elapsed = 0f;

            while (elapsed < stepDuration)
            {
                float t = elapsed / stepDuration;

                player.transform.position =
                    Vector3.Lerp(from.Position, to.Position, t);

                player.transform.rotation =
                    Quaternion.Slerp(from.Rotation, to.Rotation, t);

                elapsed += Time.deltaTime;

                yield return null;
            }

            to.Apply(player);
        }

        history[0].Apply(player);

        controller.enabled = true;

        if (player.RecallParticles != null)
            player.RecallParticles.Stop();

        history.Clear();

        yield return new WaitForSeconds(recallRecoveryTime);

        if (input != null)
            input.IgnoreMovement = false;

        isRecalling = false;
    }
    public HistorySnapshot GetOldestSnapshot()
    {
        if (history.Count == 0)
            return default;

        return history[0];
    }
}