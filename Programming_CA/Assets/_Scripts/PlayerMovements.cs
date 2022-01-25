using System;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(CharacterController))]
public class PlayerMovements : MonoBehaviour, PlayerControls.IGameplayActions{

    /*
    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference jumpControl;
    [SerializeField]
    private float playerSpeed = 5.0f;
    [SerializeField]
    private float rotationSpeed = 4.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraMainTransform;
    */
    [SerializeField]
    private PlayerControls playerControls;
    private float playerSpeed = 5.0f;
    float xPos;
    float yPos;
    float zPos;
    private Vector3 Direction { get; set; }

    public void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.SetCallbacks(this);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        // Read Vector 2 from context
        Vector2 direction = context.ReadValue<Vector2>();
        // Set Dirction vector values from direction Vector2
        Direction = new Vector3(direction.x, 0, direction.y);
        //cameraMainTransform = Camera.main.transform;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        
    }

    private void Start()
    {
        //controller = gameObject.GetComponent<CharacterController>();
    }
  

    private void OnEnable()
    {
        playerControls.Enable();
        //movementControl.action.Enable();
        //jumpControl.action.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        //movementControl.action.Disable();
        //jumpControl.action.Disable();
    }

    private void Update()
    {
        // get the current coordinates from the transform
        xPos = transform.position.x;
        yPos = transform.position.y;
        zPos = transform.position.z;

        // Update position of Ship based on input from the Dirction Vector
        xPos += (Direction.x * Time.deltaTime * playerSpeed);
        //yPos = -5;
        // Define forward movement using y value
        zPos += (Direction.z * Time.deltaTime * playerSpeed);

        //now set the position vector to new coordinates
        transform.position = new Vector3(xPos, yPos, zPos);


        /*
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = movementControl.action.ReadValue<Vector2>();//reads input of Movement ActionMap
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);


        if (jumpControl.action.triggered && groundedPlayer)//if Space is pressed and player is touching ground
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;//Puts gravity on player constantly
        controller.Move(playerVelocity * Time.deltaTime);

        if (movement != Vector2.zero) {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
        */
    }

}