using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayReactions : MonoBehaviour
{
    public GameObject ExclamationPoint;

    void start()
    {
        ExclamationPoint.SetActive(false);
    }

    void ShowExcmaùationPoint()
    {
        ExclamationPoint.SetActive(true);
    }

    void HideExcmaùationPoint()
    {
        ExclamationPoint.SetActive(false);
    }
}
