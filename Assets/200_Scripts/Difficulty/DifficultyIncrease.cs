using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyIncrease : MonoBehaviour
{

    public EnemyStats enemyStatsScript;
    
    
    private float elapsedTime = 0f;
    private int repetitionsMax = 3;
    private int repetitions;

    int level = 1;

    public List<StatRule> statRules;

    void start()
    {
        repetitions = 0;
    }

    void Update()
    {
        if(repetitions < repetitionsMax)
        {
        elapsedTime += Time.deltaTime;

            if (elapsedTime >= 10f)
            {
                // Augmenter la difficult�
                IncreaseDifficulty();

                // R�initialiser le compteur
                elapsedTime = 0f;
                repetitions++;
            }
        }
    }

    void IncreaseDifficulty()
    {
        // Mettez en �uvre les changements de difficult� ici
        Debug.Log("WOW Ca devient plus difficile !!!");

        // Augmenter le niveau
        level++;

        // Mettez en �uvre les changements de difficult� sp�cifiques ici

        // Mettez � jour les statistiques des ennemis en fonction du niveau
   /*     foreach (StatRule rule in statRules)
        {
            if (level >= rule.minLevel && level <= rule.maxLevel)
            {
                EnemyStats.health += rule.increaseAmount;
                EnemyStats.speed += rule.increaseAmount;
                EnemyStats.damage += rule.increaseAmount;
            }
        } */
    }
}
