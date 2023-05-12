using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public List<Item> Inventory;

    private bool pickUpAllowed;

    private GameObject currentGameObject;
    private string CurrentObjectRef;
    private Item CurrentItemRef;
 
    List<GameObject> objetsEnCollision;


    public GameObject projectile;

    private void Start()
    {
        Inventory = new List<Item>();

        objetsEnCollision = new List<GameObject>();

    }

    private void Update()
    {

        //Ramasser quand on appuie sur E et qu'on est dans la zone de trigger

        if (Input.GetKeyDown(KeyCode.E))
        {

            CollectObjects();

        }

        // Faire apparaitre un objet présent dans le sac

        if ((Input.GetKeyDown(KeyCode.F)) && (Inventory.Count !=0))
        {

            // Prendre un item aléatoire dans le sac
        Item ItemALancer = Inventory[(Random.Range(0, Inventory.Count))];
            Inventory.Remove(ItemALancer);
        projectile = ItemALancer.Object;

            var projectilePosition = new Vector2(Random.Range(-5,5), Random.Range(-5,5));
            Instantiate(projectile,projectilePosition, Quaternion.identity);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {

            objetsEnCollision.Add(collision.gameObject);



            //Obtenir les refs de l'objet sur lequel on est actuellement
            CurrentObjectRef = collision.gameObject.GetComponent<Collectibles>().propRef;
            CurrentItemRef = collision.gameObject.GetComponent<ItemController>().Item;
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
            Inventory.Add(CurrentItemRef);

            // Retirez le premier objet de la liste
            objetsEnCollision.Remove(premierObjet);
        }
    }


}
