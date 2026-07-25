using UnityEngine;
using UnityEngine.UI;

public class RecallUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private AbilitySlot slot;

    public void Initialize(AbilitySlot slot)
    {
        this.slot = slot;
    }

    private void Update()
    {
        if (slot == null)
            return;

        fillImage.fillAmount = slot.IsReady
            ? 1f
            : slot.RechargeProgress;
    }
}