using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public string propRef;

    private void start()
    {
        GameObject Detection = transform.Find("Detection").gameObject;

        CircleCollider2D DetectionCollider = Detection.GetComponent<CircleCollider2D>();

        if (DetectionCollider != null)
        {
            DetectionCollider.enabled = true;
        }
    }


}
