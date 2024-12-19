using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Vector3 position =gameObject.transform.position;
        Quaternion rotation = other.gameObject.transform.rotation;
        //Debug.Log(position);Debug.Log(rotation);
        GameObject.Find("Props").GetComponent<PropScriptRespawn>().Respawn(position, rotation);
        GameObject.Find("Kart").GetComponent<CarController>().GetProp();
        Destroy(gameObject);
    }
}
