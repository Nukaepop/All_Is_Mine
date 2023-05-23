using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
 

    public List<Item> Inventory;

    private Item CurrentItemRef;
 
    List<GameObject> objetsEnCollision;
    private GameObject nearestObject;

    public GameObject projectile;

    public float TotalWeight = 0f;

    public int bagSize;
    public Transform BagTransform;

    public bool canPickup = true;
    private bool isCollecting = false;

    #region GestionDesItemsDansLeSac


    private void Start()
    {
        Inventory = new List<Item>();

        objetsEnCollision = new List<GameObject>();

        canPickup = true;
    }

    private void Update()
    {
        // Mettre � jour l'objet le plus proche
        UpdateNearestObject();

        // Ramasser quand on appuie sur E et qu'on est dans la zone de trigger
        if (Input.GetKeyDown(KeyCode.E) && canPickup)
        {
            CollectObjects();
        }

        // Faire appara�tre un objet pr�sent dans le sac
        if (Input.GetKeyDown(KeyCode.F) && Inventory.Count != 0)
        {
            LoseItems();
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

            // Si aucun objet n'est en cours de collecte, mettez � jour l'objet le plus proche
            if (!isCollecting)
            {
                UpdateNearestObject();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            objetsEnCollision.Remove(collision.gameObject);

            // Si aucun objet n'est en cours de collecte, mettez � jour l'objet le plus proche
            if (!isCollecting)
            {
                UpdateNearestObject();
            }
        }
    }

    void CollectObjects()
    {
        if (nearestObject != null)
        {
            // D�finir la variable isCollecting � true pour indiquer qu'un objet est en cours de collecte
            isCollecting = true;

            Item item = nearestObject.GetComponent<ItemController>().Item;
            Inventory.Add(item);
            TotalWeight = CalculateTotalWeight();
            bagSize = CalculateBagSize();
            Destroy(nearestObject);
            objetsEnCollision.Remove(nearestObject);

            // Mettre � jour l'objet le plus proche
            UpdateNearestObject();

            // R�tablir la variable isCollecting � false apr�s la collecte
            isCollecting = false;
        }
    }

    private void UpdateNearestObject()
    {
        float nearestDistance = Mathf.Infinity;
        nearestObject = null;

        foreach (GameObject obj in objetsEnCollision)
        {
            float distance = Vector2.Distance(transform.position, obj.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObject = obj;
            }
        }
    }


    public void LoseItems()
    {

        // Prendre un item al�atoire dans le sac
        Item ItemALancer = Inventory[(Random.Range(0, Inventory.Count))];
        Inventory.Remove(ItemALancer);
        projectile = ItemALancer.Object;

        float raycastDistance = 2f; // Distance maximale pour le raycast
        int maxAttempts = 10; // Nombre maximum de tentatives pour trouver une position sans collision
        int attemptCount = 0; // Compteur de tentatives

        // Variables pour la nouvelle position al�atoire
        Vector2 newPosition = Vector2.zero;
        bool foundValidPosition = false;

        while (!foundValidPosition && attemptCount < maxAttempts)
        {
            // G�n�rer une nouvelle position al�atoire autour du joueur
            newPosition = new Vector2(transform.position.x + Random.Range(-2f, 2f), transform.position.y + Random.Range(-2f, 2f));

            // Effectuer un raycast pour v�rifier s'il y a une collision avec un mur ou un obstacle
            RaycastHit2D hit = Physics2D.Raycast(newPosition, Vector2.zero, raycastDistance);

            // Si le raycast ne d�tecte pas de collision, nous avons trouv� une position valide
            if (hit.collider == null)
            {
                foundValidPosition = true;
            }

            attemptCount++;
        }

        // Si une position valide a �t� trouv�e, instancier l'objet � cette position
        if (foundValidPosition)
        {
            Instantiate(projectile, newPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("Impossible de trouver une position sans collision pour l'instanciation de l'objet.");
        }

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
