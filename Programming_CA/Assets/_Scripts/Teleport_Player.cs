using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Player : MonoBehaviour
{
    [SerializeField] public Transform tpPoint;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            other.transform.position = tpPoint.position;

        }

    }
}
