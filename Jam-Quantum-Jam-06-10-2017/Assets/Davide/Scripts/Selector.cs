using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selector : MonoBehaviour {

    public GameObject[] lights;
    public GameObject[] neutrons;
    int lightActive;

	// Use this for initialization
	void Start ()
    {
        lightActive = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.D))
        {
            if (lightActive < 2)
            {
                lightActive++;
                foreach (var light in lights)
                {
                    light.SetActive(false);
                }
                lights[lightActive].SetActive(true);
                foreach (var n in neutrons)
                {
                    n.SetActive(false);
                }
                neutrons[lightActive].SetActive(true);
            }          
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(lightActive > 0)
            {
                lightActive--;
                foreach (var light in lights)
                {
                    light.SetActive(false);
                }
                lights[lightActive].SetActive(true);
                foreach (var n in neutrons)
                {
                    n.SetActive(false);
                }
                neutrons[lightActive].SetActive(true);
            }         
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (lightActive == 0)
            {
                SceneManager.LoadScene("Elliot_Scene");
            }
            if (lightActive == 1)
            {
                //TODO add CreditScene
                SceneManager.LoadScene("CheckThisOut");
            }
            if (lightActive == 2)
            {
                Application.Quit();
            }
        }

    }
}
