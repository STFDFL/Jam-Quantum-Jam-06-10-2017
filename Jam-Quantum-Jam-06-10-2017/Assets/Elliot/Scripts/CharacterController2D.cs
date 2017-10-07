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

            if(CheckLeftWall() == true)
            {
                velocity.x -= 10;
            }

            if(CheckRightWall() == true)
            {
                velocity.x += 10;
            }

            Vector3 movementVector = new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0);

            foreach (GameObject player in GameManager.Instance.activePlayers)
            {
                player.transform.Translate(movementVector);
            }
        }
    }

    bool CheckGrounded()
    {
        if (Physics.Raycast(thePlayer.transform.position, -Vector3.up, distToGround))
        {
            return true;
        }

        return false;
    }

    bool CheckLeftWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(thePlayer.transform.position, -Vector3.left, out hit, coll.bounds.extents.x - 0.2f))
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    bool CheckRightWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(thePlayer.transform.position, -Vector3.right, out hit, coll.bounds.extents.x + 0.2f))
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}