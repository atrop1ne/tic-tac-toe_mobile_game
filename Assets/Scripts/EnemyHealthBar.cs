using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Slider slider;

    public void InitalizeSlider(int healthPoints)
    {
        slider.maxValue = healthPoints;
        slider.value = healthPoints;
    }

    public void SetSliderValue(int healthPoints)
    {
        slider.value = healthPoints;
    }

    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
}
