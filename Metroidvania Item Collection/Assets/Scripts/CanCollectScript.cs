using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanCollectScript : MonoBehaviour
{
    public bool canCollect;
    // Start is called before the first frame update
    void Start()
    {
        canCollect = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collectible")
        {
            canCollect = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "Collectible")
        {
            canCollect = false;
        }
    }
}
