using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // R�f�rence au transform du joueur
    public float detectionRange = 10f; // Port�e de d�tection du joueur

    private NavMeshAgent navMeshAgent;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // R�cup�rer le composant NavMeshAgent
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        // V�rifier si le joueur est d�tect�
        if (DetectPlayer())
        {

            MoveTowardsPlayer();

            // Le joueur est d�tect�, ajoutez ici votre logique de comportement
            // lorsque le joueur est rep�r�, par exemple, attaquer ou suivre le joueur
        }
    }

    private bool DetectPlayer()
    {
        // V�rifier la distance entre l'ennemi et le joueur
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

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
        navMeshAgent.SetDestination(player.position); // D�finir la destination du joueur pour le NavMeshAgent
        Debug.Log("IA en mouvement");
    }

}

