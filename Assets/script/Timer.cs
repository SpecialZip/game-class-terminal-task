using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public TextMeshPro timerText;
    private float elapsedTime;

    void Start()
    {
        // 获取场景中的Text组件
        if (timerText == null)
        {
            Debug.LogError("未能找到Text组件，请检查对象名称是否正确！");
        }
    }

    void Update()
    {
        // 游戏运行时，时间不断累加
        elapsedTime += Time.deltaTime;
        // 格式化时间并更新Text显示内容
        string formattedTime = FormatTime(elapsedTime);
        timerText.text = formattedTime;
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
