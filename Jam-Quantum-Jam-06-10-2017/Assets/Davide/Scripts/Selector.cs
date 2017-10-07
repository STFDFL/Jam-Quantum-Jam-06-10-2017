using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selector : MonoBehaviour {

    public GameObject[] lights;
    int lightActive;

	// Use this for initialization
	void Start ()
    {
        lightActive = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.S))
        {
            if (lightActive < 2)
            {
                lightActive++;
                foreach (var light in lights)
                {
                    light.SetActive(false);
                }
                lights[lightActive].SetActive(true);
            }          
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(lightActive > 0)
            {
                lightActive--;
                foreach (var light in lights)
                {
                    light.SetActive(false);
                }
                lights[lightActive].SetActive(true);
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
                SceneManager.LoadScene("");
            }
            if (lightActive == 2)
            {
                Application.Quit();
            }
        }

    }
}
