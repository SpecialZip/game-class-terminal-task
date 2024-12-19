using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public List<PlayerData> playerDatas=new List<PlayerData>();
    public GameObject[] groups;
    private void Awake()
    {
        playerDatas.AddRange(Resources.LoadAll<PlayerData>(""));
        SortGrades();
        ShowRank();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SortGrades()
    {
        playerDatas.Sort((a,b)=>a.recordTime.CompareTo(b.recordTime));
    }

    public void ShowRank()
    {
        for (int i = 0; i < playerDatas.Count; i++)
        {
            TextMeshProUGUI rank = groups[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            rank.text = playerDatas[i].rank.ToString();
            
            TextMeshProUGUI name = groups[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            name.text = playerDatas[i].name;
            
            TextMeshProUGUI time= groups[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
            time.text = FormatTime(playerDatas[i].recordTime);
        }
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
