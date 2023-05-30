using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSprite : MonoBehaviour
{
    public Transform transform;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Retourner le sprite horizontalement
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
