using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanCollectScript : MonoBehaviour
{
    public bool canCollect;
    public ItemScript item;
    // Start is called before the first frame update
    void Start()
    {
        canCollect = false;
        //item = gameObject.GetComponent<ItemScript>();
    }

    private void Update()
    {
        Collect();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collectible")
        {
            canCollect = true;
            //item.gameObject.GetComponent<ItemScript>();
            item = other.gameObject.GetComponent<ItemScript>();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "Collectible")
        {
            canCollect = false;
            item = null;
        }
    }

    void Collect()
    {
        if(canCollect && Input.GetKeyDown(KeyCode.E))
        {
            item.ItemActivate(item.gameObject);
        }
    }
}
