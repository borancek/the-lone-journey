using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteraction : MonoBehaviour
{
    public GameObject thingToShow;
    public void interaction()
    {
        thingToShow.SetActive(true);
        Time.timeScale = 0;
        Player.instance.isPaused = true;
    }
}
