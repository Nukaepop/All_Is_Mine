using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Référence au transform du joueur
    public float detectionRange = 10f; // Portée de détection du joueur

    private NavMeshAgent navMeshAgent;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Récupérer le composant NavMeshAgent
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        // Vérifier si le joueur est détecté
        if (DetectPlayer())
        {

            MoveTowardsPlayer();

            // Le joueur est détecté, ajoutez ici votre logique de comportement
            // lorsque le joueur est repéré, par exemple, attaquer ou suivre le joueur
        }
    }

    private bool DetectPlayer()
    {
        // Vérifier la distance entre l'ennemi et le joueur
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Vérifier si le joueur est à portée de détection
        if (distanceToPlayer <= detectionRange)
        {
            Debug.Log("Joueur en range");
            // Effectuer une vérification de ligne de vue/raycast pour s'assurer qu'il n'y a pas d'obstacles entre l'ennemi et le joueur
            return true;
        }
        Debug.Log("Joueur pas en range");
        // Le joueur n'est pas détecté
        return false;
    }

    private void MoveTowardsPlayer()
    {
        navMeshAgent.SetDestination(player.position); // Définir la destination du joueur pour le NavMeshAgent
        Debug.Log("IA en mouvement");
    }

}

