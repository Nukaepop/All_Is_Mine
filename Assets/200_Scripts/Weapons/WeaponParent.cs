using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{

    public Animator animator;
    public float delay = 0.3f;
    private bool cantAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
