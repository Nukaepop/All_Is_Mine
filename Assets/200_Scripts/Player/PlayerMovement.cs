using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Keybinds")]
    public KeyCode MoveUpKey = KeyCode.UpArrow;
    public KeyCode MoveLeftKey = KeyCode.LeftArrow;
    public KeyCode MoveRightKey = KeyCode.RightArrow;
    public KeyCode MoveDownKey = KeyCode.DownArrow;

    public KeyCode sprintKey = KeyCode.X;
    public KeyCode rollKey = KeyCode.C;

    [Header("ScriptRefs")]

    public PlayerInventory InventoryScript;

    [Header("Walk")]

    private float moveSpeed;
    public float WalkSpeed;


    [Header("Sprint")]

    public float SprintSpeed;

    [Header("Roll")]

    public float rollDuration;
    public bool isRolling = false;
    private Vector2 dashDirection;
    public float dashSpeed;

    [Header("General")]

    public Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;
    public Color baseColor;
    public Color currentColor;

    public Vector2 moveDirection;

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
        }
        else if(Input.GetKey(sprintKey))
        {
            State = MovementState.Sprinting;
            moveSpeed = SprintSpeed;
            spriteRenderer.color = Color.magenta;
        }
        else
        {
            State = MovementState.Walking;
            moveSpeed = WalkSpeed;
            spriteRenderer.color = baseColor;
        }
    }

    private void Start()
    {
        baseColor = spriteRenderer.color;
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

        if(Input.GetKeyDown(rollKey) && !isRolling)
        {
            dashDirection = GetDashDirection();
            StartCoroutine(PerformRoll());
        }

    }

    private IEnumerator PerformRoll()
    {
        isRolling = true;

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
