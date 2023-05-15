using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public override void TakeDamage(int damage)
    {
        // Logique sp�cifique pour les personnages ennemis lorsqu'ils subissent des d�g�ts
        base.TakeDamage(damage); // Appel � la m�thode de la classe de base pour g�rer les points de vie
        // Autres actions sp�cifiques aux ennemis
    }

    public override void Heal(int amount)
    {
        // Les personnages ennemis ne peuvent g�n�ralement pas �tre soign�s, cette m�thode peut �tre laiss�e vide
    }
}