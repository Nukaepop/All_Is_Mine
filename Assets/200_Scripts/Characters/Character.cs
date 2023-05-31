using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float health;

    public virtual void TakeDamage(int damage)
    {
        // Logique pour r�duire les points de vie du personnage

        health -= damage;

        // Logique suppl�mentaire, comme la v�rification de la mort du personnage


    }

    public virtual void Heal(int amount)
    {
        // Logique pour soigner le personnage
        health += amount;

        // Logique suppl�mentaire, comme la v�rification des limites de points de vie
    }
}
