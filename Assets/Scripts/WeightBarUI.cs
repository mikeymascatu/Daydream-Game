using UnityEngine;
using UnityEngine.UI;

public class GravityBarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D playerRb;   // Player's Rigidbody2D
    [SerializeField] private Slider slider;          // UI Slider
    [SerializeField] private Image fillImage;        // optional: slider fill
    [SerializeField] private Gradient colorByValue;  // optional: slider colour

    [Header("Gravity Settings")]
    [SerializeField] private float minGravity = -5f;
    [SerializeField] private float maxGravity = 5f;

    void Update()
    {
        if (!slider) slider = GetComponent<Slider>();

        // Configure slider limits
        slider.minValue = minGravity;
        slider.maxValue = maxGravity;

        if (playerRb != null)
        {
            // Initialise slider with current gravity scale
            slider.value = playerRb.gravityScale;
            UpdateFill();
        }

        // Subscribe to slider events
        slider.onValueChanged.AddListener(HandleSliderChanged);
    }

    private void HandleSliderChanged(float value)
    {
        if (playerRb != null)
            playerRb.gravityScale = value;

        UpdateFill();
    }

    private void UpdateFill()
    {
        if (fillImage && colorByValue != null)
        {
            float t = Mathf.InverseLerp(minGravity, maxGravity, slider.value);
            fillImage.color = colorByValue.Evaluate(t);
        }
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(HandleSliderChanged);
    }
}