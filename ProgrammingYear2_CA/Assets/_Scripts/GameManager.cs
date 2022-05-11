using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;
using System;

//STRUCTS
[Serializable]
public struct CollectableStatus
{
    public int coinscollected;
}



public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Vector3 checkpoint;
    public UnityEvent<string> addCoins;
    public UnityEvent<float> updateHealth;
    private int coins;

    //===DATA==
    public CollectableStatus collectableData;
    string filePath;
    const string FILE_NAME = "SaveStatus.json";

    

    private void Start()
    {
        filePath = Application.persistentDataPath;
        collectableData = new CollectableStatus();
        Debug.Log(filePath);
        //startup initialisation
        LoadGameStatus();


        checkpoint = player.transform.position;
        coins = 0;
        UpdateUI();

    }

    public void AddPoints(int num)
    {
        coins += num;
        UpdateUI();
    }

    private void UpdateUI()
    {
        addCoins.Invoke(coins.ToString());
        //updateHealth.Invoke(player);
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    public void gotoCheckpoint()
    {
        player.transform.position = checkpoint;
    }

    public void Respawn()
    {
        
    }

    public void LoadGameStatus()
    {
        if (File.Exists(filePath + "/" + FILE_NAME))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME);
            collectableData = JsonUtility.FromJson<CollectableStatus>(loadedJson);
            Debug.Log("File loaded successfully");
        }
        else
        {
            resetGame();
        }

        
    }
    public void SaveGameStatus()
    {
        string collectableDataJson = JsonUtility.ToJson(collectableData);

        File.WriteAllText(filePath + "/" + FILE_NAME, collectableDataJson);

        Debug.Log("File created and saved");
    }

    public void resetGame()
    {
        collectableData.coinscollected = 0;

        SaveGameStatus();
    }
}
