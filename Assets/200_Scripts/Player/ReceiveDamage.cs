using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReceiveDamage : MonoBehaviour
{

    public PlayerInventory playerInventoryScript;
    public PlayerMovement playerMovementScript;
    public Animator playerAnimator;

    private int compteur = 0;
    public int itemsLost = 0;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !playerMovementScript.isInvincible)
        {
            playerAnimator.SetTrigger("DamageReceived");
            Debug.Log("Collisions avec le joueur");
            if (playerInventoryScript.Inventory.Count > 0) // S'il y a des objets dans le sac
            {

                int damage = 1;
                while (compteur < damage && playerInventoryScript.Inventory.Count > 0)
                {
                    Debug.Log("Perte d'objet");
                    playerInventoryScript.LoseItems();
                    compteur++;
                    itemsLost++;
                }
                playerInventoryScript.ShowDamage(itemsLost.ToString());
                compteur = 0;
                itemsLost = 0;
                playerMovementScript.isInvincible = true;
            }
            else // Si le sac est vide
            {
                SceneManager.LoadScene("Level1");
            }
        }
    }
}