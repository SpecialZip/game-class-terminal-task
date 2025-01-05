using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCanvas : MonoBehaviourPunCallbacks
{
    public Canvas playerCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            playerCanvas.enabled = true;
        }
        else
        {
            playerCanvas.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
