using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{

    public SpriteRenderer characterRenderer, weaponRenderer;

    public Animator animator;
    private float delay;
    public float baseDelay;
    public float attackDelay;

    public Transform TransformWeaponParent;
    public Transform playerTransform;
    public PlayerInventory playerInventoryScript;

    public int AttackDamage;

    public bool isFacingRight;


    public bool isAttacking { get; private set; }

    public void ResetIsAttacking()
    {
        isAttacking = false;
    }

    // Start is called before the first frame update
    void Start()
    {
 
    }

    private void LateUpdate()
    {
        attackDelay = baseDelay + playerInventoryScript.bagSize * 0.5f;
        animator.speed = 1 - playerInventoryScript.bagSize * 0.15f;
    }

    // Update is called once per frame
    void Update()
    {


        if (!isAttacking)
        {

            Vector3 mousePosition = Input.mousePosition;

            // R�cup�re la position du personnage par rapport � la cam�ra
            Vector3 characterPosition = Camera.main.WorldToScreenPoint(playerTransform.position);

            // Compare les positions X pour d�terminer la direction de la souris par rapport au personnage
            if (mousePosition.x > characterPosition.x)
            {
                if (!isFacingRight)
                    // La souris est � droite du personnage
                    playerTransform.localScale = new Vector3(1, 1, 1); // Ne pas retourner le sprite
                isFacingRight = true;
            }
            else
            {
                if (isFacingRight)
                    // La souris est � gauche du personnage
                    playerTransform.localScale = new Vector3(-1, 1, 1); // Retourner le sprite horizontalement
                isFacingRight = false;
            }


            //arme qui suis la souris
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - TransformWeaponParent.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            if (!isFacingRight)
            {
                rotation_z += 180f;
            }

            TransformWeaponParent.rotation = Quaternion.Euler(0f, 0f, rotation_z);
        }
        //rotate le sprite de l'arme en fonction du cot� de notre curseur sur l'axe x



        //faire passer l'arme devant ou derri�re le joueur en fonction de la rotation de l'arme  -- en haut = derri�re le joueur | en bas = devant

        if(TransformWeaponParent.eulerAngles.z > 0 && TransformWeaponParent.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }

 

        if (Input.GetMouseButton(0))
        {
            Attack();
        }

        if(delay > 0)
        {
            delay -= Time.deltaTime;
        }

    }

    public void Attack()
    {
        if (delay <= 0)
        {
            if (playerInventoryScript.bagSize == 0)
            {
                AttackDamage = 1;
            }
            else if (playerInventoryScript.bagSize == 1)
            {
                AttackDamage = 3;
            }
            else if (playerInventoryScript.bagSize == 2)
            {
                AttackDamage = 5;
            }
            else if (playerInventoryScript.bagSize >= 3)
            {
                AttackDamage = 8;
            }
            else
            {
                // Valeur par d�faut si bagSize ne correspond � aucun cas pr�cis
                AttackDamage = 1;
            }
            Debug.Log("Attack Damage: " + AttackDamage);
            animator.SetTrigger("Attack");
            isAttacking = true;
            delay = attackDelay;
            Debug.Log(AttackDamage);
        }
    }


}
