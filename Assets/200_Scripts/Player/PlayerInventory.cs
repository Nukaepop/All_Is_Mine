using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
 

    public List<Item> Inventory;

    private Item CurrentItemRef;
 
    List<GameObject> objetsEnCollision;
    public GameObject nearestObject;



    public GameObject projectile;


    public Image Enclume1, Enclume2, Enclume3;
    public float TotalWeight = 0f;

    public int bagSize;
    public Transform BagTransform;

    public bool canPickup = true;
    private bool isCollecting = false;


    private bool isInteracting = false;
    public float interactionTime = 2.0f; // Durée d'interaction requise en secondes
    private float currentInteractionTime = 0.0f;

    public GameObject textMeshProObjectWeight;
    public GameObject textMeshProObjectCount;
    public TextMeshProUGUI textMeshProComponentWeight;
    public TextMeshProUGUI textMeshProComponentCount;

    public GameObject floatingTextPrefab;

    public InteractionBar interactionBarScript;


    #region GestionDesItemsDansLeSac


    private void Start()
    {
        Inventory = new List<Item>();

        objetsEnCollision = new List<GameObject>();

        canPickup = true;


        textMeshProComponentCount = textMeshProObjectCount.GetComponent<TextMeshProUGUI>();
        textMeshProComponentWeight = textMeshProObjectWeight.GetComponent<TextMeshProUGUI>();

        textMeshProComponentCount.text = Inventory.Count.ToString();
        textMeshProComponentWeight.text = TotalWeight.ToString();

        if(Inventory.Count == 0)
        {
            textMeshProComponentCount.color = Color.red;
        }
        else
        {
            textMeshProComponentCount.color = Color.blue;
        }
        if(TotalWeight <= 0)
        {
            textMeshProComponentWeight.color = Color.red;
        }
        else
        {
            textMeshProComponentWeight.color = Color.white;
        }

        interactionBarScript.HideInteractionBar();
    }

    private void Update()
    {
        // Mettre à jour l'objet le plus proche
        UpdateNearestObject();

        // Ramasser quand on appuie sur E et qu'on est dans la zone de trigger
        if (Input.GetKeyDown(KeyCode.E) && canPickup)
        {
            StartInteracting();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            StopInteracting();
        }

        if (isInteracting)
        {
            currentInteractionTime += Time.deltaTime;
            interactionBarScript.UpdateInteractionBar(currentInteractionTime, interactionTime);

            if (currentInteractionTime >= interactionTime)
            {
                CollectObjects();
                StopInteracting();
            }
        }

        void StartInteracting()
        {
            if (!isInteracting && nearestObject != null)
            {
                isInteracting = true;
                currentInteractionTime = 0.0f;
                interactionBarScript.ShowInteractionBar();
            }
        }

        void StopInteracting()
        {
            if (isInteracting )
            {
                isInteracting = false;
                currentInteractionTime = 0.0f;
                interactionBarScript.HideInteractionBar();
            }
        }


        // Faire apparaître un objet présent dans le sac
        if (Input.GetKeyDown(KeyCode.F) && Inventory.Count != 0)
        {
            LoseItems();
        }

        if(bagSize >=1)
        {
            Enclume1.color = new Color(0.85f, 0.85f, 0.85f);
        }
        else
        {
            Enclume1.color = new Color(0.15f,0.15f,0.15f);
        }
        if (bagSize >= 2)
        {
            Enclume2.color = new Color(0.85f, 0.85f, 0.85f);
        }
        else
        {
            Enclume2.color = new Color(0.15f, 0.15f, 0.15f);
        }
        if (bagSize >= 3)
        {
            Enclume3.color = new Color(0.85f, 0.85f, 0.85f);
        }
        else
        {
            Enclume3.color = new Color(0.15f, 0.15f, 0.15f);
        }




    }
    private void LateUpdate()
    {
        BagTransform.localScale = new Vector3(1f + bagSize * 0.2f, 1f + bagSize * 0.2f, 1f + bagSize * 0.2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            objetsEnCollision.Add(collision.gameObject);

            // Si aucun objet n'est en cours de collecte, mettez à jour l'objet le plus proche
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

            // Si aucun objet n'est en cours de collecte, mettez à jour l'objet le plus proche
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

            // Définir la variable isCollecting à true pour indiquer qu'un objet est en cours de collecte
            isCollecting = true;

            Item item = nearestObject.GetComponent<ItemController>().Item;
            Inventory.Add(item);

            textMeshProComponentCount.text = Inventory.Count.ToString();

            if (Inventory.Count == 0)
            {
                textMeshProComponentCount.color = Color.red;
            }
            else
            {
                textMeshProComponentCount.color = Color.blue;
            }

            TotalWeight = CalculateTotalWeight();
            bagSize = CalculateBagSize();
            Destroy(nearestObject);
            objetsEnCollision.Remove(nearestObject);

            // Mettre à jour l'objet le plus proche
            UpdateNearestObject();

            // Rétablir la variable isCollecting à false après la collecte
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


        // Prendre un item aléatoire dans le sac
        Item ItemALancer = Inventory[(Random.Range(0, Inventory.Count))];
        Inventory.Remove(ItemALancer);
        projectile = ItemALancer.Object;

        textMeshProComponentCount.text = Inventory.Count.ToString();

        if (Inventory.Count == 0)
        {
            textMeshProComponentCount.color = Color.red;
        }
        else
        {
            textMeshProComponentCount.color = Color.blue;
        }

        float raycastDistance = 2f; // Distance maximale pour le raycast
        int maxAttempts = 10; // Nombre maximum de tentatives pour trouver une position sans collision
        int attemptCount = 0; // Compteur de tentatives

        // Variables pour la nouvelle position aléatoire
        Vector2 newPosition = Vector2.zero;
        bool foundValidPosition = false;

        while (!foundValidPosition && attemptCount < maxAttempts)
        {
            // Générer une nouvelle position aléatoire autour du joueur
            newPosition = new Vector2(transform.position.x + Random.Range(-2f, 2f), transform.position.y + Random.Range(-2f, 2f));

            // Effectuer un raycast pour vérifier s'il y a une collision avec un mur ou un obstacle
            RaycastHit2D hit = Physics2D.Raycast(newPosition, Vector2.zero, raycastDistance);

            // Si le raycast ne détecte pas de collision, nous avons trouvé une position valide
            if (hit.collider == null)
            {
                foundValidPosition = true;
            }

            attemptCount++;
        }

        // Si une position valide a été trouvée, instancier l'objet à cette position
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

        textMeshProComponentWeight.text = TotalWeight.ToString();

        if (TotalWeight <= 0)
        {
            textMeshProComponentWeight.color = Color.red;
        }
        else
        {
            textMeshProComponentWeight.color = Color.white;
        }


        return TotalWeight;

    }


    // Fonction qui calcule dans quel categorie de poids se situe notre sac
    public int CalculateBagSize()
    {
        int _bagSize = 0;

        if (TotalWeight < 10)
        {
            _bagSize = 0;
        }
        else if (TotalWeight >= 10 && TotalWeight < 30)
        {
            _bagSize = 1;
        }
        else if (TotalWeight >= 30 && TotalWeight < 50)
        {
            _bagSize = 2;
        }
    /*    else if (TotalWeight >= 15 && TotalWeight < 20)
        {
            _bagSize = 3;
        }*/
        else
        {
            _bagSize = 3;
        }

        Debug.Log("taille du sac " + _bagSize);

        bagSize = _bagSize;
        return bagSize;
    }



    #endregion

    public void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = text;
        }
    }

}
