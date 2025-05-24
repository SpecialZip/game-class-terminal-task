using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public static bool isLogin = false;
    public GameObject hint;
    public GameObject startUI;
    public TMP_InputField accountInputField;
    public TMP_InputField passwordInputField;
    private void Awake()
    {
        isLogin = PlayerPrefs.GetInt("isLogin", 0) == 1;
        isLogin = false;
        if (isLogin)
        {
            gameObject.SetActive(false);
            startUI.SetActive(true);
        }
        else
        {
            gameObject.SetActive(true);
            startUI.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Canvas/Login/Text (TMP)").GetComponent<Button>().onClick.AddListener(onLogin);
        transform.Find("Canvas/Register/Text (TMP)").GetComponent<Button>().onClick.AddListener(onRegister);
        hint.SetActive(false);
    }
    
    public void onLogin()
    {
        //获取输入的账密信息
        string account;
        string password;
        fetchInput(out account,out password);
        //检查数据库里是否存在
        // if (MySqlConnect.CheckAccountInDatabase(account, password))
        // {
        //     Debug.Log("登录成功");
        //     startUI.SetActive(true);
        //     gameObject.SetActive(false);
        //     PlayerPrefs.SetInt("isLogin", 1);
        //     CreatePlayerDataObject(account);
        // }
        
        //测试代码，跳过数据库验证
        Debug.Log("登录成功");
        startUI.SetActive(true);
        gameObject.SetActive(false);
        PlayerPrefs.SetInt("isLogin", 1);
        CreatePlayerDataObject(account);
    }

    public void onRegister()
    {
        string account;
        string password;
        fetchInput(out account,out password);
        //检查数据库里是否存在
        if (MySqlConnect.CheckAccountExists(account))
        {
            popHint("账号已存在");
        }
        else
        {
            if (MySqlConnect.Register(account, password))
            {
                popHint("注册成功");
            }
            else
            {
                popHint("注册失败");
            }
        }
    }

    //创建一个挂载数据的对象
    public void CreatePlayerDataObject(string account)
    {
        // GameObject PlayerDataObject = new GameObject("PlayerDataObject");
        // PlayerData data=ScriptableObject.CreateInstance<PlayerData>();
        // data.name = account;
        // PlayerDataObject.AddComponent<PlayerDataMono>().playerData = data;
        // Instantiate(PlayerDataObject);
        // DontDestroyOnLoad(PlayerDataObject);
        PlayerDataManager.Instance.localPlayerData.name=account;
    }
    private void fetchInput(out string account, out string password)
    {
        //account=transform.Find("Canvas/Input/InputAccount/Text Area/Text").GetComponent<TextMeshProUGUI>().text;
        //password=transform.Find("Canvas/Input/InputPassword/Text Area/Text").GetComponent<InputField>().text;
        account=accountInputField.text;
        password=passwordInputField.text;
    }

    private void popHint(string msg)
    {
        hint.transform.Find("hint").GetComponent<TextMeshProUGUI>().text = msg;
        hint.SetActive(true);
        //等3秒后消失
        StartCoroutine(HideHintAfterDelay(3f));
    }
    
    private IEnumerator HideHintAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hint.SetActive(false);
    }
}
