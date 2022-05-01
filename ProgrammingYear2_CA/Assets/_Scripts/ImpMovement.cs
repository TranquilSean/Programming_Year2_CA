using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpMovement : MonoBehaviour
{

    Animator anim;
    public float speedMultiplier = 1.0f;
    public Vector2 velocity = Vector2.zero;
    public bool shouldMove;

    void Start()
    {

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity.x = speedMultiplier * Input.GetAxis("Horizontal");
        velocity.y = speedMultiplier * Input.GetAxis("Vertical");
        shouldMove = velocity.sqrMagnitude > Mathf.Epsilon;
        anim.SetBool("move", shouldMove);
        anim.SetFloat("velx", velocity.x);
        anim.SetFloat("vely", velocity.y);
    }
}