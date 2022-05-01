using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce_Platform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Create Physics Material 
        PhysicMaterial rubber = new PhysicMaterial("Rubber");
        // Set physic mat varaibles (maxbounce) 
        rubber.bounciness = 1.0f;
        rubber.staticFriction = 0.1f;
        rubber.dynamicFriction = 0.1f;

    // Update the Collider 
    GetComponent<BoxCollider>().material = rubber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
