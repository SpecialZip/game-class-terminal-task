using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using static FormatTimeNamespace.FormatTimer;
public class End : MonoBehaviour
{
    public TextMeshProUGUI recordTimeText;

    void Start()
    {
        float recordTime = PlayerDataManager.Instance.localPlayerData.recordTime;
        if(recordTime == 0)
        {
            recordTimeText.text = "Incomplete";
        }
        else
        {
            recordTimeText.text = FormatTime(recordTime);
        }
        if(PlayerPrefs.GetString("GameMode", "Local") == "Network")
        {       
            OnlineMsg.Instance.SendScoreToAll(PlayerDataManager.Instance.localPlayerData);
        }
        else
        {
            OnlineMsg.Instance.AddPlayerData(PlayerDataManager.Instance.localPlayerData);
        }
    }

}
