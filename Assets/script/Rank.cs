using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank : MonoBehaviour
{
    public List<PlayerData> playerDatas=new List<PlayerData>();
    public GameObject[] groups;
    private void Awake()
    {
        SortGrades();
        ShowRank();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SortGrades()
    {
        
    }

    public void ShowRank()
    {
        for (int i = 0; i < playerDatas.Count; i++)
        {
            //groups[i]
        }
    }
}
