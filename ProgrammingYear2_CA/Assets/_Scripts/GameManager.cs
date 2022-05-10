using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;
using System;

//STRUCTS
[Serializable]
public struct PlayerData
{
    public string playerName;
    public float health;

}

[Serializable]
public struct StatisticData
{
    public int lastScore;
    public int highScore;
    public int points;
}



public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Vector3 checkpoint;
    public UnityEvent<string> addSpirits;
    public UnityEvent<float> updateHealth;
    private int spirits;

    //===DATA==
    string filePath;
    const string FILE_NAME_PD = "PlayerDataJSONFORMAT.json";

    PlayerData playerStats;

    private void Start()
    {
        playerStats = new PlayerData();


        checkpoint = player.transform.position;
        spirits = 0;
        UpdateUI();
    }

    public void AddPoints(int num)
    {
        spirits += num;
        UpdateUI();
    }

    private void UpdateUI()
    {
        addSpirits.Invoke(spirits.ToString());
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
        if (File.Exists(filePath + "/" + FILE_NAME_PD))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME_PD);
            playerStats = JsonUtility.FromJson<PlayerData>(loadedJson);
            Debug.Log("File loaded successfully");
        }
        else
        {
            ResetPlayerData();
        }

        
    }
    public void SaveGameStatus()
    {
        string playerStatusJson = JsonUtility.ToJson(playerStats);

        File.WriteAllText(filePath + "/" + FILE_NAME_PD, playerStatusJson);

        Debug.Log("File created and saved");
    }

    public void ResetPlayerData()
    {
        playerStats.playerName = "Sean";
        playerStats.health = 100;
    }
}
