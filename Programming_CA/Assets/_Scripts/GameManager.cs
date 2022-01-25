using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public UnityEvent<string> addSpirits;
    private int spirits;    

    private void Start()
    {
        spirits = 0;
        UpdateUI();
    }

    public void AddSpirits(int spiritAmt)
    {
        spirits += spiritAmt;
        UpdateUI();
    }

    private void UpdateUI()
    {
        addSpirits.Invoke(spirits.ToString());
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

}
