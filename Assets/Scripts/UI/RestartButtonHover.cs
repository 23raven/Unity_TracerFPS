using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RestartButtonHover : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private TMP_Text buttonText;

    [Header("Hover")]
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private float scaleSpeed = 10f;

    private Vector3 normalScale;
    private Vector3 targetScale;

    private void Awake()
    {
        normalScale = buttonText.transform.localScale;
        targetScale = normalScale;
    }

    private void Update()
    {
        buttonText.transform.localScale = Vector3.Lerp(
            buttonText.transform.localScale,
            targetScale,
            scaleSpeed * Time.unscaledDeltaTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = normalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = normalScale;
    }
}