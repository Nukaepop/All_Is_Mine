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

    void ShowExcma�ationPoint()
    {
        ExclamationPoint.SetActive(true);
    }

    void HideExcma�ationPoint()
    {
        ExclamationPoint.SetActive(false);
    }
}
