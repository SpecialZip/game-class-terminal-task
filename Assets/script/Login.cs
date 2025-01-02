using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public static bool isLogin = false;
    public GameObject startUI;
    private void Awake()
    {
        startUI.SetActive(false);
        if (isLogin)
        {
            Destroy(gameObject);
            startUI.SetActive(true);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Canvas/Login/Text (TMP)").GetComponent<Button>().onClick.AddListener(onLogin);
        transform.Find("Canvas/Register/Text (TMP)").GetComponent<Button>().onClick.AddListener(onRegister);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void onLogin()
    {
        //获取输入的账密信息
        string account;
        string password;
        fetchInput(out account,out password);
        //检查数据库里是否存在
        if (MySqlConnect.CheckAccountInDatabase(account, password))
        {
            Debug.Log("登录成功");
            startUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void onRegister()
    {
        string account;
        string password;
        fetchInput(out account,out password);
    }
    
    private void fetchInput(out string account, out string password)
    {
        account=transform.Find("Canvas/Input/InputAccount/Text Area/Text").GetComponent<TextMeshProUGUI>().text;
        password=transform.Find("Canvas/Input/InputPassword/Text Area/Text").GetComponent<TextMeshProUGUI>().text;
    }
}
