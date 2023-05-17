using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{

    public WeaponParent playerWeaponScript;

    public override void TakeDamage(float damage)
    {
        // Logique spécifique pour les personnages ennemis lorsqu'ils subissent des dégâts
        base.TakeDamage(damage); // Appel à la méthode de la classe de base pour gérer les points de vie
        // Autres actions spécifiques aux ennemis
        if(health <=0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy Died"); 
        }
    }

    public override void Heal(float amount)
    {
        // Les personnages ennemis ne peuvent généralement pas être soignés, cette méthode peut être laissée vide
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon")
        {
             float damage = playerWeaponScript.AttackDamage;
           TakeDamage(damage);
           Debug.Log("Enemy Hit");
         }
    }

}