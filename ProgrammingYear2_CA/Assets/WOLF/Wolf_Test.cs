using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wolf_Test : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        float dist = Vector3.Distance(location, this.transform.position);
        if (dist <= 5)
        {
            Vector3 fleeVector = location - this.transform.position;
            agent.SetDestination(this.transform.position - fleeVector);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Seek(target.transform.position);
    }
}
