using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaWheel : MonoBehaviour
{

    public Slider staminaSlider;
    public PlayerMovement playerMovementScript;
    public RectTransform sliderRectTransform;
    public CanvasGroup sliderCanvasGroup;

    public Slider usageSlider;

    public Slider StaminaLockedSlider;

    private void Start()
    {
        staminaSlider.maxValue = playerMovementScript.MaxStamina;
        usageSlider.maxValue = playerMovementScript.MaxStamina;
        usageSlider.maxValue = playerMovementScript.baseMaxStamina;
    }

    private void Update()
    {
        UpdateStaminaBar();
        FollowPlayer();
        UpdateSliderVisibility();


    }

    private void UpdateStaminaBar()
    {
        staminaSlider.value = playerMovementScript.currentStamina;
        if (playerMovementScript.isRolling)
        {
            usageSlider.value = playerMovementScript.currentStamina + playerMovementScript.RollStaminaCost;
        }
        else if (Input.GetKey(playerMovementScript.sprintKey))
        {
            usageSlider.value = playerMovementScript.currentStamina + (playerMovementScript.SprintStaminaCostRate/3);
        }


    }

    private void FollowPlayer()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(playerMovementScript.transform.position);
        sliderRectTransform.position = playerScreenPosition + new Vector3(0,75,0);
    }

    private void UpdateSliderVisibility()
    {
        if (playerMovementScript.isUsingStamina || playerMovementScript.currentStamina < playerMovementScript.MaxStamina)
        {
            sliderCanvasGroup.alpha = 1f;
        }
        else
        {
            sliderCanvasGroup.alpha = 0f;
        }
    }
}