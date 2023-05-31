using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterCD : MonoBehaviour
{
    public float delay = 10f; // Temps de délai en secondes
    public float blinkInterval = 0.5f; // Intervalle de clignotement initial en secondes
    public float blinkSpeedIncrease = 0.1f; // Augmentation de la vitesse de clignotement à chaque cycle

    private Renderer[] objectRenderers;
    private bool isBlinking = false;

    private void Start()
    {
        objectRenderers = GetComponentsInChildren<Renderer>();
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        isBlinking = true;
        float currentBlinkInterval = blinkInterval;

        while (isBlinking)
        {
            foreach (Renderer renderer in objectRenderers)
            {
                renderer.enabled = !renderer.enabled;
            }
            yield return new WaitForSeconds(currentBlinkInterval);
            currentBlinkInterval -= blinkSpeedIncrease;

            if (currentBlinkInterval <= 0f)
            {
                currentBlinkInterval = blinkInterval; // Réinitialise l'intervalle de clignotement
                isBlinking = false;
            }
        }

        Destroy(gameObject);
    }
}