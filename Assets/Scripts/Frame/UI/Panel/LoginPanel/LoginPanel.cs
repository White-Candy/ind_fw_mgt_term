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
            TCPExp.LoginReq(m_Account?.text, m_Pwd?.text, 1);
        });
    }
}

/// <summary>
/// �û���Ϣ��
/// </summary>
[Serializable]
public class UserInfo
{
    public string userName;
    public string password;
    public int level;
    public bool login = false;
    public string hint = "";
}
