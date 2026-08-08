using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform cameraHandle;
    [SerializeField] private ViewModelMotion viewModelMotion;

    [Header("Look")]
    [SerializeField] private float sensitivity = 150f;

    [Header("Camera Height")]
    [SerializeField] private float standingCameraHeight = 2.0f;
    [SerializeField] private float crouchingCameraHeight = 0.3f;
    [SerializeField] private float cameraLerpSpeed = 12f;

    public Transform CameraHandle => cameraHandle;

    private PlayerManager playerManager;

    private float targetCameraHeight;
    private float xRotation;

    private float previousYaw;
    private float previousPitch;

    private float defaultFov;

    private Coroutine fovCoroutine;
    private Coroutine chromaticCoroutine;
    [Header("Post Processing")]
    [SerializeField] private PostProcessVolume postProcess;
    private ChromaticAberration chromatic;

    public void Initialize(PlayerManager manager)
    {
        playerManager = manager;

        SetCursorLocked(true);

        defaultFov = playerCamera.fieldOfView;
        targetCameraHeight = standingCameraHeight;

        InitializePostProcessing();
    }

    private void InitializePostProcessing()
    {
        if (postProcess == null)
        {
            Debug.LogError("PostProcessVolume is not assigned.");
            return;
        }

        if (!postProcess.profile.TryGetSettings(out chromatic))
        {
            Debug.LogError("Chromatic Aberration is missing from the Post Process Profile.");
            return;
        }

        chromatic.intensity.value = 0f;
    }

    private void Update()
    {
        UpdateCameraHeight();
        Look();
    }

    private void Look()
    {
        Vector2 look = playerManager.Input.Look;

        float mouseX = look.x * sensitivity * Time.deltaTime;
        float mouseY = look.y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraHandle.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

        float yawDelta = Mathf.DeltaAngle(previousYaw, transform.eulerAngles.y);
        float pitchDelta = xRotation - previousPitch;

        viewModelMotion.SetLookInput(new Vector2(yawDelta, pitchDelta));

        previousYaw = transform.eulerAngles.y;
        previousPitch = xRotation;
    }

    public void SetCrouch(bool crouching)
    {
        targetCameraHeight = crouching
            ? crouchingCameraHeight
            : standingCameraHeight;
    }

    private void UpdateCameraHeight()
    {
        Vector3 localPos = cameraHandle.localPosition;

        localPos.y = Mathf.Lerp(
            localPos.y,
            targetCameraHeight,
            cameraLerpSpeed * Time.deltaTime);

        cameraHandle.localPosition = localPos;
    }

    public void PlayFov(
        float targetFov,
        float enterDuration,
        float holdDuration,
        float exitDuration)
    {
        if (fovCoroutine != null)
            StopCoroutine(fovCoroutine);

        fovCoroutine = StartCoroutine(
            FovRoutine(
                targetFov,
                enterDuration,
                holdDuration,
                exitDuration));
    }

    private IEnumerator FovRoutine(
        float targetFov,
        float enterDuration,
        float holdDuration,
        float exitDuration)
    {
        float time = 0f;

        while (time < enterDuration)
        {
            time += Time.deltaTime;

            float t = Mathf.SmoothStep(0f, 1f, time / enterDuration);

            playerCamera.fieldOfView =
                Mathf.Lerp(defaultFov, targetFov, t);

            yield return null;
        }

        playerCamera.fieldOfView = targetFov;

        yield return new WaitForSeconds(holdDuration);

        time = 0f;

        while (time < exitDuration)
        {
            time += Time.deltaTime;

            float t = Mathf.SmoothStep(0f, 1f, time / exitDuration);

            playerCamera.fieldOfView =
                Mathf.Lerp(targetFov, defaultFov, t);

            yield return null;
        }

        playerCamera.fieldOfView = defaultFov;

        fovCoroutine = null;
    }

    public void PlayChromatic(
    float intensity,
    float fadeIn,
    float hold,
    float fadeOut)
    {
        if (chromatic == null)
            return;

        chromatic.intensity.value = 0f;

        if (chromaticCoroutine != null)
            StopCoroutine(chromaticCoroutine);

        chromaticCoroutine = StartCoroutine(
            ChromaticRoutine(
                intensity,
                fadeIn,
                hold,
                fadeOut));
    }


    private IEnumerator AnimateChromatic(
        float from,
        float to,
        float duration)
    {
        if (duration <= 0f)
        {
            chromatic.intensity.value = to;
            yield break;
        }

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = Mathf.SmoothStep(
                0f,
                1f,
                time / duration);

            chromatic.intensity.value = Mathf.Lerp(
                from,
                to,
                t);

            yield return null;
        }

        chromatic.intensity.value = to;
    }

    private IEnumerator ChromaticRoutine(
    float intensity,
    float fadeIn,
    float hold,
    float fadeOut)
    {
        yield return AnimateChromatic(
            0f,
            intensity,
            fadeIn);

        yield return new WaitForSeconds(hold);

        yield return AnimateChromatic(
            intensity,
            0f,
            fadeOut);

        chromatic.intensity.value = 0f;
        chromaticCoroutine = null;
    }

    public void SetCursorLocked(bool locked)
    {
        Cursor.lockState = locked
            ? CursorLockMode.Locked
            : CursorLockMode.None;

        Cursor.visible = !locked;
    }
}

