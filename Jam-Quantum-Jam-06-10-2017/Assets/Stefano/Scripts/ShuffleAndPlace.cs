using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleAndPlace : MonoBehaviour
{

    public Transform[] spawnPoints;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randomInt = Random.Range(0, GameManager.Instance.moversCollection.Length);
            Instantiate(GameManager.Instance.moversCollection[randomInt], spawnPoints[i].position, Quaternion.identity);
        }
	}
	
}
