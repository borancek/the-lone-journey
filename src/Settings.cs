using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    #region Singleton
    public static Settings instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one player");
            return;
        }

        instance = this;
    }
    #endregion

    public int difficulty;

}
