using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public override void TakeDamage(int damage)
    {
        // Logique spécifique pour les personnages ennemis lorsqu'ils subissent des dégâts
        base.TakeDamage(damage); // Appel à la méthode de la classe de base pour gérer les points de vie
        // Autres actions spécifiques aux ennemis
    }

    public override void Heal(int amount)
    {
        // Les personnages ennemis ne peuvent généralement pas être soignés, cette méthode peut être laissée vide
    }
}