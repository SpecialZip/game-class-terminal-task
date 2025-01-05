using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerManager : MonoBehaviourPunCallbacks
{
    private PlayerData playerData;
    public PlayerData local;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            // 仅本地玩家创建 PlayerData 实例
            CreatePlayerData();
        }
    }
    
    private void CreatePlayerData()
    {
        // 创建 PlayerData 的实例
        playerData = ScriptableObject.CreateInstance<PlayerData>();
        playerData.name = PhotonNetwork.LocalPlayer.NickName;
        playerData.recordTime = Single.PositiveInfinity;
        playerData.propNum = 0;
        playerData.rank = 0;
    }

    public void addPropNum()
    {
        if(playerData.propNum < 2)
            playerData.propNum++;
    }
    
    public void minusPropNum()
    {
        if(playerData.propNum >0)
            playerData.propNum--;
    }

    public void setRecordTime(float time)
    {
        playerData.recordTime = time;
    }

    public int getPropNum()
    {
        return playerData.propNum;
    }
}
