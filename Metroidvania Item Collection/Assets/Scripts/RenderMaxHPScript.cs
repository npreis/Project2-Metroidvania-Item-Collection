using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMaxHPScript : MonoBehaviour
{
    public int hpNum;
    public PlayerHealthScript maxHp;
    public GameObject healthSlot;
    // Start is called before the first frame update
    void Start()
    {
        maxHp = FindObjectOfType<PlayerHealthScript>().gameObject.GetComponent<PlayerHealthScript>();
    }

    public void RenderHP(int currMaxHP)
    {
        if(hpNum <= maxHp.maxHealth)
        {
            healthSlot.SetActive(true);
        }
        else
        {
            healthSlot.SetActive(false);
        }
    }
}
