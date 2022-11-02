using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUpManagerScript : MonoBehaviour
{
    public TutorialPopUpManagerScript instance;
    public GameObject tutorialCanvas;
    public List<TutorialPopUpScript> tutorials;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tutorials.AddRange(GetComponents<TutorialPopUpScript>());
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialCanvas.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
             
    }

    public TutorialPopUpScript FindTutorialEntry(string str)
    {
        for (int i = 0; i < tutorials.Count; i++)
        {
            if (tutorials[i].title == str)
            {
                return tutorials[i];
            }
        }
        return null;
    }

    public void Unlock(string str)
    {
        TutorialPopUpScript tutorial = FindTutorialEntry(str);
        tutorial.TutorialActivate();
    }
}
