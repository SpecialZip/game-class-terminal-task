using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target; // 车辆的Transform
    public Vector3 offset=new Vector3(5,2.2f,0);   // 摄像机相对于车辆的偏移
    public float smoothSpeed = 0.125f; // 平滑速度

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        // 计算目标位置
        Vector3 desiredPosition = target.position + offset;
        // 平滑移动摄像机到目标位置
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        // 使摄像机始终看向目标
        transform.LookAt(target);
    }

}