using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdateInteractionWheel(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
    public void ShowSlider()
    {
        slider.gameObject.SetActive(true);
    }

    public void HideSlider()
    {
        slider.gameObject.SetActive(false);
    }

}
