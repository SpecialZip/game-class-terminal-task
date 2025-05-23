using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Minimap : MonoBehaviour
{
    public Transform player; // 玩家对象的 Transform
    public Vector3 offset = new Vector3(0, 50, 0); // 小地图相机的偏移量
    //(-40,100,20)相机全景坐标

    private bool isNetworkGame;// 是否为网络游戏
    void Start()
    {
        isNetworkGame=PlayerPrefs.GetString("GameMode") == "Network";
        StartCoroutine(FindPlayer());
    }

    // 协程：等待玩家加载完成
    private IEnumerator FindPlayer()
    {
        while (player == null)
        {
            GameObject localPlayer = GameObject.FindWithTag("Player");
            if (isNetworkGame)
            {
                if (localPlayer != null && localPlayer.GetComponent<PhotonView>().IsMine)
                {
                    player = localPlayer.transform;
                }
            }
            else
            {
                if (localPlayer != null)
                {
                    player = localPlayer.transform;
                }
            }
            
            yield return null;
        }
    }

    // LateUpdate 用于更新小地图相机的位置
    void LateUpdate()
    {
        if (player != null)
        {
            // 更新小地图相机的位置，使其跟随玩家
            transform.position = player.position + offset;
        }
    }
}