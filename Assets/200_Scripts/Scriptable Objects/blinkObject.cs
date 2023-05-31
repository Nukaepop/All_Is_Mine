using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkObject : MonoBehaviour
{
    public float initialBlinkInterval = 0.5f;
    public float blinkIntervalIncrease = 0.25f;
    public float blinkDuration = 0.1f;

    private Renderer objectRenderer;
    private WaitForSeconds blinkWait;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        float blinkInterval = initialBlinkInterval;

        while (true)
        {
            objectRenderer.enabled = !objectRenderer.enabled;
            yield return new WaitForSeconds(blinkDuration);
            objectRenderer.enabled = !objectRenderer.enabled;

            yield return blinkWait ?? (blinkWait = new WaitForSeconds(blinkInterval));

            blinkInterval += blinkIntervalIncrease;
            blinkDuration += 0.05f;
        }
    }
}