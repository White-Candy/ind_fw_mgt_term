using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum PropertyType
{
    PT_None = 0,
    PT_ADD, // 添加
    PT_SET, // 修改
}

public class PropertyDialog : BasePanel
{
    [HideInInspector]
    public Button OK;

    [HideInInspector]
    public Button Cancel;

    [HideInInspector]
    public TMP_InputField m_userName;

    [HideInInspector]
    public TMP_InputField m_Pwd;

    [HideInInspector]
    public TMP_InputField m_Verify;

    [HideInInspector]
    public TMP_InputField m_NameIpt;

    [HideInInspector]
    public TMP_InputField m_Gender;

    [HideInInspector]
    public TMP_InputField m_Age;

    [HideInInspector]
    public TMP_InputField m_IdCard;

    [HideInInspector]
    public TMP_InputField m_Contact;

    [HideInInspector]
    public TMP_InputField m_HeadTeacher;

    [HideInInspector]
    public TMP_InputField m_ClassName;

    public static PropertyDialog instance;

    public PropertyType m_Type = PropertyType.PT_None; 

    private PD_BaseAction m_Action;

    private Dictionary<PropertyType, string> m_Enum2Type = new Dictionary<PropertyType, string>()
    {
        {PropertyType.PT_ADD, "AddAction"}, {PropertyType.PT_SET, "SetAction"}
    };

    public override void Awake()
    {
        base.Awake();

        instance = this;

        OK = GameObject.Find("OK").GetComponent<Button>();
        Cancel = GameObject.Find("Cancel").GetComponent<Button>();

        m_userName = GameObject.Find("userNameIpt").GetComponent<TMP_InputField>();
        m_Pwd = GameObject.Find("PwdIpt").GetComponent<TMP_InputField>();
        m_Verify = GameObject.Find("verifyIpt").GetComponent<TMP_InputField>();

        m_NameIpt = GameObject.Find("NameIpt").GetComponent<TMP_InputField>();
        m_Gender = GameObject.Find("GenderIpt").GetComponent<TMP_InputField>();
        m_Age = GameObject.Find("AgeIpt").GetComponent<TMP_InputField>();
        m_IdCard = GameObject.Find("IdCardIpt").GetComponent<TMP_InputField>();
        m_Contact = GameObject.Find("ContactIpt").GetComponent<TMP_InputField>();
        m_HeadTeacher = GameObject.Find("HTIpt").GetComponent<TMP_InputField>();
        m_ClassName = GameObject.Find("CNIpt").GetComponent<TMP_InputField>();

        Active(false);
    }

    public void Start()
    {
        OK?.OnClickAsObservable().Subscribe(x => 
        {        
            m_Action.Action(Output());

            Active(false);
            Clear();
        });

        Cancel?.OnClickAsObservable().Subscribe(x => 
        {
            Active(false);
            Clear();
        });
    }

    // 初始化界面
    public void Init(UserInfo inf, PropertyType t)
    {
        m_Action = Tools.CreateObject<PD_BaseAction>(m_Enum2Type[t]);
        m_Action.Init(inf);
    }

    // 清除InputField的内容
    public void Clear()
    {
        m_userName.text = "";
        m_Pwd.text = "";
        m_Verify.text = "";
        m_NameIpt.text = "";
        m_Gender.text = "";
        m_Age.text = "";
        m_IdCard.text = "";
        m_Contact.text = "";
        m_HeadTeacher.text = "";
        m_ClassName.text = "";
    }

    // UI装填
    public void Loading(UserInfo inf)
    {
        m_userName.text = inf.userName;
        m_Pwd.text = inf.password;
        m_Verify.text = inf.password;

        m_NameIpt.text = inf.Name;
        m_Gender.text = inf.Gender;
        m_Age.text = inf.Age;
        m_IdCard.text = inf.idCoder;
        m_Contact.text = inf.Contact;
        m_HeadTeacher.text = inf.HeadTeacher;
        m_ClassName.text = inf.className;
    }

    // 将目前界面上的内容信息存储到服务器中。
    public UserInfo Output()
    {
        UserInfo inf = new UserInfo
        {
            userName = m_userName.text,
            password = m_Pwd.text,

            Name = m_NameIpt.text,
            Gender = m_Gender.text,
            Age = m_Age.text,
            idCoder = m_IdCard.text,
            Contact = m_Contact.text,
            HeadTeacher = m_HeadTeacher.text,
            className = m_ClassName.text
        };

        return inf;
    }
}

public abstract class PD_BaseAction
{
    public virtual void Init(UserInfo inf) {}

    public virtual void Action(UserInfo inf) {}
}
