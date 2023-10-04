using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int enemyCount;
    public GameObject player;
    public GameObject Enemy;
    public LayerMask layerMask;

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= enemyCount)
        {
            for (int i = GameObject.FindGameObjectsWithTag("Enemy").Length; i < 10; i++)
            {
                var y = 200f;
                var x = player.transform.position.x;
                var z = player.transform.position.z;
                var rand = Random.Range(0.0f, 1.0f);
                float randOffset = Random.Range(100, 50);

                /*if (rand > 0.5)
                {
                    x = player.transform.position.x + randOffset;
                }
                else
                {
                    x = player.transform.position.x - randOffset;
                }
                rand = Random.Range(0.0f, 1.0f);
                randOffset = Random.Range(100, 50);
                if (rand > 0.5)
                {
                    z = player.transform.position.z + randOffset;
                }
                else
                {
                    z = player.transform.position.z - randOffset;
                }*/

                Vector3 position = choosePoint();
                Ray ray = new Ray(new Vector3(position.x + x, y, position.z + z), Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.collider.gameObject.layer == 8)
                    {
                        y = hit.point.y;
                        GameObject enemy = Instantiate(Enemy, new Vector3(position.x + x, y, position.z + z), Quaternion.identity);
                        enemy.transform.localScale = new Vector3(Random.Range(0.8f, 1.0f), Random.Range(0.8f, 1.0f), Random.Range(0.8f, 1.0f));
                    }
                }
            }
        }
    }

    Vector3 choosePoint()
    {
        var angle = Random.Range(0.0f, 360.0f);
        float angleInRadians = (Mathf.PI / 180) * angle;
        var distance = Random.Range(25.0f, 75.0f);
        Vector3 output = new Vector3((float)Mathf.Cos(angle), 0, (float)Mathf.Sin(angle)) * distance;

        return output;
    }
}
