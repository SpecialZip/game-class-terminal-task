using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; } // 单例实例
    public GameObject playerPrefab; // 玩家预制体
    public Vector3 spawnPosition = new Vector3(0, 0, 0); // 玩家出生位置

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartLocalGame()
    {
        // 本地游戏
        PlayerPrefs.SetString("GameMode", "Local");
        // 加载游戏场景
        SceneManager.LoadScene("Game");
        // 订阅场景加载事件
        SceneManager.sceneLoaded += OnLocalGameSceneLoaded;
    }
    private void OnLocalGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            // 在游戏场景中实例化玩家
            if (playerPrefab != null)
            {
                Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Player prefab is not assigned!");
            }

            // 取消订阅场景加载事件
            SceneManager.sceneLoaded -= OnLocalGameSceneLoaded;
        }
    }
    
    public void StartNetworkGame()
    {
        // 网络联机游戏
        PlayerPrefs.SetString("GameMode", "Network");
        PhotonNetwork.LoadLevel("Game");
    }

    public void toStart(){
        SceneManager.LoadScene("Start");    
    }
    
    public void toIntroduction(){
        SceneManager.LoadScene("Introduction");
    }
    
    public void toRank()
    {
        SceneManager.LoadScene("Rank");
    }
}
