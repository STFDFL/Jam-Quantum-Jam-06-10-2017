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

                break;

            case "mover":
                GameManager.Instance.KillPlayer(this.gameObject);
                break;

            case "EndGate":
                // Win!
                GameManager gM = GameObject.Find("GameManager").GetComponent<GameManager>();
                gM.WinGame();
                break;

            //case "":
            //    break;

            default:
                break;
        }
    }
}
