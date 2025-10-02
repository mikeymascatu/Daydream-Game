using UnityEngine;
using UnityEngine.UI;

public class DamageBarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AttackSystem playerAs;   // Player's Rigidbody2D
    [SerializeField] private Slider slider;          // UI Slider
    [SerializeField] private Image fillImage;        // optional: slider fill
    [SerializeField] private Gradient colorByValue;  // optional: slider colour

    [Header("Gravity Settings")]
    [SerializeField] private int minDamage = -5;
    [SerializeField] private int maxDamage = 5;

    void Update()
    {
        if (!slider) slider = GetComponent<Slider>();

        // Configure slider limits
        slider.minValue = (float)minDamage;
        slider.maxValue = (float)maxDamage;

        if (playerAs != null)
        {
            // Initialise slider with current gravity scale
            slider.value = playerAs.damage;
            UpdateFill();
        }

        // Subscribe to slider events
        slider.onValueChanged.AddListener(HandleSliderChanged);
    }

    private void HandleSliderChanged(float value)
    {
        if (playerAs != null)
            playerAs.damage = (int)value;

        UpdateFill();
    }

    private void UpdateFill()
    {
        if (fillImage && colorByValue != null)
        {
            float t = Mathf.InverseLerp(minDamage, maxDamage, slider.value);
            fillImage.color = colorByValue.Evaluate(t);
        }
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(HandleSliderChanged);
    }
}