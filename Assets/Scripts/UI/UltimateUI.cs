using UnityEngine;
using UnityEngine.UI;

public class UltimateUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image fillImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private RectTransform root;

    [Header("Colors")]
    [SerializeField] private Color chargingColor = new(0.5f, 0.5f, 0.5f);

    [Header("Animation")]
    [SerializeField] private float pulseScale = 1.1f;
    [SerializeField] private float pulseDuration = 0.08f;

    private UltimateCharge ultimateCharge;
    private float currentPercent = -1f;

    private bool wasReady;
    private Coroutine pulseCoroutine;

    [SerializeField] private Sprite iconGray;
    [SerializeField] private Sprite iconReady;

    private void Awake()
    {
        fillImage.type = Image.Type.Filled;
        fillImage.fillMethod = Image.FillMethod.Radial360;
    }

    public void Initialize(UltimateCharge ultimateCharge)
    {
        this.ultimateCharge = ultimateCharge;
    }

    private void Update()
    {
        if (ultimateCharge == null)
            return;

        float percent =
    ultimateCharge.CurrentCharge / ultimateCharge.MaxCharge;

        if (Mathf.Approximately(percent, currentPercent))
            return;

        currentPercent = percent;
        SetCharge(percent);
    }

    public void SetCharge(float percent)
    {
        percent = Mathf.Clamp01(percent);

        fillImage.fillAmount = percent;

        bool ready = Mathf.Approximately(percent, 1f);

        iconImage.sprite = ready ? iconReady : iconGray;

        if (ready && !wasReady)
        {
            if (pulseCoroutine != null)
                StopCoroutine(pulseCoroutine);

            pulseCoroutine = StartCoroutine(PulseRoutine());
        }

        wasReady = ready;
    }

    private System.Collections.IEnumerator PulseRoutine()
    {
        Vector3 start = Vector3.one;
        Vector3 target = Vector3.one * pulseScale;

        float t = 0f;

        while (t < pulseDuration)
        {
            t += Time.deltaTime;
            root.localScale = Vector3.Lerp(start, target, t / pulseDuration);
            yield return null;
        }

        t = 0f;

        while (t < pulseDuration)
        {
            t += Time.deltaTime;
            root.localScale = Vector3.Lerp(target, start, t / pulseDuration);
            yield return null;
        }

        root.localScale = start;
    }
}