using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float health;

    public virtual void TakeDamage(int damage)
    {
        // Logique pour réduire les points de vie du personnage

        health -= damage;

        // Logique supplémentaire, comme la vérification de la mort du personnage


    }

    public virtual void Heal(int amount)
    {
        // Logique pour soigner le personnage
        health += amount;

        // Logique supplémentaire, comme la vérification des limites de points de vie
    }
}
