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
    private Button m_ShowPwd;

    private bool m_ShowPassword = true;

    // public static LoginPanel instance;

    public override void Awake()
    {
        base.Awake();
        // instance = this;

        m_Login = GameObject.Find("Login").GetComponent<Button>();
        m_Account = GameObject.Find("inputAccount").GetComponent<TMP_InputField>();
        m_Pwd = GameObject.Find("inputPwd").GetComponent<TMP_InputField>();
        m_ShowPwd = GameObject.Find("showPwd").GetComponent<Button>();
    }

    public void Start()
    {
        m_Login.OnClickAsObservable().Subscribe((x) => 
        {
            NetHelper.LoginReq(m_Account?.text, m_Pwd?.text);
        });

        m_ShowPwd.onClick.AddListener(() => 
        {
            if (m_ShowPassword)
            {
                m_Pwd.contentType = TMP_InputField.ContentType.Standard;
                m_ShowPassword = false;
            }
            else
            {
                m_Pwd.contentType = TMP_InputField.ContentType.Password;
                m_ShowPassword = true;
            }

            // refresh
            string pwd = m_Pwd.text;
            m_Pwd.text = "";
            m_Pwd.text = pwd;
        });
    }
}