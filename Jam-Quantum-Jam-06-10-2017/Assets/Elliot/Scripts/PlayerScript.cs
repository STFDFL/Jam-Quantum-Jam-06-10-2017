using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    internal Animator animator;
    public bool jumping = false;

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "StartGate":
                // Game Start
                break;

            case "Wall":
                //PlayerDead();
                break;

            case "mover":
                GameManager.Instance.KillPlayer(this.gameObject);
                break;

            case "EndGate":
                // Win!
                GameManager gM = GameObject.Find("GameManager").GetComponent<GameManager>();
                gM.WinGame();
                break;

            case "SpawnLoc":
                GameManager.Instance.KillPlayer(this.gameObject);
                break;

            //case "":
            //    break;

            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        int toUse = 0;

        if ((GameManager.Instance.playerVelocity > 0 || GameManager.Instance.playerVelocity < 0) && GameManager.Instance.isJumping == false)
        {
            toUse = 1;
        }

        animator.SetInteger("Speed", toUse);
    }

    public void Jump()
    {
        if (jumping == false)
        {
            animator.SetBool("Jump", true);
            jumping = true;
        }
    }

    public void StopJump()
    {
        if (jumping == true)
        {
            animator.SetBool("Jump", false);
            jumping = false;
        }
    }

    public void PlayerDead()
    {
        if(animator.GetBool("Dead") == false)
        animator.SetBool("Dead", true);
    }
}
