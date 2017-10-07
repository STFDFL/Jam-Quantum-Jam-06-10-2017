using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    private GameObject thePlayer;
    private bool isGrounded = false;
    private float distToGround;
    private Vector2 velocity = Vector2.zero;
    private bool pressedJump = false;
    private BoxCollider coll;

    

    public void CharacterControllerConstructor(GameObject player)
    {
        thePlayer = player;

        this.gameObject.AddComponent<CameraFollow>();
        this.gameObject.GetComponent<CameraFollow>().CameraFollowConstructor(player.transform);

        coll = thePlayer.GetComponent<BoxCollider>();
        distToGround = coll.bounds.extents.y;
    }

    void Update()
    {
        if (thePlayer != null)
        {
            // is grounded
            isGrounded = CheckGrounded();

            velocity.x = Mathf.Lerp(velocity.x, GameManager.Instance.runSpeed * Input.GetAxis("Horizontal"), GameManager.Instance.runSpeedDamping);

            if (isGrounded == true)
            {
                velocity.y = 0;

                if (Input.GetButtonDown("Jump") && pressedJump == false)
                {
                    pressedJump = true;

                    velocity.y = GameManager.Instance.jumpSpeed;

                    //play jump sound

                }

                if (Input.GetAxis("Jump") <= 0)
                {
                    pressedJump = false;
                }
            }
            else
            {
                velocity.y -= GameManager.Instance.fallSpeed * Time.deltaTime;
            }

            Vector3 movementVector = new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0);

            thePlayer.transform.Translate(movementVector);
        }
    }

    bool CheckGrounded()
    {
        if (Physics.Raycast(thePlayer.transform.position, -Vector3.up, distToGround - 0.1f))
        {
            return true;
        }

        return false;
    }


}