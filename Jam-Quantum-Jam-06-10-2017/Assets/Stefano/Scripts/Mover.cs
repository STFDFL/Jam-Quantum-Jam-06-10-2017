using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public GameObject[] moverObjects;

    public enum ObstacleType {Rotator, Flyer, Expander, Phaser, Ring,  };
    public ObstacleType obstacleType;
    public Transform[] locations;
    Rigidbody moverRigidBody;

    public float speed;
    public float delay;

    // Use this for initialization
    void Start ()
    {
        switch (obstacleType)
        {
            case ObstacleType.Rotator:
                StartCoroutine(ChangeSpeed(10,40));
                break;
            case ObstacleType.Flyer:              
                break;
            case ObstacleType.Expander:
                StartCoroutine("ExpanderAnimation");
                break;
            case ObstacleType.Phaser:
                moverObjects[0].SetActive(false);
                moverObjects[1].SetActive(false);
                moverObjects[2].SetActive(false);
                break;
            case ObstacleType.Ring:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        switch (obstacleType)
        {
            case ObstacleType.Rotator:
                moverObjects[0].transform.Rotate(Vector3.left * Time.deltaTime * (speed * 10));
                break;
            case ObstacleType.Flyer:
                transform.Rotate(Vector3.right * Time.deltaTime * speed);
                transform.Rotate(Vector3.up * Time.deltaTime * speed);
                moverObjects[0].transform.Rotate(Vector3.right * Time.deltaTime * (speed * 10));
                break;
            case ObstacleType.Expander:               
                break;
            case ObstacleType.Phaser:
                StartCoroutine("Phase");
                break;
            case ObstacleType.Ring:
                transform.Rotate(Vector3.up * Time.deltaTime * speed);
                break;
            default:
                break;
        }
    }

    IEnumerator Phase()
    {
        bool flag = false;
        while (true)
        {
            if(!flag)
            {
                moverObjects[0].SetActive(false);
                moverObjects[1].SetActive(false);
                moverObjects[2].SetActive(false);
                moverObjects[3].SetActive(true);
                moverObjects[4].SetActive(true);
                moverObjects[5].SetActive(true);
                flag = true;
            }
            else
            {
                moverObjects[0].SetActive(true);
                moverObjects[1].SetActive(true);
                moverObjects[2].SetActive(true);
                moverObjects[3].SetActive(false);
                moverObjects[4].SetActive(false);
                moverObjects[5].SetActive(false);
                flag = false;
            }           
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator ChangeSpeed(float speed1, float speed2)
    {
        bool flag = false;
        while (true)
        {
            if(!flag)
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
        float growFactor = 1.1f;
        float waitTime = 0.1f;
        float timer = 0;

        while (true)
        {
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
