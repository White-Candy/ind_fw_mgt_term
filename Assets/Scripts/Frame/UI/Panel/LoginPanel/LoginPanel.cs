using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using TMPro;
using System;
using LitJson;

public class LoginPanel : BasePanel
{
    
    private Button m_Login;
    private TMP_InputField m_Account;
    private TMP_InputField m_Pwd;

    public static LoginPanel instance;

    public override void Awake()
    {
        base.Awake();
        instance = this;

        m_Login = GameObject.Find("Login").GetComponent<Button>();
        m_Account = GameObject.Find("inputAccount").GetComponent<TMP_InputField>();
        m_Pwd = GameObject.Find("inputPwd").GetComponent<TMP_InputField>();
    }

    public void Start()
    {
        m_Login.OnClickAsObservable().Subscribe((x) => 
        {
            TCPHelper.LoginReq(m_Account?.text, m_Pwd?.text, 1);
        });
    }
}

/// <summary>
/// 用户信息包
/// </summary>
[Serializable]
public class UserInfo : BaseInfo
{
    public string userName;
    public string Name;
    public string Gender;
    public string idCoder;
    public string Age;
    public string Contact;
    public string HeadTeacher;
    public string className;
    public string password;
    public bool login = false;
    public string hint = "";
}
