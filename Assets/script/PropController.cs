using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PropController : MonoBehaviourPunCallbacks
{
    public Vector3 rotationSpeed = new Vector3(0, 1, 0); // 定义旋转速度
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed);
    }
    
    //物体杯碰撞后消失，10s后重新出现
    private void OnTriggerEnter(Collider other)
    {
        Vector3 position =gameObject.transform.position;
        Quaternion rotation = other.gameObject.transform.rotation;
        //Debug.Log(position);Debug.Log(rotation);
        GameObject.Find("Props").GetComponent<PropRespawn>().Respawn(position, rotation);
        
        GameObject playerObject = other.gameObject;
        PhotonView photonView = playerObject.GetComponent<PhotonView>();
        if (photonView!= null && photonView.IsMine)
        {
            playerObject.GetComponent<CarController>().GetProp();
        }
        Destroy(gameObject);
    }
}
