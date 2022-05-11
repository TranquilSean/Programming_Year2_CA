using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input_Movement : MonoBehaviour
{

    //movement fields
    float vInput;
    float hInput;
    Vector2 move;
    private Rigidbody rb;
    [SerializeField]
    private float movementForce = .6f;
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;
    private Animator animator;
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();


    }
    void Update()
    {
        // Get the input from vertical/horizontal axis
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        move = new Vector2(hInput,vInput);
        animator.SetFloat("vInput", vInput);
        animator.SetFloat("hInput", hInput);
    }
    private void FixedUpdate()
    {
        forceDirection += hInput * GetCameraRight(playerCamera) * movementForce;
        forceDirection += vInput * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;

        LookAt();

        //ANIMATIONS
        //Check that input to move
        if (vInput > 0.1  || vInput < -0.1 || hInput > 0.1 || hInput < -0.1)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        // Detect Z Key press
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Set runBool to true if pressed
            animator.SetBool("IsRunning", true);
            Debug.Log("Run");
            movementForce = 1f;
        }
        else
        {
            // Set runBool to false if not pressed
            animator.SetBool("IsRunning", false);
            //Debug.Log("No Run");
            movementForce = .6f;
        }
        if (Input.GetKey(KeyCode.Space) && GroundCheck())
        {
            DoJump();
        }
        else
        {
            animator.SetBool("IsJump", false);
        }
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump()
    {
        forceDirection += Vector3.up * jumpForce;
        animator.SetBool("IsJump", true);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsWalking", true);
    }

    private bool GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f + 0.1f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}