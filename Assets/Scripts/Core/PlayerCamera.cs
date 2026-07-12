using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraHandle;
    [SerializeField] private float sensitivity = 150f;
    [SerializeField] private float standingCameraHeight = 2.0f;
    [SerializeField] private float crouchingCameraHeight = 0.3f;
    [SerializeField] private float cameraLerpSpeed = 12f;
    public Transform CameraHandle => cameraHandle;

    private float targetCameraHeight;

    private PlayerManager playerManager;

    private float xRotation;

    public void Initialize(PlayerManager manager)
    {
        playerManager = manager;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        targetCameraHeight = standingCameraHeight;
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

}