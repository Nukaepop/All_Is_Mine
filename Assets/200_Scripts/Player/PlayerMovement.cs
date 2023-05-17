using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Keybinds")]

    private KeyCode MoveUpKey = KeyCode.Z;
    private KeyCode MoveLeftKey = KeyCode.Q;
    private KeyCode MoveRightKey = KeyCode.D;
    private KeyCode MoveDownKey = KeyCode.S;

    private KeyCode sprintKey = KeyCode.LeftShift;
    private KeyCode rollKey = KeyCode.Space;


    [Header("ScriptRefs")]

    public PlayerInventory InventoryScript;


    [Header("Walk")]

    private float moveSpeed;
    public float WalkSpeed;


    [Header("Sprint")]

    public float SprintSpeed;
    public float SprintStaminaCostRate;


    [Header("Roll")]

    public float rollDuration;
    public bool isRolling = false;
    private Vector2 dashDirection;
    public float dashSpeed;
    public float RollStaminaCost;


    [Header("General")]

    public Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;
    public Color baseColor;
    public Color currentColor;

    public Vector2 moveDirection;


    [Header("Stamina")]

    public float MaxStamina;
    public float currentStamina;
    public float staminaRecoveryRate;
    public float baseMaxStamina;


    private bool hasStamina = true;
    private bool isUsingStamina = false;

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


            isUsingStamina = true;
        }
        else if(Input.GetKey(sprintKey) && hasStamina)
        {
            State = MovementState.Sprinting;
            moveSpeed = SprintSpeed;
            spriteRenderer.color = Color.magenta;

            currentStamina -= (SprintStaminaCostRate * Time.deltaTime);
            isUsingStamina = true;
        }
        else
        {
            State = MovementState.Walking;
            moveSpeed = WalkSpeed;
            spriteRenderer.color = baseColor;

            isUsingStamina = false;
        }
    }

    private void Start()
    {
        baseColor = spriteRenderer.color;
        currentStamina = MaxStamina;
        hasStamina = true;
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
                hasStamina = true;
            }
        }

        MaxStamina = baseMaxStamina - InventoryScript.bagSize * 15;

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
        spriteRenderer.color = Color.yellow;


        yield return new WaitForSeconds(rollDuration);

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
        else
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }   
    }

}
