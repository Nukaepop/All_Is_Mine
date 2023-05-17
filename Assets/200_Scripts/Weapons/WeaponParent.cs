using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{

    public SpriteRenderer characterRenderer, weaponRenderer;

    public Animator animator;
    public float delay = 0.3f;
    private bool cantAttack;
    public Transform TransformWeaponParent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - TransformWeaponParent.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        TransformWeaponParent.rotation = Quaternion.Euler(0f, 0f, rotation_z);
       
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

    }

    public void Attack()
    {
        if (cantAttack)
            return;

        animator.SetTrigger("Attack");
        Debug.Log("Attacking");
        cantAttack = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        cantAttack = false;
    }

}
