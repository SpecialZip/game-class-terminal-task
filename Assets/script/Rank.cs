using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    private static Rank _instance;
    public static Rank Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Rank>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<Rank>();
                    singletonObject.name = typeof(Rank).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }
    public List<PlayerData> playerDataList=new List<PlayerData>();
    public GameObject[] groups;
    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
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

    public void AddPlayerData(PlayerData playerData)
    {
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
