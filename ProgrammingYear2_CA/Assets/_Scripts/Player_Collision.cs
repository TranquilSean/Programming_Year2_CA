using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    public GameManager gm;
    public GameObject eco;

    public int ammountToAdd = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spirit"))
        {
            gm.AddPoints(ammountToAdd);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Respawn"))
        {
            gm.SendMessage("gotoStart");
        }
        if (other.gameObject.CompareTag("Chest"))
        {
            eco.SetActive(true);
            //eco.transform.parent = transform;
            Destroy(other.gameObject);
        }
    }
}
