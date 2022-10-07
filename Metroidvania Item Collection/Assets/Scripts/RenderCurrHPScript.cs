using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCurrHPScript : MonoBehaviour
{
    public int hpNum;
    public PlayerHealthScript currHp;
    public GameObject healthSlot;
    // Start is called before the first frame update
    void Start()
    {
        currHp = FindObjectOfType<PlayerHealthScript>().gameObject.GetComponent<PlayerHealthScript>();
    }

    public void RenderHP(int currMaxHP)
    {
        if (hpNum <= currHp.currHealth)
        {
            healthSlot.SetActive(true);
        }
        else
        {
            healthSlot.SetActive(false);
        }
    }
}
