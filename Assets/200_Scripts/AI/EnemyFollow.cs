using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minimumDistance;
    public Rigidbody2D rb;

    private void Update()
    {

        //se rapprocher du joueur
        if (Vector2.Distance(transform.position, target.position) > minimumDistance)
        {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        //attaquer le joueur
        else
        {

        }
    }
}
