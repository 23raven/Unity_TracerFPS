using System.Collections;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform leftWeapon;
    [SerializeField] private Transform rightWeapon;

    [Header("Settings")]
    [SerializeField] private float recallAngle = -60f;
    [SerializeField] private float reloadRotation = 360f;

    private Quaternion leftDefaultRotation;
    private Quaternion rightDefaultRotation;

    private void Awake()
    {
        leftDefaultRotation = leftWeapon.localRotation;
        rightDefaultRotation = rightWeapon.localRotation;
    }

    private Coroutine animationCoroutine;

    public void PlayReload()
    {
        PlayAnimation(ReloadRoutine(0.75f));
    }

    public void PlayRecall()
    {
        PlayAnimation(RecallRoutine(3f));
    }

    private void PlayAnimation(IEnumerator routine)
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(routine);
    }

    private IEnumerator ReloadRoutine(float duration)
    {

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = Mathf.SmoothStep(0f, 1f, time / duration);
            float angle = Mathf.Lerp(0f, reloadRotation, t);

            leftWeapon.localRotation =
                leftDefaultRotation * Quaternion.Euler(angle, 0f, 0f);

            rightWeapon.localRotation =
                rightDefaultRotation * Quaternion.Euler(angle, 0f, 0f);

            yield return null;
        }

        leftWeapon.localRotation = leftDefaultRotation;
        rightWeapon.localRotation = rightDefaultRotation;

        animationCoroutine = null;
    }

    private IEnumerator RecallRoutine(float duration)
    {

        float halfDuration = duration * 0.5f;

        float time = 0f;

        // Опустить оружие
        while (time < halfDuration)
        {
            time += Time.deltaTime;

            float t = Mathf.SmoothStep(0f, 1f, time / halfDuration);
            float angle = Mathf.Lerp(0f, recallAngle, t);

            leftWeapon.localRotation =
                leftDefaultRotation * Quaternion.Euler(angle, 0f, 0f);

            rightWeapon.localRotation =
                rightDefaultRotation * Quaternion.Euler(angle, 0f, 0f);

            yield return null;
        }

        time = 0f;

        // Вернуть обратно
        while (time < halfDuration)
        {
            time += Time.deltaTime;

            float t = Mathf.SmoothStep(0f, 1f, time / halfDuration);
            float angle = Mathf.Lerp(recallAngle, 0f, t);

            leftWeapon.localRotation =
                leftDefaultRotation * Quaternion.Euler(angle, 0f, 0f);

            rightWeapon.localRotation =
                rightDefaultRotation * Quaternion.Euler(angle, 0f, 0f);

            yield return null;
        }

        leftWeapon.localRotation = leftDefaultRotation;
        rightWeapon.localRotation = rightDefaultRotation;

        animationCoroutine = null;
    }
}