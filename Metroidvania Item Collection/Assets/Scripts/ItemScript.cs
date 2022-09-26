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
    public Item itemType;
    public GameObject collectible;
    // Start is called before the first frame update
    void Start()
    {
        itemCheck.gameObject.GetComponent<MasterItemCheckScript>();
    }

    public void ItemActivate(GameObject item)
    {
        switch(itemType)
        {
            case (Item.DOUBLE_JUMP):
                itemCheck.canDoubleJump = true;
                Destroy(collectible);
                break;
                
            case (Item.AIR_DASH):
                itemCheck.canDash = true;
                break;

            case (Item.WALL_JUMP):
                itemCheck.canWallJump = true;
                break;
        }
    }
}
