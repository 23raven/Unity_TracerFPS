using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image fill;
    [SerializeField] private Canvas canvas;

    private Transform cameraTransform;

    private void Awake()
    {
        if (health == null)
            health = GetComponentInParent<Health>();

        if (canvas == null)
            canvas = GetComponent<Canvas>();

        health.OnHealthChanged += UpdateHealth;
    }

    private void Start()
    {
        Camera[] cameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);
               
        cameraTransform = Camera.main?.transform;
    }

    private void OnDestroy()
    {
        if (health != null)
            health.OnHealthChanged -= UpdateHealth;
    }

    private void LateUpdate()
    {
        if (cameraTransform == null)
            return;

        transform.rotation = Quaternion.LookRotation(
            transform.position - cameraTransform.position);

    }

    private void UpdateHealth(float current, float max)
    {
        fill.fillAmount = max > 0f ? current / max : 0f;

        canvas.enabled = current > 0f && current < max;
    }
}