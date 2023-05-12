using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public List<string> Inventory;
    private bool pickUpAllowed;
    private GameObject currentGameObject;
    private string CurrentObjectRef;

    private void Start()
    {
        Inventory = new List<string>();
    }

    private void Update()
    {

        //Ramasser quand on appuie sur E et qu'on est dans la zone de trigger

        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            //ajouter à la liste de l'inventaire et detruire l'objet
            Inventory.Add(CurrentObjectRef);
            Destroy(currentGameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            pickUpAllowed = true;

            //Obtenir les refs de l'objet sur lequel on est actuellement
            CurrentObjectRef = collision.gameObject.GetComponent<Collectibles>().propRef;
            currentGameObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {

            //Empecher le pickup quand on est sur aucun collectible
            pickUpAllowed = false;
        }
    }

}
