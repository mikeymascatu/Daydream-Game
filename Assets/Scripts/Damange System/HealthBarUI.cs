using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] PlayerHealth target;   // drag your Player here
    [SerializeField] Slider slider;         // drag the Slider here
    [SerializeField] Image fillImage;       // optional: the Fill image
    [SerializeField] Gradient colorByHealth; // optional Gradient (0=red, 1=green)

    void Awake()
    {
        if (!slider) slider = GetComponent<Slider>();
        Bind(target);
    }

    public void Bind(PlayerHealth t)
    {
        if (target != null) target.OnHealthChanged -= HandleHealth;
        target = t;
        if (target != null)
        {
            slider.maxValue = target.Max;
            slider.value = target.Current;
            UpdateFill();
            target.OnHealthChanged += HandleHealth;
        }
    }

    void HandleHealth(int current, int max)
    {
        slider.maxValue = max;
        slider.value = current;
        UpdateFill();
    }

    void UpdateFill()
    {
        if (fillImage && colorByHealth != null)
            fillImage.color = colorByHealth.Evaluate(slider.normalizedValue);
    }

    void OnDestroy()
    {
        if (target != null) target.OnHealthChanged -= HandleHealth;
    }
}