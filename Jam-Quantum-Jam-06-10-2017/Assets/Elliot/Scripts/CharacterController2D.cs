using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
    public float f_speed;
    public float f_jumpSpeed;
    public float f_gravity;
    private Vector3 v3_moveDirection = Vector3.zero;
    private CharacterController controller;

    void Awake()
    {
        controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            v3_moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            v3_moveDirection *= f_speed;

            if (Input.GetButton("Jump"))
            {
                v3_moveDirection.y = f_jumpSpeed;
            }
        }

        v3_moveDirection.y -= f_gravity * Time.deltaTime;
        controller.Move(v3_moveDirection * Time.deltaTime);
    }

}