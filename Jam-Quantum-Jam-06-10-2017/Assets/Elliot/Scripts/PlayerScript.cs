using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        switch (other.gameObject.tag)
        {
            case "StartGate":
                // Game Start
                break;

            case "Wall":
                Debug.Log("Wall!");
                GameManager.Instance.stopMovement = true;
                break;

            case "obstacle":
                GameManager.Instance.KillPlayer(this.gameObject);
                break;

            case "EndGate":
                // Win!
                break;

            //case "":
            //    break;

            default:
                break;
        }
    }
}
