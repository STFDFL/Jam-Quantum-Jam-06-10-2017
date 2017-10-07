using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public GameObject moverObject;

    public enum ObstacleType {Rotator, Flyer, Expander };
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
            case ObstacleType.Expander:
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
            case ObstacleType.Expander:
                StartCoroutine("ExpanderAnimation");
                break;
            default:
                break;
        }

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

    IEnumerator ExpanderAnimation()
    {
        float maxSize = 7f;
        float growFactor = 0.5f;
        float waitTime = 2f;
        float timer = 0;

        while (true) // this could also be a condition indicating "alive or dead"
        {
            // we scale all axis, so they will have the same value, 
            // so we can work with a float instead of comparing vectors
            while (maxSize > transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }
            // reset the timer

            yield return new WaitForSeconds(waitTime);

            timer = 0;
            while (1 < transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }

            timer = 0;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
