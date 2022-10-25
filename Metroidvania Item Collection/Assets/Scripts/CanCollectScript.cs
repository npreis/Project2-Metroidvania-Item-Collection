using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanCollectScript : MonoBehaviour
{
    public bool canCollect;
    public ItemScript item;
    public Text textPopup;
    // Start is called before the first frame update
    void Start()
    {
        canCollect = false;
        textPopup.text = "";
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
            item = other.gameObject.GetComponent<ItemScript>();
            textPopup.text = "E";
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "Collectible")
        {
            canCollect = false;
            item = null;
            textPopup.text = "";
        }
    }

    void Collect()
    {
        if(canCollect && Input.GetKeyDown(KeyCode.E))
        {
            item.ItemActivate(item.gameObject);
            textPopup.text = "";
        }
    }
}
