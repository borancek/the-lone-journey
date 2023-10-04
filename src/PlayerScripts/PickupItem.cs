using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private RaycastHit hit;
    void Update()
    {
        var player = Player.instance;

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = new Ray(transform.position, transform.rotation * new Vector3(0,0,10));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("Did Hit " + hit.transform.tag);

                if (hit.transform.tag.Equals("Item"))
                {
                    //debug to visualise ray when in editor
                    //Debug.DrawRay(transform.position, ray.direction * hit.distance, Color.yellow);

                    //adds the item hit by the raycast to the players inventory;

                    var item = hit.transform.GetComponent<ItemObject>();

                    if (item.item.isInteractable)
                    {
                        hit.transform.GetComponent<OnInteraction>().interaction();
                    }

                    player.addItem(item.item, item.Count);

                    Debug.Log(player.invToStr());

                    //destorys the object hit by the raycast
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
