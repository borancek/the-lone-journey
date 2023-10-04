using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Singleton
    public static Player instance;
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

    public bool isPaused;
    public Dictionary<Item, int> inventory = new Dictionary<Item, int>();

    public int health = 100;
    public int maxHealth = 100;

    public Item equippedItem;

    public delegate void OnInvChange();
    public OnInvChange OnInvChangeCall;

    public float stamina;
    public float curStamina;
    public float staGenDelay;
    public float staGenTime;

    public void Update()
    {
        if (curStamina <= stamina && staGenTime == staGenDelay)
        {
            curStamina += (stamina * 0.1f) * Time.deltaTime;
            if (curStamina > stamina)
            {
                curStamina = stamina;
            }
        }

        if (staGenTime < staGenDelay)
        {
            staGenTime += 1f * Time.deltaTime;
        }
        else if (staGenTime >= staGenDelay)
        {
            staGenTime = staGenDelay;
        }

        if (health <= 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public bool useStamina(float staminaUsed)
    {
        staGenTime = 0;
        if (curStamina <= 0)
        {
            return false;
        }
        if (staminaUsed > stamina)
        {
            curStamina = 0;
            return true;
        }
        curStamina -= staminaUsed;
        return true;
    }

    public int getItemCount(string item)
    {
        foreach (var a in inventory.Keys)
        {
            if (a.Name == item)
            {
                return inventory[a];
            }
        }
        return 0;
    }

    public void addItem(Item item, int count)
    {
        if (inventory.ContainsKey(item))
        {
            foreach (var a in inventory.Keys)
            {
                if (a == item)
                {
                    inventory[a] += count;
                    break;
                }
            }
        }
        else
        {
            inventory.Add(item, count);
        }
        OnInvChangeCall();
    }

    public void removeItem(Item item)
    {
        foreach (var a in inventory.Keys)
        {
            if (a.Name.Equals(item.Name))
            {
                inventory.Remove(a);
                OnInvChangeCall();

                return;
            }
        }
    }

    public void removeItem(Item item, int count)
    {
        foreach (var a in inventory.Keys)
        {
            if (a.Name.Equals(item.Name))
            {
                if (inventory[a] - count <= 0)
                {
                    inventory.Remove(a);
                }
                else
                {
                    inventory[a] -= count;
                }
                OnInvChangeCall();
                return;
            }
        }
    }

    public void removeItem(string item, int count)
    {
        foreach (var a in inventory.Keys)
        {
            if (a.Name.Equals(item))
            {
                if (inventory[a] - count <= 0)
                {
                    inventory.Remove(a);
                }
                else
                {
                    inventory[a] -= count;
                }
                OnInvChangeCall();
                return;
            }
        }
    }

    public bool hasItem(Item item, int count)
    {
        foreach (var a in inventory.Keys)
        {
            if (a.Name.Equals(item.Name))
            {
                if (inventory[a] - count >= 0)
                {
                    return true;
                }
                break;
            }
        }
        return false;
    }

    public bool hasItem(string item, int count)
    {
        foreach (var a in inventory.Keys)
        {
            if (a.Name.Equals(item))
            {
                if (inventory[a] - count >= 0)
                {
                    return true;
                }
                break;
            }
        }
        return false;
    }

    public string invToStr()
    {
        string output = "";
        foreach (var a in inventory)
        {
            output += a.Key.Name + " " + a.Key.Description + " " + a.Value.ToString() + "\n";
        }
        return output;
    }
}
