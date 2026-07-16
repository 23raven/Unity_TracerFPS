using UnityEngine;

public class WalkingBot : TrainingBot
{
    [Header("Movement")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stoppingDistance = 0.15f;

    private int currentWaypoint;

    private void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypoint];

        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude <= stoppingDistance * stoppingDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;

            return;
        }

        direction.Normalize();

        transform.position += direction * moveSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(direction),
            8f * Time.deltaTime);
    }

    protected override void Respawn()
    {
        currentWaypoint = 0;

        base.Respawn();
    }
}