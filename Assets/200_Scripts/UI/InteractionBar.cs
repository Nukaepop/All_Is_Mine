using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionBar : MonoBehaviour
{
    [SerializeField] private Slider interactionSlider;

    public void UpdateInteractionBar(float currentValue, float maxValue)
    {
        interactionSlider.value = currentValue / maxValue;
    }

    public void ShowInteractionBar()
    {
        interactionSlider.gameObject.SetActive(true);
    }

    public void HideInteractionBar()
    {
        interactionSlider.gameObject.SetActive(false);
    }
}
