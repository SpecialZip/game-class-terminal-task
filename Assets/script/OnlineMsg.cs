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
public class OnlineMsg : MonoBehaviourPunCallbacks,IOnEventCallback
{
    private static OnlineMsg _instance;
    public static OnlineMsg Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<OnlineMsg>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<OnlineMsg>();
                    singletonObject.name = typeof(OnlineMsg).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            if (this.gameObject.GetComponent<PhotonView>() == null)
            {
                this.gameObject.AddComponent<PhotonView>();
            }
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void SendScoreToAll(PlayerData playerData)
    {
        //object[] data = new object[] { playerData.name,playerData.recordTime };
        //PhotonNetwork.RaiseEvent(1, data, new RaiseEventOptions { Receivers = ReceiverGroup.All}, SendOptions.SendReliable);
        
        //PhotonView photonView = PhotonView.Get(this);
        //photonView.RPC("AddPlayerDataNameScore", RpcTarget.All, playerData.name,playerData.recordTime);
    }
    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 1)
        {
            // 假设服务器将玩家数据作为事件数据发送回来
            object[] data = (object[])photonEvent.CustomData;
            Debug.Log((string)data[0]);
            PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();
            playerData.name = (string)data[0];
            playerData.recordTime = (float)data[1];
            
            //Rank.Instance.AddPlayerData(playerData);
        }
    }
}
