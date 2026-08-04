using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health playerHealth;

    [Header("UI")]
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Image healthBarBackground;

    private void OnEnable()
    {
        playerHealth.OnHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= UpdateHealth;
    }

    private void Start()
    {
        UpdateHealth(
            playerHealth.CurrentHealth,
            playerHealth.MaxHealth);
    }

    private void UpdateHealth(float currentHealth, float maxHealth)
    {
        float normalizedHealth = currentHealth / maxHealth;

        healthBarFill.fillAmount = normalizedHealth;

        if (healthBarBackground != null)
            healthBarBackground.fillAmount = normalizedHealth;
    }
}