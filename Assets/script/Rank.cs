using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    
    public List<PlayerData> playerDataList=new List<PlayerData>();
    public GameObject[] groups;
    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        AddPlayerData(PlayerDataMono.Instance.playerData);
    }
    
    
    public void SortGrades()
    {
        playerDataList.Sort((a,b)=>a.recordTime.CompareTo(b.recordTime));
        for (int i = 0; i < playerDataList.Count; i++)
        {
            playerDataList[i].rank = i + 1;
        }
    }

    public void ShowRank()
    {
        for (int i = 0; i < playerDataList.Count; i++)
        {
            TextMeshProUGUI rank = groups[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            rank.text = playerDataList[i].rank.ToString();
            
            TextMeshProUGUI name = groups[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            name.text = playerDataList[i].name;
            
            TextMeshProUGUI time= groups[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
            time.text = FormatTime(playerDataList[i].recordTime);
        }
    }
    
    public void SendScoreToOther(PlayerData playerData)
    {
        //object[] data = new object[] { playerData.name,playerData.recordTime };
        //PhotonNetwork.RaiseEvent(1, data, new RaiseEventOptions { Receivers = ReceiverGroup.All}, SendOptions.SendReliable);
        
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("AddPlayerDataNameScore", RpcTarget.All, playerData.name,playerData.recordTime);
    }    
    
    [PunRPC]
    public void AddPlayerData(PlayerData playerData)
    {
        playerDataList.Add(playerData);
        SortGrades();
        ShowRank();
    }

    [PunRPC]
    public void AddPlayerDataNameScore(string name, float recordTime)
    {
        PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();
        playerData.name = name;
        playerData.recordTime = recordTime;
        playerDataList.Add(playerData);
        SortGrades();
        ShowRank();
    }
    
    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        int milliseconds = Mathf.FloorToInt((timeInSeconds * 1000f) % 1000f);

        // 格式化分钟、秒和毫秒，确保都占两位（不足两位前面补0）
        string minutesStr = minutes.ToString("00");
        string secondsStr = seconds.ToString("00");
        string millisecondsStr = milliseconds.ToString("000");

        return string.Format("{0}'{1}''{2}", minutesStr, secondsStr, millisecondsStr);
    }
}
