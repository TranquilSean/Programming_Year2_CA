using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    public GameManager gm;
    public Inventory_Manager im;
    public GameObject eco;
    public ItemClass Coin;

    public int ammountToAdd = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spirit"))
        {
            gm.AddPoints(ammountToAdd);
            im.Add(Coin);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Respawn"))
        {
            gm.SendMessage("gotoCheckpoint");
        }
        if (other.gameObject.CompareTag("Chest"))
        {
            eco.SetActive(true);
            //eco.transform.parent = transform;
            Destroy(other.gameObject);
        }
    }
}
