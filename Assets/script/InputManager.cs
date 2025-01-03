using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InputManager : MonoBehaviourPun
{
    public float horizontal;
    public float vertical;
    public float brake;
    public float prop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine && PhotonNetwork.IsConnected)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            brake = Input.GetAxis("Brake");
            prop = Input.GetAxis("Prop");
        }
    }
}
