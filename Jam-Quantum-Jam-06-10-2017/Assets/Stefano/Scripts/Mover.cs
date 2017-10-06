using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public GameObject moverObject;

    public enum ObstacleType {Rotator, Flyer };
    public ObstacleType obstacleType;
    public Transform[] locations;


    public float speed;
    public float delay;

    // Use this for initialization
    void Start ()
    {

        switch (obstacleType)
        {
            case ObstacleType.Rotator:
                StartCoroutine(ChangeSpeed(10,30));
                break;
            case ObstacleType.Flyer:              
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        switch (obstacleType)
        {
            case ObstacleType.Rotator:
                moverObject.transform.Rotate(Vector3.right * Time.deltaTime * (speed * 10));
                break;
            case ObstacleType.Flyer:
                transform.Rotate(Vector3.right * Time.deltaTime * speed);
                transform.Rotate(Vector3.up * Time.deltaTime * speed);
                moverObject.transform.Rotate(Vector3.right * Time.deltaTime * (speed * 10));
                break;
            default:
                break;
        }

        //transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    IEnumerator Phase()
    {
        while(true)
        {

            yield return new WaitForSeconds(delay);

        }
    }

    IEnumerator ChangeSpeed(float speed1, float speed2)
    {
        bool flag = false;
        while (true)
        {
            if(flag == false)
            {
                yield return new WaitForSeconds(delay);
                speed = speed1;
                flag = true;
            }
            else
            {
                yield return new WaitForSeconds(delay);
                speed = speed2;
                flag = false;
            }
        }
    }
}
