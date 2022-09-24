using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public enum Item
    {
        HEALTH_UPGRADE,
        DOUBLE_JUMP,
        WALL_JUMP,
        AIR_DASH
    };

    public MasterItemCheckScript itemCheck;
    // Start is called before the first frame update
    void Start()
    {
        itemCheck = gameObject.GetComponent<MasterItemCheckScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
