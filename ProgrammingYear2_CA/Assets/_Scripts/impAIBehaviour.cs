using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class impAIBehaviour : MonoBehaviour
{

	// Reference to NavMeshAgent component
	NavMeshAgent navAgent;
	Vector3 destination;

	// Canvas Array
	public TextMeshProUGUI canvasUpdate;

	// Decision Tree control booleans 
	public bool isVisible;
	public bool isAudible;
	public bool isClose;

	// Also to the game object we will follow
	public Transform targetObject;

	//Controlling Navmesh and Animation
	Vector3 worldDeltaPosition;
	Vector2 groundDeltaPosition;
	Vector2 velocity = Vector2.zero;
	

	// Animator Object
	Animator anim;

	// Waypoints for Patrol functionality
	int nextIndex;
	public GameObject[] waypoints;
	Vector3 origin;

	// Sight variables
	public float fieldOfViewAngle = 360f;


	void Start()
	{
		// Get Animator & navAgent
		anim = GetComponent<Animator>();
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		//Set Orgin
		origin = new Vector3(54, 0, 64);

		navAgent.updatePosition = false;

		destination = NextWaypoint(origin);
	}

	void Update()
	{
		if (targetObject) //run the functions
		{
			isPlayerVisible();
			isPlayerAudible();
			isPlayerClose();


			// Logic Checks for seen and nearby
			if (isVisible && isClose)
			{
				seekFunction();
			}
			else if (isVisible && !isClose)
			{
				patrolFunction();
			}
			else if (!isVisible && !isAudible)
			{
				IdleFunction();
			}
			else if (!isVisible && isAudible)
			{
				patrolFunction();
			}
		}
		else
		{
			// If RandomBall has been destroyed then Idle
			IdleFunction();
		}

		navAgent.SetDestination(destination);

		worldDeltaPosition = navAgent.nextPosition - transform.position;

		groundDeltaPosition.x = Vector3.Dot(transform.right, worldDeltaPosition);
		groundDeltaPosition.y = Vector3.Dot(transform.forward, worldDeltaPosition);

		velocity = (Time.deltaTime > 1e-5f) ? groundDeltaPosition / Time.deltaTime : velocity = Vector2.zero;

		bool shouldMove = velocity.magnitude > 0.01f && navAgent.remainingDistance > navAgent.radius;

		anim.SetBool("move", shouldMove);
		anim.SetFloat("velx", velocity.x);
		anim.SetFloat("vely", velocity.y);
	}


	void OnAnimatorMove()
	{

		transform.position = navAgent.nextPosition;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Player")
		{
			// Stop the chase by destroying the yellow ball
			//Destroy(col.gameObject);
			Debug.Log("Got Ya");

		}
	}
	// Seek out RandomBall
	void seekFunction()
	{
		// Seek out the Enemy
		destination = targetObject.position;
		// Update Canvas with current state
		canvasUpdate.text = "Seeking";
	}
	// Patrol Array of cubes (added to script)
	void patrolFunction()
	{
		// If within 2.5, then move onto next waypoint in array
		if (Vector3.Distance(transform.position, destination) < 2.5)
		{
			destination = NextWaypoint(destination);
		}
		// Update Canvas with current state
		canvasUpdate.text = "Patrolling";
	}
	
	void IdleFunction()
	{
		// Idle at 0
		destination = origin;
		// Update Canvas with current state
		canvasUpdate.text = "Idling";
	}

	// Function that loops through waypoints for the Patrol fucntionality
	public Vector3 NextWaypoint(Vector3 currentPosition)
	{
		Debug.Log(currentPosition);
		if (currentPosition != origin)
		{
			// Find array index of given waypoint
			for (int i = 0; i < waypoints.Length; i++)
			{
				// Once found calculate next one
				if (currentPosition == waypoints[i].transform.position)
				{
					// Modulus operator helps to avoid to go out of bounds
					// And resets to 0 the index count once we reach the end of the array
					nextIndex = (i + 1) % waypoints.Length;
				}
			}
		}
		else
		{
			// Default is first index in array 
			nextIndex = 0;
		}
		return waypoints[nextIndex].transform.position;
	}

	// Checks if Player is visible using Raycast
	public void isPlayerVisible()
	{

		// Create a vector from the enemy to the player and store the angle between it and forward.
		Vector3 direction = targetObject.transform.position - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);

		// Create NavMesh hit var
		NavMeshHit hit;

		// If the Ray cast hits something other than the target, then true is returned, if not false
		// So !hit is used to specify visibility and...
		// If the angle between forward and where the player is, is less than half the angle of view...
		if (!navAgent.Raycast(targetObject.transform.position, out hit) && angle < fieldOfViewAngle * 0.5f)
		{
			// ... the player is Visible
			isVisible = true;

		}
		else
		{
			// ... the player is Not Visible
			isVisible = false;
		}
	}

	// Checks if player is Audible using simple distance calculation
	public void isPlayerAudible()
	{
		// If direct distance < 20, then audible
		if (Vector3.Distance(transform.position, targetObject.position) < 30.0f)
		{
			// Is Audible
			isAudible = true;
		}
		else
		{
			// Is not Audible
			isAudible = false;
		}
	}

	// Check is Player is CloseBy based on the NavMesh
	// Distance to Player via the NavMesh is calculated here
	// If Distance < 30, then isClose == true
	public void isPlayerClose()
	{

		// Create a path and set it based on a target position.
		NavMeshPath path = new NavMeshPath();
		if (navAgent.enabled)
			navAgent.CalculatePath(targetObject.position, path);

		// Create an array of points which is the length of the number of corners in the path + 2.
		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

		// The first point is the enemy's position.
		allWayPoints[0] = transform.position;

		// The last point is the target position.
		allWayPoints[allWayPoints.Length - 1] = targetObject.position;

		// The points inbetween are the corners of the path.
		for (int i = 0; i < path.corners.Length; i++)
		{
			allWayPoints[i + 1] = path.corners[i];
		}

		// Create a float to store the path length that is by default 0.
		float pathLength = 0;

		// Increment the path length by an amount equal to the distance between each waypoint and the next.
		for (int i = 0; i < allWayPoints.Length - 1; i++)
		{
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		}

		if (pathLength < 30.0f)
		{
			// Set Close Bool true
			isClose = true;
		}
		else
		{
			// Set Close Bool false
			isClose = false;
		}
	}
}
