using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data")]
[Serializable]
public class PlayerDataManager :MonoBehaviourPunCallbacks
{
    
    public static PlayerDataManager Instance;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public class PlayerData
    {
        public int rank;
        public string name;
        public float recordTime;
        public int propNum;//道具数量
        public bool isTrackCompleted;
    }
    
    public PlayerData localPlayerData = new PlayerData(); //本地玩家数据
    
    //序列化
    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsWriting)
    //     {
    //         // 发送数据
    //         stream.SendNext(rank);
    //         stream.SendNext(name);
    //         stream.SendNext(recordTime);
    //         stream.SendNext(propNum);
    //         stream.SendNext(isTrackCompleted);
    //     }
    //     else
    //     {
    //         // 接收数据
    //         rank = (int)stream.ReceiveNext();
    //         name = (string)stream.ReceiveNext();
    //         recordTime = (float)stream.ReceiveNext();
    //         propNum = (int)stream.ReceiveNext();
    //         isTrackCompleted = (bool)stream.ReceiveNext();
    //     }
    // }
}
