using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RootMovement : MonoBehaviour
{
	// Setup a variable to point to the Animator Controller for the character
	Animator animator;
	// Setup 2 float for vertical/horizontal input
	float verticalInput;
	float horizontalInput;

	void Start()
	{
		//get the Animator Controller Component from the character component hierarchy
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		// Get the input from vertical/horizontal axis
		verticalInput = Input.GetAxis("Vertical");
		horizontalInput = Input.GetAxis("Horizontal");
	}
	void FixedUpdate()
	{
		// Now set the animator float values (vAxisInput/hAxisInput)
		animator.SetFloat("vInput", verticalInput);
		animator.SetFloat("hInput", horizontalInput);

		//Check that input to move
		if (verticalInput> 0.1 || horizontalInput >0.1)
        {
			animator.SetBool("IsWalking", true);
        }
        else
        {
			animator.SetBool("IsWalking", false);
		}

		// Detect Z Key press
		if (Input.GetKey(KeyCode.Z))
		{
			// Set runBool to true if pressed
			animator.SetBool("IsRunning", true);
			Debug.Log("Run");

		}
		else
		{
			// Set runBool to false if not pressed
			animator.SetBool("IsRunning", false);
			Debug.Log("No Run");
		}
	}
}
