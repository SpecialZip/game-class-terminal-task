using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data")]
[System.Serializable]
public class PlayerData : ScriptableObject,IPunObservable
{
    public int rank;
    public string name;
    public float recordTime;
    public int propNum;//道具数量
    
    //序列化
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 发送数据
            stream.SendNext(rank);
            stream.SendNext(name);
            stream.SendNext(recordTime);
            stream.SendNext(propNum);
        }
        else
        {
            // 接收数据
            rank = (int)stream.ReceiveNext();
            name = (string)stream.ReceiveNext();
            recordTime = (float)stream.ReceiveNext();
            propNum = (int)stream.ReceiveNext();
        }
    }
}

public class PlayerDataMono :MonoBehaviour{
    public PlayerData playerData;
    private static PlayerDataMono _instance;
    public static PlayerDataMono Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerDataMono>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<PlayerDataMono>();
                    _instance.playerData=ScriptableObject.CreateInstance<PlayerData>();
                    singletonObject.name = typeof(PlayerDataMono).ToString() + " (Singleton)";
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
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void setName(string name)
    {
        playerData.name = name;
    }
}

public class PlayerDataTransfer
{
    public int rank;
    public string name;
    public float recordTime;
    public int propNum;
}