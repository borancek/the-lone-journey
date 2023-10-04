using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAxe : MonoBehaviour
{
    private bool firingWeapon = false;
    private bool hitObject = false;
    public LayerMask layerMask = 1 << 9;

    void Update()
    {
        Debug.DrawRay(GameObject.FindGameObjectWithTag("MainCamera").transform.position, GameObject.FindGameObjectWithTag("MainCamera").transform.forward);
        if (!Player.instance.isPaused)
        {
            var normTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (normTime >= 0.99f && firingWeapon)
            {
                firingWeapon = false;
            }

            if (Input.GetMouseButtonDown(0) && Player.instance.useStamina(10f))
            {
                firingWeapon = true;
                hitObject = false;
                GetComponent<Animator>().Play("PickaxeAttack");
                Ray ray = new Ray(GameObject.FindGameObjectWithTag("MainCamera").transform.position, GameObject.FindGameObjectWithTag("MainCamera").transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 5f, layerMask))
                {
                    Debug.Log(hit.transform.tag);
                    if (hit.transform.tag.Equals("Choppable") && !hitObject && firingWeapon)
                    {
                        hitObject = true;
                        hit.transform.GetComponent<TreeController>().TakeDamage();
                    }
                }
            }

            else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && !firingWeapon)
            {
                GetComponent<Animator>().Play("PickaxeWalk");
            }
        }
    }
}
