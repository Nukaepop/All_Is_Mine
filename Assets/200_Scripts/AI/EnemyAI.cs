using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // R�f�rence au transform du joueur

    public float detectionRange = 10f; // Port�e de d�tection du joueur
    private float distanceToPlayer;

    public float attackRange;
    public float attackCooldown;

    private bool isPatrolling;
    public float patrolSpeed = 2f;

    public List<Transform> waypoints; // Liste des waypoints pour la ronde
    private int currentWaypointIndex = 0;

    private bool isAttacking = false;
    private bool hasCooldown = true;

    public Animator animator;

    private NavMeshAgent navMeshAgent;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // R�cup�rer le composant NavMeshAgent
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        StartPatrol();
    }

    private void Update()
    {
        // V�rifier si le joueur est d�tect�
        if (DetectPlayer())
        {
            StopPatrol();
            MoveTowardsPlayer();
            if(!isAttacking && hasCooldown && distanceToPlayer <= attackRange)
            {
                Attack();
            }
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
        // V�rifier la distance entre l'ennemi et le joueur
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // V�rifier si le joueur est � port�e de d�tection
        if (distanceToPlayer <= detectionRange)
        {
            Debug.Log("Joueur en range");
            // Effectuer une v�rification de ligne de vue/raycast pour s'assurer qu'il n'y a pas d'obstacles entre l'ennemi et le joueur
            return true;
        }
        Debug.Log("Joueur pas en range");
        // Le joueur n'est pas d�tect�
        return false;
    }

    private void MoveTowardsPlayer()
    {
        if (!isAttacking)
        {
            navMeshAgent.SetDestination(player.position); // D�finir la destination du joueur pour le NavMeshAgent
            Debug.Log("IA en mouvement");

        }
    }
    private void StartPatrol()
    {
        isPatrolling = true;
        navMeshAgent.speed = patrolSpeed;

        // D�finir la destination initiale du premier waypoint
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        Debug.Log("IA en ronde");
    }

    private void StopPatrol()
    {
        isPatrolling = false;
        navMeshAgent.ResetPath();
        Debug.Log("IA a arr�t� la ronde");
    }

    private void ContinuePatrol()
    {
        // V�rifier si l'IA a atteint sa destination actuelle
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // Passer au waypoint suivant
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;

            // D�finir la destination du prochain waypoint
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }


    private void Attack()
    {
        animator.SetTrigger("Attack");
        isAttacking = true;
        // Ajoutez ici votre logique d'attaque
        // Par exemple, d�clencher une animation d'attaque, infliger des d�g�ts au joueur, etc.
        Debug.Log("IA attaque");

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        hasCooldown = false;
        yield return new WaitForSeconds(attackCooldown);
        hasCooldown = true;
        isAttacking = false;
    }

}

