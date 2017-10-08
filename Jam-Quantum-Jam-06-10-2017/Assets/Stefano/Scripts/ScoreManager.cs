using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public GameObject[] neutrons;

	// Use this for initialization
	void Start ()
    {
        int previousResult = PlayerPrefs.GetInt("PlayersLeft");

        for (int i = 0; i < previousResult; i++)
        {
            neutrons[i].SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
