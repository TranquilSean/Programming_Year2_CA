using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcoFollow : MonoBehaviour
{
    public Transform player;
    public float moveStep = 1.0f;
    public float moveSpeed;
    public float rotateStep = 1.0f;
    public float rotateSpeed;

    void Update()
    {
        
        // Smooth out movement/roatation using Time.deltaTime
        moveStep = moveSpeed * Time.deltaTime;
        rotateStep = rotateSpeed * Time.deltaTime;
        transform.LookAt(player);

        //Vector3 playerDistance = player.position - transform.position;
        
        // Calculate the distance between both cubes using Vector3 Distance method 
        float playerDistance = Vector3.Distance(player.position, transform.position);
        Debug.Log(playerDistance);
        if (playerDistance >= 2.0f) // Use Vector3.MoveTowards to move Eco to player
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveStep);
        }

        //Eco Rotate around Player
        transform.RotateAround(player.position, Vector2.up, rotateSpeed);
    }
}

