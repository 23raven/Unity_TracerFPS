using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private float damageMultiplier = 1f;

    public float DamageMultiplier => damageMultiplier;
}