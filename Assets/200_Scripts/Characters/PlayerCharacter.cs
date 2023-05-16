using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage); // Appel � la m�thode de la classe de base pour g�rer les points de vie
        // Autres actions sp�cifiques au joueur
    }

    public override void Heal(float amount)
    {
        // Logique sp�cifique pour le personnage du joueur lorsqu'il est soign�
        base.Heal(amount); // Appel � la m�thode de la classe de base pour g�rer les points de vie
        // Autres actions sp�cifiques au joueur
    }
}