using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static FormatTimeNamespace.FormatTimer;
public class SingleRankUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI timeText;
    
    public void SetRank(int rank)
    {
        rankText.text = rank.ToString();
    }
    public void SetName(string name)
    {
        nameText.text = name;
    }
    public void SetTime(bool isTrackComplete,float time)
    {
        if (isTrackComplete)
        {
            timeText.text = FormatTime(time);
        }
        else
        {
            timeText.text = "未完成";
        }
    }
}
