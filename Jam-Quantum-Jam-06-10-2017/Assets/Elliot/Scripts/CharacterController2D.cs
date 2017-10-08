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

    public void UpdatePlayer(GameObject player)
    {
        thePlayer = player;

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

            if (isGrounded == true)
            {
                velocity.y = 0;

                if (Input.GetAxis("Jump") > 0 && pressedJump == false)
                {
                    GameManager.Instance.isJumping = true;
                    pressedJump = true;

                    velocity.y = GameManager.Instance.jumpSpeed;

                    //play jump sound

                }

                if (Input.GetAxis("Jump") <= 0)
                {
                    GameManager.Instance.isJumping = false;
                    pressedJump = false;
                }
            }
            else
            {
                velocity.y -= GameManager.Instance.fallSpeed * Time.deltaTime;
            }
        }
    }

    private void LateUpdate()
    {
        if (thePlayer != null)
        {
            GameManager.Instance.playerVelocity = Input.GetAxis("Horizontal");

            velocity.x = Mathf.Lerp(velocity.x, GameManager.Instance.runSpeed * GameManager.Instance.playerVelocity, GameManager.Instance.runSpeedDamping);

            if (CheckLeftWall() == true)
            {
                velocity.x -= 10;
            }

            if (CheckRightWall() == true)
            {
                velocity.x += 10;
            }

            Vector3 movementVector = new Vector3(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0);

            //Debug.Log("Velocity X:" + velocity.x.ToString());

            if (velocity.x < 0)
            {
                GameManager.Instance.playerFacingRight = false;

                Vector3 theScale = thePlayer.transform.GetChild(0).transform.localScale;
                theScale.z = -1;
                thePlayer.transform.GetChild(0).transform.localScale = theScale;
            }
            else if (velocity.x > 0)
            {
                GameManager.Instance.playerFacingRight = true;

                Vector3 theScale = thePlayer.transform.GetChild(0).transform.localScale;
                theScale.z = 1;
                thePlayer.transform.GetChild(0).transform.localScale = theScale;
            }

            foreach (GameObject player in GameManager.Instance.activePlayers)
            {
                player.transform.Translate(movementVector);
                player.transform.GetChild(0).localScale = thePlayer.transform.GetChild(0).localScale;

                if (pressedJump == true)
                {
                    if (player.GetComponent<PlayerScript>().animator.GetBool("Jump") == false)
                    {
                        player.GetComponent<PlayerScript>().Jump();
                    }                        
                }
                else if (pressedJump == false && player.transform.GetComponent<PlayerScript>().jumping == true)
                {
                    if (player.GetComponent<PlayerScript>().animator.GetBool("Jump") == true)
                    {
                        player.GetComponent<PlayerScript>().StopJump();
                    }
                }

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