using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private GameObject player;
    private Vector3 LastKnown;
    public NavMeshAgent agent;
    public int Damage;
    public float AttackSpeed;
    private float attackCountDown;
    private bool CanAttack = true;
    public float health;
    public GameObject[] Drops;
    public float[] probability;
    public LayerMask layerMask;
    float yVel = 0f;

    private void Start()
    {
        attackCountDown = AttackSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        Damage = Damage - ((Settings.instance.difficulty / 4) * Damage);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 150)
        {
            Destroy(transform.gameObject);
        }

        if (health <= 0)
        {
            if (Drops.Length == probability.Length) {
                for (int i = 0; i < Drops.Length; i++)
                {
                    var rand = Random.Range(0.0f, 100.0f);
                    if (rand <= probability[i])
                    {
                        GameObject drop = Instantiate(Drops[i], transform.position, Quaternion.identity);
                        drop.GetComponent<Rigidbody>().AddForce(Vector3.up);
                    }
                }
            }
            Destroy(transform.gameObject);
        }


        if (agent.isOnNavMesh)
        {
            float maxRange = 25;
            RaycastHit hit;
            if (Vector3.Distance(transform.position, player.transform.position) < maxRange && CanAttack)
            {
                Ray ray = new Ray(transform.position, Vector3.Normalize(player.transform.position - transform.position));
                Debug.DrawRay(transform.position, (player.transform.position - transform.position), Color.yellow);
                if (Physics.Raycast(ray, out hit, maxRange, layerMask))
                {
                    if (hit.transform.tag.Equals("Player"))
                    {
                        LastKnown = player.transform.position;

                        agent.SetDestination(LastKnown);
                        if (hit.distance <= 3 && CanAttack)
                        {
                            Player.instance.health -= Damage;
                            CanAttack = false;
                        }
                    }
                    else
                    {
                        ray = new Ray(transform.position, ((transform.forward * 10) - Vector3.down));
                        Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
                        try
                        {
                            agent.SetDestination(hit.point);
                        }
                        catch (UnityException e) { }
                    }
                }
            }
        }

        else
        {
            if (yVel < 0)
            {
                yVel = 0f;
            }
            yVel += -9.81f * Time.deltaTime;
            agent.Move(new Vector3(0, yVel, 0) * Time.deltaTime);
        }

        if (attackCountDown > 0)
        {
            attackCountDown -= Time.deltaTime;
        }
        else
        {
            CanAttack = true;
            attackCountDown = AttackSpeed;
        }
    }
}
