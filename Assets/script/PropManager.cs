using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PropManager : MonoBehaviourPunCallbacks
{
    public static PropManager Instance { get; private set; }
    private bool isNetworkGame; // 是否为网络游戏
    private int propNum=0;//道具数量
    private bool propCooling=false;//道具冷却状态
    
    public event EventHandler OnPropCollected; // 道具被收集的事件
    public event EventHandler OnPropConsumed; // 道具被消耗的事件
    
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isNetworkGame = PlayerPrefs.GetString("GameMode", "Local") == "Network";
    }
    
    public int GetPropNum()
    {
        return propNum;
    }
    
    //收集道具
    public void CollectProp()
    {
        if (propNum < 2)
        {
            propNum++;
            OnPropCollected?.Invoke(this, EventArgs.Empty); 
        }
    }
    
    //消耗道具
    public void ConsumeProp()
    {
        if (propNum > 0&& !propCooling)
        {
            propCooling = true;
            propNum--;
            OnPropConsumed?.Invoke(this, EventArgs.Empty);
            StartCoroutine(PropCooldown());
        }
    }
    
    //道具冷却3s
    private IEnumerator PropCooldown()
    {
        yield return new WaitForSeconds(3f);
        propCooling = false;
    }
}
