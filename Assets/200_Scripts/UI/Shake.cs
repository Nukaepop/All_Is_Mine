using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float shakeIntensity = 0.7f; // Intensit� du tremblement
    public float shakeDuration = 0.5f; // Dur�e du tremblement

    private Vector3 originalPosition;
    private float shakeTimer;

    public PlayerInventory inventoryScript;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (inventoryScript.Inventory.Count == 0)
        {
            StartShake();
        }
            if (shakeTimer > 0f)
            {
                // Calculer une translation al�atoire
                float offsetX = Random.Range(-4f, 4f) * shakeIntensity;
                float offsetY = Random.Range(-4f, 4f) * shakeIntensity;
                Vector3 randomOffset = new Vector3(offsetX, offsetY, 0f);

                // Appliquer la translation au transform de l'objet
                transform.position = originalPosition + randomOffset;

                // R�duire le timer de tremblement
                shakeTimer -= Time.deltaTime;
            }
            else
            {
                // R�initialiser la position de l'objet lorsque le tremblement est termin�
                transform.position = originalPosition;
            }
        }

        void StartShake()
        {
            // D�buter le tremblement en r�initialisant le timer
            shakeTimer = shakeDuration;
        }
}