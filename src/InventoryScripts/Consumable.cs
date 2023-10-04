using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]

public class Consumable : ScriptableObject
{
    public int healAmount;
    
    public void heal()
    {
        var maxHealth = Player.instance.maxHealth;

        Player.instance.health += healAmount;

        if (Player.instance.health > maxHealth)
        {
            Player.instance.health = maxHealth;
        }
    }
}
