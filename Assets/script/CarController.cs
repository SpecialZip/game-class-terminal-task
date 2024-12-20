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
    public Rigidbody rb;
    public TextMeshProUGUI speedText;       //车速
    //速度模块
    public float torque = 200;
    public float brakeTorqueMax = 500;
    public float steeringMax = 40;
    public bool speedUpStatus = false;//加速状态。
    //道具模块
    public struct Props
    {
        public string use;//用途

        public Props(string value)
        {
            use = value;
        }
    }
    List<Props> propSlot = new List<Props>();

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        uiManager=GameObject.Find("EventSystem").GetComponent<UIManager>();
        audioSource = GetComponent<AudioSource>();
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //UI时速更新
        float speed=rb.velocity.magnitude;
        speedText.text = Mathf.FloorToInt(speed).ToString();
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
    }


    private void FixedUpdate()
    {
        animateWheels();
        if (inputManager.prop > 0 && !speedUpStatus)
        {
            ConsumeProp();
        }
        if (speedUpStatus)
        {
            MoveVehicle(10);
        }
        else
        {
            MoveVehicle();
        }
    }

    private void MoveVehicle(int acceleration = 1)
    {
        float brake = Mathf.Clamp(inputManager.brake,0,1)*brakeTorqueMax;
        foreach (WheelCollider wheel in backWheels)
        { 
            wheel.motorTorque = inputManager.vertical*torque*acceleration;
            wheel.brakeTorque = brake;
        }
        
        foreach (WheelCollider wheel in frontWheels)
        { 
            wheel.steerAngle = inputManager.horizontal* steeringMax;
        }
    }

    private void animateWheels()
    {
        Vector3 wheelPositions = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float steerAngle = horizontalInput * steeringMax;

        for (int i = 0; i < frontWheels.Length; i++)
        {
            //frontWheels[i].transform.localRotation = Quaternion.Euler(0, steerAngle, 0);
        }
        // leftFrontWheel.localRotation = Quaternion.Slerp(leftFrontWheel.localRotation, Quaternion.Euler(0, leftWheelAngle, 0), Time.deltaTime * steerSpeed);
        // rightFrontWheel.localRotation = Quaternion.Slerp(rightFrontWheel.localRotation, Quaternion.Euler(0, rightWheelAngle, 0), Time.deltaTime * steerSpeed);

        // for(int i=0; i<frontWheels.Length; i++)
        // {
        //     frontWheels[i].GetWorldPose(out wheelPositions, out wheelRotation);
        //     wheelMesh[i].transform.position = wheelPositions;
        //     wheelMesh[i].transform.rotation = wheelRotation;
        // }
    }

    
    //控制加速时间
    private IEnumerator SpeedUpTimer()
    {
        yield return new WaitForSecondsRealtime(5f);
        speedUpStatus = false;
    }
    
    
    //获得道具
    public void GetProp()
    {
        if (propSlot.Count < 2)
        {
            propSlot.Add(new Props("SpeedUp"));
            Debug.Log("获得");
            uiManager.UpdateProps(propSlot);
        }
    }
    
    //消耗道具
    public void ConsumeProp()
    {
        if (propSlot.Count > 0)
        {
            propSlot.RemoveAt(0);
            Debug.Log("消耗");
            uiManager.UpdateProps(propSlot);
            StartCoroutine(SpeedUpTimer());
        }
    }
}
