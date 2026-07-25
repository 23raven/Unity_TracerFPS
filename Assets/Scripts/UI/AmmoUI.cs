using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;

    public void Initialize(Weapon weapon)
    {
        weapon.AmmoChanged += UpdateAmmo;
        weapon.ReloadStarted += ShowReloading;

        UpdateAmmo(weapon.CurrentAmmo, weapon.MagazineSize);
    }

    private void UpdateAmmo(int current, int max)
    {
        ammoText.text = $"{current}/{max}";
    }

    private void ShowReloading()
    {
        ammoText.text = "Reloading...";
    }

    
}