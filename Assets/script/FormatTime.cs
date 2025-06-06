﻿using UnityEngine;

namespace FormatTimeNamespace
{
    public static class FormatTimer
    {
        public static string FormatTime(float timeInSeconds)
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

        public static string FormatLaps(int currentLap,int totalLaps)
        {
            currentLap=currentLap>totalLaps?totalLaps:currentLap;
            return string.Format("{0}/{1}", currentLap,totalLaps);
        }
    }
}