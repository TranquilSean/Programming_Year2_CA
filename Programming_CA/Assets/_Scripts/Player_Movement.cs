using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    //Components Needed
    PlayerControls playerControls;
    Rigidbody rbPlayer;
    Transform cam;
    Animator animator;

    [Header("Movement Variables")]
    public float playerSpeed = 10.0f;
    public float rotateSpeed = 3.0f;
    public float gravity = -8.9f;
    public float jumpForce = 10.0f;
    bool jumpInput;
    bool sprintInput;

    Vector2 moveInput;
    Vector3 currentMovement;
    bool isMovementPressed;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        cam = Camera.main.transform;
        animator = GetComponentInChildren<Animator>();
        rbPlayer = gameObject.AddComponent<Rigidbody>();
        // Set Up RigidBody Constraints so player doesnt fall over
        rbPlayer.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    public void Awake()
    {
        playerControls = new PlayerControls();

        playerControls.Gameplay.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        playerControls.Gameplay.Movement.canceled += context => moveInput = Vector2.zero; //no input so (0,0)
        playerControls.Gameplay.Jump.performed += context => jumpInput = true;
        playerControls.Gameplay.Jump.canceled += context => jumpInput = false; ;

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void FixedUpdate()
    {
        /* Adding force/move torque/rotate didnt work.
        rbPlayer.AddTorque(transform.up * torque * moveInput.x);
        rbPlayer.AddForce(transform.forward * moveInput.y * playerSpeed);
        */
    }

    
    
    
    
    
    void Move()
    {
        //gravity
        rbPlayer.AddForce(Vector3.down * gravity);

        //create Vector3 from Vector2 inputs to deal with X and Z axis
        rbPlayer.velocity = transform.forward * (moveInput.y * playerSpeed);

        //Roatating
        transform.Rotate(Vector3.up * (rotateSpeed * moveInput.x) * Time.deltaTime);

        animator.SetFloat("vInput", (moveInput.x));
        animator.SetFloat("hInput", (moveInput.y));

        if( moveInput.x != 0 || moveInput.y != 0)
        {
            animator.SetBool("IsWalking", true);
        } else 
            animator.SetBool("IsWalking", false);

    }
    void Jump()
    {
        if (jumpInput)
        {
            // the cube is going to move upwards in 10 units per second
            rbPlayer.velocity = transform.up * jumpForce;
            //rbPlayer.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("IsJump", true);
            Debug.Log("jump");
        }else animator.SetBool("IsJump", false);
    }
    public void gotoStart()
    {
        transform.position = startPos; //Return the player to the checkpoint
    }
}
