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
        recordTimeText.text = FormatTime(PlayerDataManager.Instance.localPlayerData.recordTime);
    }

}
