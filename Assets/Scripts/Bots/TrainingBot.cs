using UnityEngine;

public class TrainingBot : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Health health;
    [SerializeField] private Collider bodyHitbox;
    [SerializeField] private Collider headHitbox;
    [SerializeField] private GameObject visuals;

    [Header("Respawn")]
    [SerializeField] private float respawnDelay = 3f;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    protected virtual void Awake()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        health.OnDeath += HandleDeath;
    }

    protected virtual void OnDestroy()
    {
        health.OnDeath -= HandleDeath;
    }

    protected virtual void HandleDeath()
    {
        SetAlive(false);

        Invoke(nameof(Respawn), respawnDelay);
    }

    protected virtual void Respawn()
    {
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;

        health.ResetHealth();

        SetAlive(true);
    }

    protected virtual void SetAlive(bool alive)
    {
        visuals.SetActive(alive);

        if (bodyHitbox != null)
            bodyHitbox.enabled = alive;

        if (headHitbox != null)
            headHitbox.enabled = alive;
    }
}