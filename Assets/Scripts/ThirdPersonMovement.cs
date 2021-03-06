using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    Animator anim;
    public bool walk;

    public float speed = 6f;
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;
    public float gravity;
    private Vector3 velocity;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("Walk", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }
        else
        {
            anim.SetBool("Walk", false);
        }
        Gr();
    }
    void Gr()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded && velocity.y < 0)

            velocity.y = gravity;

        //if (isGrounded && Input.GetKey(KeyCode.Space))
        //{

        //    if (isRunning)
        //        velocity.y = Mathf.Sqrt(-2f * runJumpHeight * gravity);
        //    else
        //        velocity.y = Mathf.Sqrt(-2f * walkJumpHeight * gravity);

        //    anim.SetBool("Jump", true);

        //}

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            // anim.SetBool("Jump", false);
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
