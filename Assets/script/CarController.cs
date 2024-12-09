using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider[] frontWheels;//前轮碰撞器
    public WheelCollider[] backWheels;//后轮碰撞器
    public GameObject[] wheelMesh = new GameObject[2];//轮子模型

    public float torque = 200;

    public float steeringMax = 40;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // float horizontal = Input.GetAxis("Horizontal");//左右
        // float vertical = Input.GetAxis("Vertical");
        // foreach (WheelCollider wheel in frontWheels)
        // {
        //     wheel.steerAngle = horizontal * 30;
        // }
        //
        // foreach (WheelCollider wheel in backWheels)
        // {
        //     wheel.motorTorque = vertical*torque;
        // }
    }


    private void FixedUpdate()
    {
        animateWheels();
        if (Input.GetAxis("Vertical")!=0)
        {
            foreach (WheelCollider wheel in backWheels)
            {
                wheel.motorTorque = Input.GetAxis("Vertical")*torque;
            }
        }
        else
        {
            foreach (WheelCollider wheel in backWheels)
            {
                wheel.motorTorque = 0;
            }
        }


        if (Input.GetAxis("Horizontal") != 0)
        {
            foreach (WheelCollider wheel in frontWheels)
            {
                wheel.steerAngle = Input.GetAxis("Horizontal") * steeringMax;
            }

            
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
            frontWheels[i].transform.localRotation = Quaternion.Euler(0, steerAngle, 0);
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
