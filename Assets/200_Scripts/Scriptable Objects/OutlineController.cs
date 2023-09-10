using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    public Sprite defaultSprite, outlinedSprite;

    private SpriteRenderer spriteRenderer;
    public GameObject Object;
    public PlayerInventory inventoryScript;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Object == inventoryScript.nearestObject)
        {
            spriteRenderer.sprite = outlinedSprite;
            Debug.Log(" ouais ouais outline");
        }
        else
        {
            spriteRenderer.sprite = defaultSprite;
        }
    }
}
