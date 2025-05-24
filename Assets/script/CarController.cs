using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CarController : MonoBehaviour
{
    private InputManager inputManager;//输入
    private UIManager uiManager;//UI
    private AudioSource audioSource;//音频
    
    public WheelCollider[] frontWheels;//前轮碰撞器
    public WheelCollider[] backWheels;//后轮碰撞器
    public GameObject[] wheelMesh = new GameObject[2];//轮子模型
    public static Rigidbody rb;
    
    //速度模块
    public float torque = 200;
    public float brakeTorqueMax = 100;
    public float steeringMax = 40;
    public bool speedUpStatus = false;//加速状态。

    //汽车状态
    private enum CarState
    {
        Idle,//静止 
        MovingForward,//前进 
        MovingBackward, //后退
        Braking, //刹车
        SpeedingUp//加速
    }
    private CarState currentState = CarState.Idle;
    
    //道具模块
    public struct Props
    {
        public string use;//用途

        public Props(string value)
        {
            use = value;
        }
    }

    //玩家数据
    private PlayerManager PM;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        audioSource = GetComponent<AudioSource>();
        rb=GetComponent<Rigidbody>();
        uiManager=GameObject.Find("GameUI").GetComponent<UIManager>();
        PM = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //音频播放
        if (inputManager.brake > 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        UpdateState();
    }

    //状态更新
    private void UpdateState()
    {
        if (inputManager.brake > 0)
        {
            currentState = CarState.Braking;
        }
        else if (speedUpStatus)
        {
            currentState = CarState.SpeedingUp;
        }
        else if (inputManager.vertical > 0)
        {
            currentState = CarState.MovingForward;
        }
        else if (inputManager.vertical < 0)
        {
            currentState = CarState.MovingBackward;
        }
        else
        {
            currentState = CarState.Idle;
        }
    }

    private void FixedUpdate()
    {
        animateWheels();
        if (inputManager.prop > 0 && !speedUpStatus&& PropManager.Instance.GetPropNum() > 0)
        {
            //加速状态
            speedUpStatus = true;
            PropManager.Instance.ConsumeProp();
            StartCoroutine(SpeedUpTimer());
        }
        if (speedUpStatus)
        {
            MoveVehicle(8);
        }
        else
        {
            MoveVehicle();
        }
    }

    private void MoveVehicle(int acceleration = 1)
    {
        // if (speedUpStatus && inputManager.brake>0)
        // {
        //     foreach (WheelCollider wheel in backWheels)
        //     {
        //         WheelFrictionCurve curve = wheel.sidewaysFriction; // 获取侧向摩擦曲线
        //         curve.extremumSlip = 0.01f; // 设置新的极值点打滑值
        //         wheel.sidewaysFriction = curve;
        //     }
        //     foreach (WheelCollider wheel in frontWheels)
        //     { 
        //         WheelFrictionCurve curve = wheel.sidewaysFriction; // 获取侧向摩擦曲线
        //         curve.extremumSlip = 0.01f; // 设置新的极值点打滑值
        //         wheel.sidewaysFriction = curve;
        //     }
        // }
        // else{
        //     foreach (WheelCollider wheel in backWheels)
        //     {
        //         WheelFrictionCurve curve = wheel.sidewaysFriction; // 获取侧向摩擦曲线
        //         curve.extremumSlip = 0.2f; // 设置新的极值点打滑值
        //         wheel.sidewaysFriction = curve;
        //     }
        //     foreach (WheelCollider wheel in frontWheels)
        //     { 
        //         WheelFrictionCurve curve = wheel.sidewaysFriction; // 获取侧向摩擦曲线
        //         curve.extremumSlip = 0.2f; // 设置新的极值点打滑值
        //         wheel.sidewaysFriction = curve; 
        //     }
        // }
        float brake = Mathf.Clamp(inputManager.brake,0,1)*brakeTorqueMax;
        foreach (WheelCollider wheel in backWheels)
        {
            if (brake == 0) wheel.motorTorque = inputManager.vertical * torque * acceleration;
            else wheel.motorTorque = 0;
            wheel.brakeTorque = brake;
        }
        
        foreach (WheelCollider wheel in frontWheels)
        { 
            wheel.steerAngle = inputManager.horizontal* steeringMax;
        }
    }

    private void animateWheels()
    {
        float horizontalInput = inputManager.horizontal;
        float steerAngle = horizontalInput * steeringMax;

        for (int i = 0; i < wheelMesh.Length; i++)
        {
            wheelMesh[i].transform.localRotation = Quaternion.Euler(0, 90+steerAngle, 0);       
        }
    }

    //获取时速
    public static float GetSpeed()
    {
        return rb.velocity.magnitude;
    }
    
    //控制加速时间
    private IEnumerator SpeedUpTimer()
    {
        yield return new WaitForSecondsRealtime(3f);
        speedUpStatus = false;
    }
    
    
 
}
