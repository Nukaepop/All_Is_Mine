using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayer : MonoBehaviour
{
    public SpriteRenderer itemRenderer, playerRenderer;
    public Transform playerTransform, itemTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.y > itemTransform.position.y)
        {
            itemRenderer.sortingOrder = playerRenderer.sortingOrder + 3;
        }
        else
        {
            itemRenderer.sortingOrder = playerRenderer.sortingOrder -3;
        }

    }
}
