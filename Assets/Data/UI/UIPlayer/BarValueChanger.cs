using UnityEngine;
using UnityEngine.UI;

public abstract class BarValueChanger : MonoBehaviour
{
    [SerializeField] protected Slider Slider;

    public void OnValueChanged(int value, int maxValue)
    {
        Slider.value = (float)value / maxValue;
    }
}
