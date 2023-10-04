using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    public int segmentHealth;
    public int segments;
    public int health = 3;
    public GameObject pebble;
    public GameObject Ingot;
    int curHealth = 0;
    int curSegCount;

    public void Start()
    {
        curHealth = health;
        curSegCount = segments;
    }

    public void TakeDamage()
    {
        if (curSegCount <= 0)
        {
            Destroy(transform.gameObject);
        }

        curHealth--;

        if (curHealth <= 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            GameObject a;

            if (curSegCount == 1)
            {
                a = Instantiate(Ingot, new Vector3(transform.position.x, transform.position.y+1, transform.position.z), Quaternion.identity);

                Destroy(transform.gameObject);
            }

            else
            {
                curSegCount--;
                curHealth = segmentHealth;
                a = Instantiate(pebble, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            }

            a.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0, 1), 0.0f, Random.Range(0, 1)));
        }
    }
}
