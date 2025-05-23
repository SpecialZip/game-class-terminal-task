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

    private bool isNetworkGame;

    private void Start()
    {
        isNetworkGame = PlayerPrefs.GetString("GameMode", "Local") == "Network";
    }

    private void FixedUpdate()
    {
        if (!isNetworkGame||(photonView.IsMine && PhotonNetwork.IsConnected))
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            brake = Input.GetAxis("Brake");
            prop = Input.GetAxis("Prop");
        }
    }
}
