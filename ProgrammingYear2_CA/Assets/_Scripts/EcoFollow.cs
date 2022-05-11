using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcoFollow : MonoBehaviour
{

    public Vector3 destination;
    public Vector3 source;
    public float targetDistance;
    public float playerDistance;
    public bool isScouting;
    public bool isFollowing = true;
    public Transform player;
    public float moveSpeed;
    public float rotateSpeed;
    float distance;

    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        //Set up targets and positionals
        rotateSpeed = .5f;
        isFollowing = true;
    }
    private void Update()
    {
        source = transform.position;
        
        playerDistance = Vector3.Distance(player.position, source);
        targetDistance = Vector3.Distance(source, destination);
   
                
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // Create Ray to determine where click occurred in world space
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Make target == hit point
                destination = hit.point;
                isScouting = true;
                isFollowing = false;
            }
            else isScouting = false;
        }

        if (isScouting)
        {
            this.transform.position = FollowPoint(transform.position, destination);

            if(targetDistance <= .1f)
            {
                isScouting = false;
                isFollowing = true;
            }
        }

        if (isFollowing)
        {
            Idle();
                
        }
    }

    // FollowPoint function
    private Vector3 FollowPoint(Vector3 source, Vector3 target)
    {
        // Get distance between source and target
        distance = Vector3.Distance(source, target);
        // Determine speed based on distance and deceleration factor
        moveSpeed = distance / 0.5f;
        // return position to follow to
        return Vector3.MoveTowards(source, target, Time.deltaTime * moveSpeed);
    }

    public void Idle()
    {

        if (playerDistance >= 2.0f) // Use Vector3.MoveTowards to move Eco to player
        {
            // Get distance between source and target
            distance = Vector3.Distance(source, player.position);
            moveSpeed = distance / 0.5f;
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.RotateAround(player.position, Vector2.up, rotateSpeed);
        }

        transform.LookAt(player);

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.name);
        
        if (other.gameObject.CompareTag("Spirit"))
        {
            gm.AddPoints(1);
            Destroy(other.gameObject);
            Debug.Log("Collected" + other.name);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Debug.Log("Defeated" + other.name);
        }

    }
}

