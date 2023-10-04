using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class updateHud : MonoBehaviour
{
    public TMP_Text health;
    public TMP_Text stamina;

    private void Update()
    {
        health.SetText(Player.instance.health + "/" + Player.instance.maxHealth);
        stamina.SetText(Mathf.Clamp(Mathf.RoundToInt(Player.instance.curStamina),0, 100) 
            + "/" + Mathf.Clamp(Mathf.RoundToInt(Player.instance.stamina), 0, 100));
    }
}
