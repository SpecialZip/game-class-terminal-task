using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class End : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI recordTimeText;
    // Start is called before the first frame update
    private void Awake()
    {
        recordTimeText.text = FormatTime(playerData.recordTime);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
