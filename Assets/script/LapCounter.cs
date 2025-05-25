using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using static FormatTimeNamespace.FormatTimer;
/// <summary>
/// 圈数计数器
/// </summary>
public class LapCounter : MonoBehaviour
{
    private static LapCounter _instance;
    public static LapCounter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LapCounter>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(LapCounter).Name);
                    _instance = singletonObject.AddComponent<LapCounter>();
                }
            }
            return _instance;
        }
    }
    
    private bool isNetworkGame = false; //是否是网络游戏
    private float maxLapTime=Single.PositiveInfinity;               //最大圈速
    private float lapTimeStart;             //圈速记录开始
    private float lapTimeEnd;               //圈速记录结束
    private float recordTime;               //本次记录
    private float recordBestTime=Single.PositiveInfinity;           //个人记录
    private int currentLap;                 //当前第几圈
    private int totalLaps=1;                //一共几圈
    [SerializeField] private GameObject startPoint;           //起点
    private Ray startPointRay;              //起点射线
    private bool carPassing = false;        //经过起点
    private bool isTrackCompleted = false;  //赛道是否完成
    public bool IsTrackCompleted
    {
        get => isTrackCompleted;        
        private set => isTrackCompleted = value; 
    }
    
    public event EventHandler OnLapCountChanged; // 圈数变化了

    private void Start()
    {
        startPointRay = new Ray(startPoint.transform.position, startPoint.transform.right);
        isNetworkGame = PlayerPrefs.GetString("GameMode", "Local") == "Network";
        //playerData = GameObject.Find("PlayerDataObject").GetComponent<PlayerData>();
    }

    private void Update()
    {
        Debug.DrawRay(startPointRay.origin,startPointRay.direction,Color.red);

        if (isNetworkGame)
        {
            if (!carPassing)
            {
                RaycastHit hit;
                float rayLength = 10f;
                if (Physics.Raycast(startPointRay, out hit, rayLength))
                {
                    Debug.Log("碰到的物体是："+hit.transform.name);
                    PhotonView photonView = hit.collider.GetComponent<PhotonView>();
                    if (photonView != null && photonView.IsMine)
                    {
                        StartCoroutine(UpdateLaps());
                    }
                }
            }
        }
        else
        {
            if (!carPassing)
            {
                RaycastHit hit;
                float rayLength = 10f;
                if (Physics.Raycast(startPointRay, out hit, rayLength))
                {
                    StartCoroutine(UpdateLaps());
                }
            }
        }
        
    }

    //更新圈数
    IEnumerator UpdateLaps()
    {
        carPassing = true;
        //更新圈速
        UpdateLapTime();

        //更新圈数
        currentLap++;
        
        //结束
        if (currentLap == totalLaps+1)
        {
            //标记赛道完成
            isTrackCompleted = true;
            //记录时间
            recordTime = GameManager.Instance.GetElapsedTime();
            //更新最佳时间
            if (recordBestTime > recordTime)
            {
                recordBestTime=recordTime;
            }
            
            //记录个人数据
            PlayerDataManager.Instance.localPlayerData.recordTime=recordTime;
            PlayerDataManager.Instance.localPlayerData.isTrackCompleted=true;
            
            string gameMode = PlayerPrefs.GetString("GameMode", "Local");
            if (gameMode == "Network")
            {
                //广播结束
                GameManager.Instance.photonView.RPC("PlayerReachedFinishLine", RpcTarget.All);
            }
            else
            {
                GameManager.Instance.PlayerReachedFinishLine();
            }
            
            //进入结算画面
            StartCoroutine(Ending());
            
            
        }
        OnLapCountChanged?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(5f);
        
        carPassing = false;    
    }

    //更新圈速
    private void UpdateLapTime()
    {
        //更新一圈的时间
        lapTimeEnd = GameManager.Instance.GetElapsedTime();
        if (currentLap>0) maxLapTime = Mathf.Min(maxLapTime, lapTimeEnd - lapTimeStart);
        lapTimeStart = GameManager.Instance.GetElapsedTime();
    }
    
    //获取圈速
    public float GetMaxLapTime()
    {
        return maxLapTime;
    }
    
    //获取圈数
    public int GetCurrentLap()
    {
        return currentLap;
    }
    //获取总圈数
    public int GetTotalLaps()
    {
        return totalLaps;
    }
    //获取最佳成绩
    public float GetRecordBestTime()
    {
        return recordBestTime;
    }
    //进入End场景
    public IEnumerator Ending()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("End");
    }
}
