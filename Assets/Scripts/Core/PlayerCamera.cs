using System.Collections;
using UnityEngine;

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

    [Header("Recall FOV")]
    [SerializeField] private float recallFov = 70f;
    [SerializeField] private float recallFovEnterDuration = 0.15f;
    [SerializeField] private float recallFovHoldDuration = 0.60f;
    [SerializeField] private float recallFovExitDuration = 0.15f;

    public Transform CameraHandle => cameraHandle;

    private PlayerManager playerManager;

    private float targetCameraHeight;
    private float xRotation;

    private float previousYaw;
    private float previousPitch;

    private float defaultFov;
    private Coroutine fovCoroutine;

    public void Initialize(PlayerManager manager)
    {
        playerManager = manager;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        targetCameraHeight = standingCameraHeight;

        defaultFov = playerCamera.fieldOfView;
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

    public void PlayRecallFov()
    {
        if (fovCoroutine != null)
            StopCoroutine(fovCoroutine);

        fovCoroutine = StartCoroutine(RecallFovRoutine());
    }

    private IEnumerator RecallFovRoutine()
    {
        float time = 0f;

        while (time < recallFovEnterDuration)
        {
            time += Time.deltaTime;

            float t = Mathf.SmoothStep(0f, 1f, time / recallFovEnterDuration);

            playerCamera.fieldOfView =
                Mathf.Lerp(defaultFov, recallFov, t);

            yield return null;
        }

        playerCamera.fieldOfView = recallFov;

        yield return new WaitForSeconds(recallFovHoldDuration);

        time = 0f;

        while (time < recallFovExitDuration)
        {
            time += Time.deltaTime;

            float t = Mathf.SmoothStep(0f, 1f, time / recallFovExitDuration);

            playerCamera.fieldOfView =
                Mathf.Lerp(recallFov, defaultFov, t);

            yield return null;
        }

        playerCamera.fieldOfView = defaultFov;

        fovCoroutine = null;
    }
}