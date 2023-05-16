using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public KeyCode AttackKey = KeyCode.Space;

    [SerializeField] private Animator Anim;
    [SerializeField] private float meleeSpeed;
    [SerializeField] private float damage;

    float timeUntilMelee;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeUntilMelee <= 0f)
        {
            if(Input.GetKey(AttackKey)
                {
                Anim.SetTrigger('Attack');
                timeUntilMelee = meleeSpeed;
                }
        }

        else
        {
            timeUntilMelee -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

}
