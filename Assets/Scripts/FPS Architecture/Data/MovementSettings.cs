using UnityEngine;

[CreateAssetMenu(menuName = "Player/Movement Settings")]
public class MovementSettings : ScriptableObject
{
    [Header("Movement")]
    public float MoveSpeed = 5f;

    [Header("Jump")]
    public float JumpForce = 5f;

    [Header("Gravity")]
    public float Gravity = -9.81f;

    [Header("Crouch")]
    public float CrouchSpeed = 2.5f;

    public float StandingHeight = 2f;
    public float CrouchingHeight = 1f;

    public float HeightLerpSpeed = 12f;

    public LayerMask GroundLayer;

}