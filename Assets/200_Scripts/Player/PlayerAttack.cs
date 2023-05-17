using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public KeyCode AttackKey = KeyCode.Space;

    [SerializeField] private Animator Anim;
    [SerializeField] private float meleeSpeed;
    [SerializeField] private float damage;

    public float PlayerDamage = 10f;
    private float animationSpeed;

    private Vector2 lastMoveDirection;

    public PlayerMovement playerMovementScript;
    public PlayerInventory playerInventoryScript;
    public Transform playerTransform;
    private Rigidbody2D rb;

    float timeUntilMelee;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        float animationSpeed = 1.0f; // Remplacez cette valeur par la valeur de votre variable
        Anim.speed = animationSpeed;

    }

    // Update is called once per frame
    void Update()
    {

        if (timeUntilMelee <= 0f) //si la melee est dispo (a le cd)
        {
            if(Input.GetMouseButtonDown(0))
                {

                Anim.SetTrigger("Attack"); // Trigger l'anim d'attaque
                timeUntilMelee = meleeSpeed; //Reset le temps avant de pouvoir melee

            }

        }

        else
        {
            timeUntilMelee -= Time.deltaTime; // Si cd pas dispo decrease le timer
        }

        //Scaling avec la taille du sac
        meleeSpeed = 0.5f + playerInventoryScript.bagSize * 0.3f;
        animationSpeed = 1f / meleeSpeed; // La vitesse de l'animation est inversément proportionnelle à la vitesse d'attaque
        Anim.speed = animationSpeed;
    }

}
