using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapons : MonoBehaviour
{

    public int weaponDamage; // Le nombre d'aobjets que le joueur va perdre
    private int compteur = 0;

    public PlayerInventory playerInventoryScript;
    public PlayerMovement playerMovementScript;



    void start()
    {
            
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !playerMovementScript.isInvincible)
        {

            Debug.Log("Collisions avec le joueur");
            if (playerInventoryScript.Inventory.Count > 0) // S'il y a des objets dans le sac
            {
                int damage = weaponDamage;
                while (compteur < damage && playerInventoryScript.Inventory.Count > 0)
                {
                    Debug.Log("Perte d'objet");
                    playerInventoryScript.LoseItems();
                    compteur++;
                }
                compteur = 0;
                playerMovementScript.isInvincible = true;
            }
            else // Si le sac est vide
            {
                Debug.Log("DEAD");
            }
        }
    }

}