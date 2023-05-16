using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage); // Appel à la méthode de la classe de base pour gérer les points de vie
        // Autres actions spécifiques au joueur
    }

    public override void Heal(float amount)
    {
        // Logique spécifique pour le personnage du joueur lorsqu'il est soigné
        base.Heal(amount); // Appel à la méthode de la classe de base pour gérer les points de vie
        // Autres actions spécifiques au joueur
    }
}