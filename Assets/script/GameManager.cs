using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using static FormatTimeNamespace.FormatTimer;
public class GameManager : MonoBehaviourPun
{
    public static GameManager Instance { get; private set; }
    private const float countdownTime = 10f; // Countdown duration in seconds
    private float contdownTimer = 10f;

    private enum GameState
    {
        BeforeStart, InProgress, CountdownToEnd, GameOver
    }
    private GameState currentState = GameState.BeforeStart;
    private float elapsedTime=0f;              //流动时间
    
    public event EventHandler<CountdownToEndTimer> OnCountdownTimerChanged; //倒计时变化了
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentState= GameState.InProgress;
    }

    void Update()
    {
        // 游戏运行时，时间不断累加
        if(currentState==GameState.InProgress ||currentState==GameState.CountdownToEnd) elapsedTime += Time.deltaTime;
        
    }
    
    
    

    [PunRPC]
    public void PlayerReachedFinishLine()
    {
        //进入结束倒计时
        if (currentState != GameState.CountdownToEnd)
        {
            currentState = GameState.CountdownToEnd;
            StartCoroutine(EndGameCountdown());
        }
    }

    private IEnumerator EndGameCountdown()
    {
        float timer = countdownTime;

        while (timer > 0)
        {
            Debug.Log($"Game ending in {timer} seconds...");
            photonView.RPC("UpdateCountdown", RpcTarget.All, timer);
            yield return new WaitForSeconds(1f);
            timer--;
        }

        if(PlayerPrefs.GetString("GameMode") == "Network")
        {
            //广播结束
            photonView.RPC("EndGame", RpcTarget.All);
        }
        else
        {
            EndGame();
        }
    }

    [PunRPC]
    private void UpdateCountdown(float timeLeft)
    {
        Debug.Log($"Countdown: {timeLeft} seconds remaining.");
        OnCountdownTimerChanged?.Invoke(this, new CountdownToEndTimer(timeLeft));
    }

    //跳转结束界面
    [PunRPC]
    private void EndGame()
    {
        currentState = GameState.GameOver;
        PhotonNetwork.LoadLevel("End");
    }
    
    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}

public class CountdownToEndTimer : EventArgs
{
    public float TimeLeft { get; private set; }

    public CountdownToEndTimer(float timeLeft)
    {
        TimeLeft = timeLeft;
    }
}