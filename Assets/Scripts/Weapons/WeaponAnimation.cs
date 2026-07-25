using System.Collections;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform leftWeapon;
    [SerializeField] private Transform rightWeapon;

    [Header("Reload")]
    [SerializeField] private float reloadRotation = 360f;

    [Header("Recall Left")]
    [SerializeField] private Vector3 leftRecallPosition = new(-0.5f, 0.7f, 2f);
    [SerializeField] private Vector3 leftRecallRotation = new(-60f, 30f, 0f);

    [Header("Recall Right")]
    [SerializeField] private Vector3 rightRecallPosition = new(0.5f, 0.7f, 2f);
    [SerializeField] private Vector3 rightRecallRotation = new(-60f, -30f, 0f);

    private Vector3 leftDefaultPosition;
    private Vector3 rightDefaultPosition;

    private Quaternion leftDefaultRotation;
    private Quaternion rightDefaultRotation;

    private Coroutine animationCoroutine;

    private void Awake()
    {
        leftDefaultPosition = leftWeapon.localPosition;
        rightDefaultPosition = rightWeapon.localPosition;

        leftDefaultRotation = leftWeapon.localRotation;
        rightDefaultRotation = rightWeapon.localRotation;
    }

    public void PlayReload()
    {
        PlayAnimation(ReloadRoutine(0.75f));
    }

    public void PlayRecall()
    {
        PlayAnimation(RecallRoutine(1.2f));
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

            Quaternion rotation = Quaternion.Euler(angle, 0f, 0f);

            leftWeapon.localRotation = leftDefaultRotation * rotation;
            rightWeapon.localRotation = rightDefaultRotation * rotation;

            yield return null;
        }

        ResetWeapons();
    }

    private IEnumerator RecallRoutine(float duration)
    {
        float halfDuration = duration * 0.5f;

        float time = 0f;

        // Войти в позу
        while (time < halfDuration)
        {
            time += Time.deltaTime;

            float t = Mathf.SmoothStep(0f, 1f, time / halfDuration);

            leftWeapon.localPosition = Vector3.Lerp(
                leftDefaultPosition,
                leftRecallPosition,
                t);

            rightWeapon.localPosition = Vector3.Lerp(
                rightDefaultPosition,
                rightRecallPosition,
                t);

            leftWeapon.localRotation = Quaternion.Lerp(
                leftDefaultRotation,
                Quaternion.Euler(leftRecallRotation),
                t);

            rightWeapon.localRotation = Quaternion.Lerp(
                rightDefaultRotation,
                Quaternion.Euler(rightRecallRotation),
                t);

            yield return null;
        }

        time = 0f;

        // Вернуться обратно
        while (time < halfDuration)
        {
            time += Time.deltaTime;

            float t = Mathf.SmoothStep(0f, 1f, time / halfDuration);

            leftWeapon.localPosition = Vector3.Lerp(
                leftRecallPosition,
                leftDefaultPosition,
                t);

            rightWeapon.localPosition = Vector3.Lerp(
                rightRecallPosition,
                rightDefaultPosition,
                t);

            leftWeapon.localRotation = Quaternion.Lerp(
                Quaternion.Euler(leftRecallRotation),
                leftDefaultRotation,
                t);

            rightWeapon.localRotation = Quaternion.Lerp(
                Quaternion.Euler(rightRecallRotation),
                rightDefaultRotation,
                t);

            yield return null;
        }

        ResetWeapons();
    }

    private void ResetWeapons()
    {
        leftWeapon.localPosition = leftDefaultPosition;
        rightWeapon.localPosition = rightDefaultPosition;

        leftWeapon.localRotation = leftDefaultRotation;
        rightWeapon.localRotation = rightDefaultRotation;

        animationCoroutine = null;
    }
}