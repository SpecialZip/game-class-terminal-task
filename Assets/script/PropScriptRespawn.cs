using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropScriptRespawn : MonoBehaviour
{ 
    public GameObject propPrefab; // 道具预制体
    private const float respawnTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn(Vector3 position, Quaternion rotation)
    {
        StartCoroutine(WaitingRespawnTime(position, rotation));
        
    }


    IEnumerator WaitingRespawnTime(Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(respawnTime);
        Instantiate(propPrefab,position, rotation, transform);
        Debug.Log(position);Debug.Log(rotation);
    }
}