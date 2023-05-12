using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check si le joueur entre en collision avec un objet possédant l'interface pour collecter les objets

        ICollectible collectible = collision.GetComponent<ICollectible>();
        if(collectible != null)
        {
            collectible.Collect();
        }
    }
}
