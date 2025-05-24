using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static FormatTimeNamespace.FormatTimer;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;        //进行时间
    public TextMeshProUGUI maxLapTimeText;  //最大圈速
    public TextMeshProUGUI lapsText;        //圈数
    public TextMeshProUGUI recordBestTimeText;//个人最佳成绩
    public TextMeshProUGUI speedText;       //车速
    private int totalLaps;               //总圈数

    private void Start()
    {
        LapCounter.Instance.OnLapCountChanged += UpdateLapUI;
        totalLaps=LapCounter.Instance.GetTotalLaps();
    }


    private void UpdateLapUI(object sender, EventArgs e)
    {
        // 更新最大圈速UI
        float maxLapTime = LapCounter.Instance.GetMaxLapTime();
        maxLapTimeText.text = maxLapTime is Single.PositiveInfinity?FormatTime(0):FormatTime(maxLapTime);

        // 更新圈数UI
        int currentLap = LapCounter.Instance.GetCurrentLap();
        lapsText.text = FormatLaps(currentLap, totalLaps);

        // 更新个人最佳成绩UI
        float recordBestTime = LapCounter.Instance.GetRecordBestTime();
        if (recordBestTime == Single.PositiveInfinity)
        {
            recordBestTime = 0;
        }
        recordBestTimeText.text = FormatTime(recordBestTime);

    }

    private void Update()
    {
        // 更新速度UI
        string formattedTime = FormatTime(GameManager.Instance.GetElapsedTime());
        timeText.text = formattedTime;
    }


    public string FormatTime(float timeInSeconds)
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

    public string FormatLaps(int currentLap,int totalLaps)
    {
        currentLap=currentLap>totalLaps?totalLaps:currentLap;
        return string.Format("{0}/{1}", currentLap,totalLaps);
    }

    
}
