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
    public PlayerInventory playerInventoryScript;

    public float AttackDamage;


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
        attackDelay = baseDelay + playerInventoryScript.bagSize * 0.3f;
        animator.speed = 1 - playerInventoryScript.bagSize * 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            //arme qui suis la souris
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - TransformWeaponParent.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            TransformWeaponParent.rotation = Quaternion.Euler(0f, 0f, rotation_z);
        }
        //rotate le sprite de l'arme en fonction du coté de notre curseur sur l'axe x

        Vector2 scale = TransformWeaponParent.localScale;
        if (Input.mousePosition.x < 0)
        {
            scale.y = -1;
        }
        else if (Input.mousePosition.x > 0)
        {
            scale.y = 1;
        }

        //faire passer l'arme devant ou derrière le joueur en fonction de la rotation de l'arme  -- en haut = derrière le joueur | en bas = devant

        if(TransformWeaponParent.eulerAngles.z > 0 && TransformWeaponParent.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }


        TransformWeaponParent.localScale = scale;

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
            animator.SetTrigger("Attack");
            isAttacking = true;
            delay = attackDelay;
        }
    }


}
