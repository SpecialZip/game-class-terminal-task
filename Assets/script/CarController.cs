using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CarController : MonoBehaviour
{
    private InputManager inputManager;
    public WheelCollider[] frontWheels;//前轮碰撞器
    public WheelCollider[] backWheels;//后轮碰撞器
    public GameObject[] wheelMesh = new GameObject[2];//轮子模型
    
    public Rigidbody rb;
    public TextMeshProUGUI speedText;       //车速
    public float torque = 200;
    public float brakeTorqueMax = 500;
    public float steeringMax = 40;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed=rb.velocity.magnitude;
        speedText.text = Mathf.FloorToInt(speed).ToString();
    }


    private void FixedUpdate()
    {
        animateWheels();
        moveVehicle();
    }

    private void moveVehicle()
    {
        float brake = Mathf.Clamp(inputManager.brake,0,1)*brakeTorqueMax;
        foreach (WheelCollider wheel in backWheels)
        { 
            wheel.motorTorque = inputManager.vertical*torque;
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
}
