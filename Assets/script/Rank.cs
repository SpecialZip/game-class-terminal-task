using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static FormatTimeNamespace.FormatTimer;
public class Rank : MonoBehaviour
{
    
    public List<PlayerDataManager.PlayerData> leaderboardData=new List<PlayerDataManager.PlayerData>();
    public GameObject[] rankList;
    
    private void Start()
    {
        leaderboardData = OnlineMsg.Instance.leaderboardData;
        SortGrades();
        ShowRank();
    }
    
    public void SortGrades()
    {
        leaderboardData.Sort((a,b)=>a.recordTime.CompareTo(b.recordTime));
        for (int i = 0; i < leaderboardData.Count; i++)
        {
            leaderboardData[i].rank = i + 1;
        }
    }

    public void ShowRank()
    {
        for (int i = 0; i < leaderboardData.Count; i++)
        {
            rankList[i].GetComponent<SingleRankUI>().SetRank(leaderboardData[i].rank);
            rankList[i].GetComponent<SingleRankUI>().SetName(leaderboardData[i].name);
            rankList[i].GetComponent<SingleRankUI>().SetTime(leaderboardData[i].isTrackCompleted,leaderboardData[i].recordTime);
        }
    }
}
