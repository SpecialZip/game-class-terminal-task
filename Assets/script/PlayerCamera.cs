using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
/// <summary>
/// 多人联机时仅启用本地相机，禁用别的玩家的相机。
/// </summary>
public class PlayerCamera : MonoBehaviourPunCallbacks
{
    public Camera playerCamera;
    private AudioListener audioListener;
    // Start is called before the first frame update
    void Start()
    {
        
        audioListener = playerCamera.GetComponent<AudioListener>();
        if (photonView.IsMine)
        {
            playerCamera.enabled = true;
            audioListener.enabled = true;
        }
        else
        {
            playerCamera.enabled = false;
            audioListener.enabled = false;
        }

    }
}
