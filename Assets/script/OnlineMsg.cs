using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
/// <summary>
/// 网络消息传输处理，单例模式，处理排行榜数据。
/// </summary>
public class OnlineMsg : MonoBehaviourPun
{
    public static OnlineMsg Instance { get; private set; }
    public List<PlayerDataManager.PlayerData> leaderboardData { get; private set; } = new List<PlayerDataManager.PlayerData>();//排行榜数据

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SendScoreToAll(PlayerDataManager.PlayerData playerData)
    {
        photonView.RPC("ReceiveScore", RpcTarget.All, playerData);
    }
    
    //客户端接受数据
    [PunRPC]
    public void ReceiveScore(PlayerDataManager.PlayerData playerData)
    {
        leaderboardData.Add(playerData);
    }
    
    //单机准备
    public void AddPlayerData(PlayerDataManager.PlayerData playerData)
    {
        leaderboardData.Add(playerData);
    }
}
