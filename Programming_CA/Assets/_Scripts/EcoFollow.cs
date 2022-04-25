using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcoFollow : MonoBehaviour
{

    Vector3 destination;
    Vector3 source;

    public Transform player;
    public float moveStep = 1.0f;
    public float moveSpeed;
    public float rotateStep = 1.0f;
    public float rotateSpeed;
    float distance;
    

    void FixedUpdate()
    {
        source = transform.position;
        destination = player.position;
        // Smooth out movement/roatation using Time.deltaTime
        moveStep = moveSpeed * Time.deltaTime;
        rotateStep = rotateSpeed * Time.deltaTime;
        
        
        // Calculate the distance between using Vector3 Distance method 
        float playerDistance = Vector3.Distance(player.position, transform.position);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // Create Ray to determine where click occurred in world space
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Make target == hit point
                destination = hit.point;
            }
        }
        this.transform.position = FollowPoint(source, destination);

        if (source == destination && playerDistance >= 2.0f) // Use Vector3.MoveTowards to move Eco to player
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveStep);
        }else 
        //Eco Rotate around Player
        transform.RotateAround(player.position, Vector2.up, rotateSpeed);
        transform.LookAt(player);
    }

// FollowPoint function
    private Vector3 FollowPoint(Vector3 source, Vector3 target)
    {
        // Get distance between source and target
        distance = Vector3.Distance(source, target);
        // Determine speed based on distance and deceleration factor
        moveSpeed = distance / 0.6f;
        // return position to follow to
        return Vector3.MoveTowards(source, target, Time.deltaTime * moveSpeed);
    }
}

