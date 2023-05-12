using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public List<string> Inventory;

    private bool pickUpAllowed;

    private GameObject currentGameObject;
    private string CurrentObjectRef;

    List<GameObject> objetsEnCollision;

    private void Start()
    {
        Inventory = new List<string>();

        objetsEnCollision = new List<GameObject>();

    }

    private void Update()
    {

        //Ramasser quand on appuie sur E et qu'on est dans la zone de trigger

        if (Input.GetKeyDown(KeyCode.E))
        {

            CollectObjects();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {

            objetsEnCollision.Add(collision.gameObject);



            //Obtenir les refs de l'objet sur lequel on est actuellement
            CurrentObjectRef = collision.gameObject.GetComponent<Collectibles>().propRef;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {

            //Empecher le pickup quand on est sur aucun collectible


            objetsEnCollision.Remove(collision.gameObject);

        }
    }

    void CollectObjects()
    {
        if (objetsEnCollision.Count > 0)
        {
            GameObject premierObjet = objetsEnCollision[0];
            // Faites quelque chose avec le premier objet collectible (par exemple, le détruire)

            //ajouter à la liste de l'inventaire et detruire l'objet
            Destroy(premierObjet);
            Inventory.Add(CurrentObjectRef);


            // Retirez le premier objet de la liste
            objetsEnCollision.Remove(premierObjet);
        }
    }


}
