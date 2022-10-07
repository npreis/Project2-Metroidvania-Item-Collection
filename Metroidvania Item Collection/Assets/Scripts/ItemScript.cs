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
    public PlayerHealthScript health;

    public RenderMaxHPScript[] healthPool;
    public RenderCurrHPScript[] playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        itemCheck.gameObject.GetComponent<MasterItemCheckScript>();
        health.gameObject.GetComponent<PlayerHealthScript>();

        healthPool = FindObjectsOfType<RenderMaxHPScript>();

        foreach(RenderMaxHPScript hp in healthPool)
        {
            hp.RenderHP(health.maxHealth);
        }

        playerHealth = FindObjectsOfType<RenderCurrHPScript>();

        foreach(RenderCurrHPScript hp in playerHealth)
        {
            hp.RenderHP(health.currHealth);
        }
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

            case (Item.HEALTH_UPGRADE):
                health.maxHealth++;
                health.currHealth = health.maxHealth;

                foreach (RenderMaxHPScript hp in healthPool)
                {
                    hp.RenderHP(health.maxHealth);
                }

                foreach (RenderCurrHPScript hp in playerHealth)
                {
                    hp.RenderHP(health.currHealth);
                }

                Destroy(collectible);
                break;
        }
    }
}
