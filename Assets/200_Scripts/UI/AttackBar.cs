using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBar : MonoBehaviour
{
    [SerializeField] private Slider atkSlider;

    public void UpdateAttackBar(float currentValue, float maxValue)
    {
        atkSlider.value = currentValue / maxValue;
    }

    public void ShowAttackBar()
    {
        atkSlider.gameObject.SetActive(true);
    }

    public void HideAttackBar()
    {
        atkSlider.gameObject.SetActive(false);
    }
}
