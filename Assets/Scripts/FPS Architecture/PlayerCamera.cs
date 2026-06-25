using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraHandle;
    [SerializeField] private float sensitivity = 150f;

    private PlayerManager playerManager;

    private float xRotation;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
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
}