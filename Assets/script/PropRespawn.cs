using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 10s后重新生成道具
/// </summary>
public class PropRespawn : MonoBehaviour
{ 
    public GameObject propPrefab; // 道具预制体
    private const float respawnTime = 10f;


    public void Respawn(Vector3 position, Quaternion rotation)
    {
        StartCoroutine(WaitingRespawnTime(position, rotation));
    }


    IEnumerator WaitingRespawnTime(Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(respawnTime);
        Instantiate(propPrefab,position, rotation, transform);
        //Debug.Log(position);Debug.Log(rotation);
    }
}