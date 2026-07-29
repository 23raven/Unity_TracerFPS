using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    public event Action<float, float> OnHealthChanged;
    public float MaxHealth => maxHealth;
    public bool IsDead => CurrentHealth <= 0f;

    public float CurrentHealth { get; private set; }
    public event Action OnDeath;
    private bool isDead;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    public void TakeDamage(DamageInfo damageInfo)
    {

        CurrentHealth = Mathf.Max(CurrentHealth - damageInfo.Damage, 0f);

        damageInfo.Attacker?.UltimateCharge.Add(4f);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;
        OnDeath?.Invoke();
    }

    public void SetHealth(float health)
    {
        CurrentHealth = Mathf.Clamp(health, 0f, maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }


}