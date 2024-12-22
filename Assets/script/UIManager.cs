using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerData playerData;           //存储数据
    public TextMeshProUGUI timeText;        //进行时间
    public TextMeshProUGUI maxLapTimeText;  //最大圈速
    public TextMeshProUGUI lapsText;        //圈数
    public TextMeshProUGUI recordBestTimeText;//个人最佳成绩
    public Image[] props;                   //道具
    public GameObject speedUpUI;            //氮气加速UI图片

    private float maxLapTime=Single.PositiveInfinity;               //最大圈速
    private float lapTimeStart;             //圈速记录开始
    private float lapTimeEnd;               //圈速记录结束
    private float recordTime;               //本次记录
    private float recordBestTime=Single.PositiveInfinity;           //个人记录
    private float elapsedTime;              //时间
    private int currentLap;                 //当前第几圈
    private int totalLaps=2;                //一共几圈
    public GameObject startPoint;           //起点
    private Ray startPointRay;              //起点射线
    private bool carPassing = false;        //经过起点
    private int propNum = 0;                //道具数量
    void Start()
    {
        // 获取场景中的Text组件
        if (timeText == null)
        {
            Debug.LogError("未能找到Text组件，请检查对象名称是否正确！");
        }

        startPointRay = new Ray(startPoint.transform.position, startPoint.transform.right);
        
    }

    void Update()
    {
        // 游戏运行时，时间不断累加
        if(currentLap>0 && currentLap<=totalLaps) elapsedTime += Time.deltaTime;
        // 格式化时间并更新Text显示内容
        string formattedTime = FormatTime(elapsedTime);
        timeText.text = formattedTime;
        
        Debug.DrawRay(startPointRay.origin,startPointRay.direction,Color.red);

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

    //更新圈数
    IEnumerator UpdateLaps()
    {
        carPassing = true;
        //更新一圈的时间
        lapTimeEnd = elapsedTime;
        if (currentLap>0) maxLapTime = Mathf.Min(maxLapTime, lapTimeEnd - lapTimeStart);
        lapTimeStart = elapsedTime;
        maxLapTimeText.text = maxLapTime is Single.PositiveInfinity?FormatTime(0):FormatTime(maxLapTime);

        //更新圈数
        currentLap++;
        lapsText.text = FormatLaps(currentLap, totalLaps);
        
        //结束
        if (currentLap == totalLaps+1)
        {
            recordTime = elapsedTime;
            if (recordBestTime > recordTime)
            {
                recordBestTime=recordTime;
                recordBestTimeText.text = FormatTime(recordBestTime);
            }
            
            //记录个人数据
            playerData.recordTime=recordTime;
            //进入结算画面
            StartCoroutine(Ending());
        }

        yield return new WaitForSeconds(5f);
        
        carPassing = false;    
    }
    
    
    //更新道具栏UI
    public void UpdateProps(List<CarController.Props> propSlot)
    {
        int propRealNum = propSlot.Count;
        
        
        if (propRealNum== 0)
        {
            GameObject firstProp = props[0].transform.Find("SpeedUp(Clone)").gameObject;
            Destroy(firstProp);
        }
        else if (propSlot.Count == 1)
        {
            if (propNum < propRealNum)
            {
                Instantiate(speedUpUI, props[0].transform);
            }
            else
            {
                GameObject secondProp = props[1].transform.Find("SpeedUp(Clone)").gameObject;
                Destroy(secondProp);
            }
        }
        else if(propSlot.Count == 2)
        {
            Instantiate(speedUpUI, props[1].transform);
        }
        propNum = propRealNum;
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

    //进入End场景
    public IEnumerator Ending()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("End");
    }
}
