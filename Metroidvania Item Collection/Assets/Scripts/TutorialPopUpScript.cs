using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialPopUpScript : MonoBehaviour
{
    public string title;
    [TextArea(8, 50)]
    public string description;
    public GameObject tutorial;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public void TutorialActivate()
    {
        tutorial.SetActive(true);
        descriptionText.text = description;
        titleText.text = title;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
