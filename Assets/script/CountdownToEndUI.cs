using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownToEndUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI msg;
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        GameManager.Instance.OnCountdownTimerChanged+= UpdateCountdownText;
        msg.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);
    }

    private void UpdateCountdownText(object sender, CountdownToEndTimer e)
    {
        float countdownTime = e.TimeLeft;
        countdownText.text = countdownTime.ToString();
        
        // 显示倒计时消息
        msg.gameObject.SetActive(true);
        countdownText.gameObject.SetActive(true);
        
        if (countdownTime <= 0)
        {
            msg.gameObject.SetActive(false);
            countdownText.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCountdownTimerChanged -= UpdateCountdownText;
    }
}
