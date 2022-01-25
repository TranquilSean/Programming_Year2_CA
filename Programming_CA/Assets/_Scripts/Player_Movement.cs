using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    //Components Needed
    private PlayerControls playerControls;
    private Rigidbody rbPlayer;


    public float playerSpeed = 5.0f;
    float xPos;
    float yPos;
    float zPos;
    Vector2 moveInput;

    void Start()
    {
        rbPlayer = gameObject.AddComponent<Rigidbody>();
        // Set Up RigidBody Constraints so player doesnt fall over
        rbPlayer.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    public void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        playerControls.Gameplay.Movement.canceled += context => moveInput = Vector2.zero; //no input so (0,0)

        

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
        //create Vector3 from Vector2 inputs to deal with X and Z axis
        rbPlayer.velocity = new Vector3(moveInput.x * playerSpeed, rbPlayer.velocity.y, moveInput.y * playerSpeed); 
    }
}
