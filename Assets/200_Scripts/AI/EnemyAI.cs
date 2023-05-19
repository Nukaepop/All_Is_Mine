using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Référence au transform du joueur

    public float detectionRange = 10f; // Portée de détection du joueur

    private bool isPatrolling;
    public float patrolSpeed = 2f;

    public List<Transform> waypoints; // Liste des waypoints pour la ronde
    private int currentWaypointIndex = 0;

    private NavMeshAgent navMeshAgent;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Récupérer le composant NavMeshAgent
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        StartPatrol();
    }

    private void Update()
    {
        // Vérifier si le joueur est détecté
        if (DetectPlayer())
        {
            StopPatrol();
            MoveTowardsPlayer();

            // Le joueur est détecté, ajoutez ici votre logique de comportement
            // lorsque le joueur est repéré, par exemple, attaquer ou suivre le joueur
        }

        else if (isPatrolling)
        {
            // Continuer la ronde
            ContinuePatrol();
        }
        else
        {
            StartPatrol();
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

    private void StartPatrol()
    {
        isPatrolling = true;
        navMeshAgent.speed = patrolSpeed;

        // Définir la destination initiale du premier waypoint
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        Debug.Log("IA en ronde");
    }

    private void StopPatrol()
    {
        isPatrolling = false;
        navMeshAgent.ResetPath();
        Debug.Log("IA a arrêté la ronde");
    }

    private void ContinuePatrol()
    {
        // Vérifier si l'IA a atteint sa destination actuelle
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // Passer au waypoint suivant
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;

            // Définir la destination du prochain waypoint
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

}

