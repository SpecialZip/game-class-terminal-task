using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform playerPrefab;
    void Start()
    {
        string gameMode = PlayerPrefs.GetString("GameMode", "Local");
        if (gameMode == "Network")
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("本地游戏模式。不需要网络连接。");
            Instantiate(playerPrefab, new Vector3(Random.Range(-10, 0), 1, 0), Quaternion.identity);
        }
    }


    public override void OnConnectedToMaster()
    {
        if (PlayerPrefs.GetString("GameMode", "Local") == "Network")
        {
            base.OnConnectedToMaster();
            Debug.Log("OnConnectedToMaster");
            PhotonNetwork.JoinOrCreateRoom("room",new Photon.Realtime.RoomOptions() { MaxPlayers = 4 },default);
        }
    }

    public override void OnJoinedRoom()
    {
        //PhotonNetwork.Instantiate("GameUI", Vector3.zero, Quaternion.identity, 0);
        if (PlayerPrefs.GetString("GameMode") == "Network")
        {
            PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-10, 0), 1, 0), Quaternion.identity, 0);
        }
    }
}
