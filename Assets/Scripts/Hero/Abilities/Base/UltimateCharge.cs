using UnityEngine;

public class UltimateCharge : MonoBehaviour
{
    [SerializeField] private float maxCharge = 100f;

    public float CurrentCharge { get; private set; }

    public float MaxCharge => maxCharge;

    public bool IsReady => CurrentCharge >= maxCharge;

    private void Awake()
    {
        CurrentCharge = MaxCharge;
    }

    public void Add(float amount)
    {
        Debug.Log($"Add called with: {amount}");

        CurrentCharge = Mathf.Clamp(CurrentCharge + amount, 0f, maxCharge);

        Debug.Log($"CurrentCharge = {CurrentCharge}");
    }

    public bool TryConsume()
    {
        if (!IsReady)
            return false;

        CurrentCharge = 0f;
        return true;
    }
}