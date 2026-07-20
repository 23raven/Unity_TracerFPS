using UnityEngine;
using UnityEngine.UI;

public class BlinkUI : MonoBehaviour
{
    [System.Serializable]
    private class ChargeUI
    {
        public Image Empty;
        public Image Fill;
    }

    [SerializeField] private ChargeUI[] charges;

    private AbilitySlot slot;

    public void Initialize(AbilitySlot abilitySlot)
    {
        slot = abilitySlot;

        slot.OnStateChanged += Refresh;

        Refresh();
    }

    private void Refresh()
    {
        UpdateCharges(
            slot.CurrentCharges,
            slot.RechargeProgress);
    }


    /// <summary>
    /// availableCharges = сколько зарядов сейчас доступно.
    /// rechargeProgress = прогресс восстановления следующего заряда (0-1).
    /// </summary>
    public void UpdateCharges(int availableCharges, float rechargeProgress)
    {
        for (int i = 0; i < charges.Length; i++)
        {
            ChargeUI charge = charges[i];

            if (i < availableCharges)
            {
                charge.Fill.gameObject.SetActive(true);
                charge.Fill.fillAmount = 1f;
            }
            else if (i == availableCharges)
            {
                charge.Fill.gameObject.SetActive(true);
                charge.Fill.fillAmount = rechargeProgress;
            }
            else
            {
                charge.Fill.gameObject.SetActive(false);
            }
        }
    }
}