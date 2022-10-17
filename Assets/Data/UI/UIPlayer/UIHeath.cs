using CDG.Components;
using UnityEngine;
using UnityEngine.UI;

public class UIHeath : BarValueChanger
{
    [SerializeField] Heath heath;
   
    private void OnEnable()
    {
        heath.OnHealthChange += OnValueChanged;
        Slider.value = 1;
    }

    private void OnDisable() => heath.OnHealthChange -= OnValueChanged;


}
