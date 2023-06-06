using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [Header("Keybinds")]

    private KeyCode MoveUpKey = KeyCode.Z;
    private KeyCode MoveLeftKey = KeyCode.Q;
    private KeyCode MoveRightKey = KeyCode.D;
    private KeyCode MoveDownKey = KeyCode.S;

    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode rollKey = KeyCode.Space;


    [Header("Refs")]

    public PlayerInventory InventoryScript;
    public Animator animator;

    [Header("Walk")]

    private float moveSpeed;
    public float WalkSpeed;

    public ParticleSystem dust;

    [Header("Sprint")]

    public float SprintSpeed;
    public float SprintStaminaCostRate;


    [Header("Roll")]

    public float rollDuration;
    private float rollTimer = 0f;
    public bool isRolling = false;
    private Vector2 dashDirection;
    public float dashSpeed;
    public float baseDashSpeed;
    public float RollStaminaCost;


    [Header("General")]

    public Rigidbody2D rb;
    public Transform characterTransform;

    public SpriteRenderer spriteRenderer;
    public Color baseColor;
    public Color currentColor;

    public WeaponParent weaponParentScript;

    public Vector2 moveDirection;

    public bool isFacingRight;

    [Header("Stamina")]

    public float MaxStamina;
    public float currentStamina;
    public float staminaRecoveryRate;
    public float baseMaxStamina;

    private bool hasStamina = true;
    public bool isUsingStamina = false;

    public Image staminaBar;

    public bool showingBar = false;
    private float delay = 0.5f;

    [Header("Invincibility")]

    public bool isInvincible = false;
    public float invincibilityDuration;
    public float invincibilityTimer;


    [Header("MovementState")]

    public MovementState State;
    public enum MovementState
    {
        Walking,
        Sprinting,
        Rolling
    }





    private void StateHandler()
    {
        if(isRolling)
        {
            State = MovementState.Rolling;
            animator.SetBool("isRolling", true);

            isUsingStamina = true;
        }
        else if(Input.GetKey(sprintKey) && hasStamina)
        {
            State = MovementState.Sprinting;
            moveSpeed = SprintSpeed;
            spriteRenderer.color = Color.magenta;

            currentStamina -= (SprintStaminaCostRate * Time.deltaTime);
            isUsingStamina = true;

            animator.SetBool("isRolling", false);
        }
        else
        {

            State = MovementState.Walking;
            moveSpeed = WalkSpeed;
            spriteRenderer.color = baseColor;

            isUsingStamina = false;
            if (rb.velocity == Vector2.zero)
            {
                animator.SetBool("isWalking", false);
            }
            else
            {
                animator.SetBool("isWalking", true);
            }

            animator.SetBool("isRolling", false);
        }
    }

    private void Start()
    {
        baseColor = spriteRenderer.color;
        currentStamina = MaxStamina;
        hasStamina = true;

        dashSpeed = baseDashSpeed;

        animator.SetBool("isRolling", false);
    }

    // Update is called once per frame
    void Update()
    {
        //processing inputs
        ProcessInputs();
        StateHandler();

        if (isInvincible)
        {
            invincibilityTimer += Time.deltaTime;
            if (invincibilityTimer >= invincibilityDuration)
            {
                isInvincible = false;
                invincibilityTimer = 0f;
            }
        }

        if (currentStamina <= 0)
        {
            hasStamina = false;
        }

        if (!isUsingStamina)
        {
            currentStamina += (staminaRecoveryRate * Time.deltaTime);
        }

         if(currentStamina>=MaxStamina)
        {

            currentStamina = MaxStamina;

            if (!hasStamina)
            {
                showingBar = true;
                hasStamina = true;
                // Restaurer la couleur normale de la barre de stamina
                staminaBar.color = Color.green;
                StartCoroutine(Delay());

            }
        }



        MaxStamina = baseMaxStamina - InventoryScript.bagSize * 15;

        if(!hasStamina)
        {
            // Faire clignoter la barre de stamina en rouge
            float alpha = Mathf.PingPong(Time.time * 3f, 1f); // Contrôle la transparence (alpha) en utilisant une fonction de ping-pong
            staminaBar.color = new Color(1f, 0f, 0f, alpha); // Définit la couleur de la barre de stamina en rouge avec l'alpha calculé
        }
        else
        {
            staminaBar.color = new Color(0f, 1f, 0f, 1);
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        showingBar = false;
    }

    private void FixedUpdate()
    {
        //Physics calculations
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if(Input.GetKeyDown(rollKey) && !isRolling && hasStamina)
        {
            dashDirection = GetDashDirection();
            StartCoroutine(PerformRoll());
        }


    }




    private IEnumerator PerformRoll()
    {
        isRolling = true;
        currentStamina -= RollStaminaCost;

        // Effectuer les actions nécessaires pour la roulade (animations, mouvements, etc.)
        isInvincible = true;
        InventoryScript.canPickup = false;




        rollTimer = 0f;
        while (rollTimer < rollDuration)
        {
            // Diminuer la vitesse en fonction du temps écoulé
            dashSpeed = Mathf.Lerp(baseDashSpeed, 0f, Mathf.Pow(rollTimer / rollDuration, 2));

            // Augmenter le timer
            rollTimer += Time.deltaTime;

            yield return null;
        }
        isRolling = false;
        InventoryScript.canPickup = true;
        dashDirection = Vector2.zero;
        spriteRenderer.color = baseColor;
    }

    private Vector2 GetDashDirection()
    {
        Vector2 _direction = Vector2.zero;

        if(Input.GetKey(MoveUpKey))
        {
            _direction += Vector2.up;
        }
        if (Input.GetKey(MoveLeftKey))
        {
            _direction += Vector2.left;
        }
        if (Input.GetKey(MoveDownKey))
        {
            _direction += Vector2.down;
        }
        if (Input.GetKey(MoveRightKey))
        {
            _direction += Vector2.right;
        }

        return _direction.normalized;

    }


    private void Move()
    {
        if (isRolling)
        {
            rb.velocity = dashDirection * dashSpeed;
        }
        else if(!weaponParentScript.isAttacking)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
            dashSpeed = baseDashSpeed;
        }

        if (rb.velocity.magnitude > 0.05f) // Vérifie si le joueur est en mouvement
        {
            CreateDust();
        }
    }

    void CreateDust()
    {
        dust.Play();
    }


}
