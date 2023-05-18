using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
 

    public List<Item> Inventory;

    private Item CurrentItemRef;
 
    List<GameObject> objetsEnCollision;


    public GameObject projectile;

    public float TotalWeight = 0f;

    public int bagSize;
    public Transform BagTransform;

    public bool canPickup = true;

    #region GestionDesItemsDansLeSac


    private void Start()
    {
        Inventory = new List<Item>();

        objetsEnCollision = new List<GameObject>();

        canPickup = true;
    }

    private void Update()
    {
        //Ramasser quand on appuie sur E et qu'on est dans la zone de trigger

        if (Input.GetKeyDown(KeyCode.E) && canPickup)
        {

            CollectObjects();

        }

        // Faire apparaitre un objet présent dans le sac

        if ((Input.GetKeyDown(KeyCode.F)) && (Inventory.Count !=0))
        {
            ThrowObjects();
        }




}
    private void LateUpdate()
    {
        BagTransform.localScale = new Vector3(0.7f + bagSize * 0.2f, 0.7f + bagSize * 0.2f, 0.7f + bagSize * 0.2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {

            objetsEnCollision.Add(collision.gameObject);



            //Obtenir les refs de l'objet sur lequel on est actuellement
            
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
            List<GameObject> objetsCopie = new List<GameObject>(objetsEnCollision);

            foreach (GameObject obj in objetsCopie)
            {
                Item item = obj.GetComponent<ItemController>().Item;
                Inventory.Add(item);
                TotalWeight = CalculateTotalWeight();
                bagSize = CalculateBagSize();
                Destroy(obj);
                objetsEnCollision.Remove(obj);
            }
        }
    }



    void ThrowObjects()
    {

        // Prendre un item aléatoire dans le sac
        Item ItemALancer = Inventory[(Random.Range(0, Inventory.Count))];
        Inventory.Remove(ItemALancer);
        projectile = ItemALancer.Object;

        var projectilePosition = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        Instantiate(projectile, projectilePosition, Quaternion.identity);

        TotalWeight = CalculateTotalWeight();
        bagSize = CalculateBagSize();
    }

    #endregion

    #region CalculDuPoids

    //Calcul du total de poids

    public float CalculateTotalWeight()
    {
        float _TotalWeight = 0f;

        foreach (Item item in Inventory)
        {
            _TotalWeight += item.weight;
            
        }

        Debug.Log( "Poids total " + _TotalWeight);
        TotalWeight = _TotalWeight;
        return TotalWeight;

    }


    // Fonction qui calcule dans quel categorie de poids se situe notre sac
    public int CalculateBagSize()
    {
        int _bagSize = 0;

        if (TotalWeight < 5)
        {
            _bagSize = 0;
        }
        else if (TotalWeight >= 5 && TotalWeight < 10)
        {
            _bagSize = 1;
        }
        else if (TotalWeight >= 10 && TotalWeight < 15)
        {
            _bagSize = 2;
        }
        else if (TotalWeight >= 15 && TotalWeight < 20)
        {
            _bagSize = 3;
        }
        else
        {
            _bagSize = 4;
        }

        Debug.Log("taille du sac " + _bagSize);

        bagSize = _bagSize;
        return bagSize;
    }


    #endregion



}
