using System.Collections;
using UnityEngine;

public class WeaponReloadAnimation : MonoBehaviour
{
    [SerializeField] private Transform leftWeapon;
    [SerializeField] private Transform rightWeapon;

    [SerializeField] private float duration = 1f;

    private Coroutine reloadCoroutine;

    public void Play()
    {
        if (reloadCoroutine != null)
            StopCoroutine(reloadCoroutine);

        reloadCoroutine = StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        Quaternion leftStart = leftWeapon.localRotation;
        Quaternion rightStart = rightWeapon.localRotation;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = Mathf.SmoothStep(0f, 1f, time / duration);
            float angle = Mathf.Lerp(0f, 360f, t);

            leftWeapon.localRotation =
                leftStart * Quaternion.Euler(angle, 0f, 0f);

            rightWeapon.localRotation =
                rightStart * Quaternion.Euler(angle, 0f, 0f);

            yield return null;
        }

        leftWeapon.localRotation = leftStart;
        rightWeapon.localRotation = rightStart;

        reloadCoroutine = null;
    }
}