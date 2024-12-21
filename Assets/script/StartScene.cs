using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void toStart(){
        SceneManager.LoadScene("Start");    
    }
    
    public void toIntroduction(){
        SceneManager.LoadScene("Introduction");
    }
    
    public void toRank()
    {
        SceneManager.LoadScene("Rank");
    }
}
