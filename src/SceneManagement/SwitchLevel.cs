using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    public Item[] keyItems;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other){
        foreach(var i in keyItems)
        {
            if(!Player.instance.hasItem(i, 1))
            {
                return;
            }
        }
        SceneManager.LoadScene(3);
    }
}
