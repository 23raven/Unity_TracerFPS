using UnityEngine;

[CreateAssetMenu(menuName = "Player/Movement Settings")]
public class MovementSettings : ScriptableObject
{
    [Header("Movement")]
    public float MoveSpeed = 5f;

    [Header("Gravity")]
    public float Gravity = -9.81f;

    [Header("Jump")]
    public float JumpForce = 5f;
}