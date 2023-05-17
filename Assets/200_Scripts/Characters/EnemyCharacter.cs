using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{

    public WeaponParent playerWeaponScript;

    public override void TakeDamage(float damage)
    {
        // Logique sp�cifique pour les personnages ennemis lorsqu'ils subissent des d�g�ts
        base.TakeDamage(damage); // Appel � la m�thode de la classe de base pour g�rer les points de vie
        // Autres actions sp�cifiques aux ennemis
        if(health <=0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy Died"); 
        }
    }

    public override void Heal(float amount)
    {
        // Les personnages ennemis ne peuvent g�n�ralement pas �tre soign�s, cette m�thode peut �tre laiss�e vide
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