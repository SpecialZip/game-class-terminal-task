using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Player; // 车辆
    //public GameObject Camera;//车辆下的摄像机
    public Vector3 followOffset = new Vector3(3.5f,1f,0f);
    public Vector3 cameraOffset = new Vector3(1f,0f,0f);//相机偏左矫正。
    public float speed = 1f; // 速度
    

    private void Awake()
    {
        Player=GameObject.FindGameObjectWithTag("Player");
        //Camera=Player.transform.Find("Camera").gameObject;
    }

    private void FixedUpdate()
    {
        follow();
    }

    private void follow()
    {
        Vector3 targetPosition = Player.transform.position + followOffset;
        gameObject.transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        gameObject.transform.LookAt(Player.gameObject.transform.position+ cameraOffset);
    }

}