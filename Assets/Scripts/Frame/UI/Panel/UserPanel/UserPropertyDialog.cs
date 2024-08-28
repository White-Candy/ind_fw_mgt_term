using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UserPropertyDialog : BasePanel
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
    public TMP_Dropdown m_Gender;

    [HideInInspector]
    public TMP_InputField m_Age;

    [HideInInspector]
    public TMP_Dropdown m_Identity;

    [HideInInspector]
    public TMP_InputField m_IdCard;

    [HideInInspector]
    public TMP_InputField m_Contact;

    [HideInInspector]
    public TMP_Dropdown m_ClassName;

    [HideInInspector]
    public GameObject m_StudentLayout; // 专属于学生的控件

    public static UserPropertyDialog instance;
    
    [HideInInspector]
    public PropertyType m_Type = PropertyType.PT_None; 

    private PD_BaseAction m_Action;

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
        m_Gender = GameObject.Find("GenderDrop").GetComponent<TMP_Dropdown>();
        m_Age = GameObject.Find("AgeIpt").GetComponent<TMP_InputField>();
        m_Identity = GameObject.Find("IdentityrDrop").GetComponent<TMP_Dropdown>();
        m_IdCard = GameObject.Find("IdCardIpt").GetComponent<TMP_InputField>();
        m_Contact = GameObject.Find("ContactIpt").GetComponent<TMP_InputField>();
        m_ClassName = GameObject.Find("ClassDrop").GetComponent<TMP_Dropdown>();

        m_StudentLayout = GameObject.Find("StudentLayout").gameObject;

        m_StudentLayout.SetActive(false);
        Active(false);
    }

    public void Start()
    {
        OK?.OnClickAsObservable().Subscribe(x => 
        {        
            m_Action.Action(inf:Output());

            Active(false);
            Clear();
        });

        Cancel?.OnClickAsObservable().Subscribe(x => 
        {
            Active(false);
            Clear();
        });

        m_Identity?.onValueChanged.AddListener((idx) => 
        {
            checkIdentity(idx);
        });
    }

    /// <summary>
    /// 初始化界面
    /// </summary>
    /// <param name="inf"></param>
    /// <param name="t"></param>
    public void Init(UserInfo inf, PropertyType t)
    {
        UIHelper.AddDropDownOptions(m_ClassName, GlobalData.classesList);

        m_Action = Tools.CreateObject<PD_BaseAction>(GlobalData.m_Enum2Type[t]);
        m_Action.Init(inf);

        checkIdentity(m_Identity.value);
    }

    /// <summary>
    /// 清除InputField的内容
    /// </summary>
    public void Clear()
    {
        m_userName.text = "";
        m_Pwd.text = "";
        m_Verify.text = "";
        m_NameIpt.text = "";
        m_Gender.value = 0;
        m_Age.text = "";
        m_Identity.value = 0;
        m_IdCard.text = "";
        m_Contact.text = "";
        m_ClassName.value = 0;
    }

    /// <summary>
    /// UI装填
    /// </summary>
    /// <param name="inf"></param>
    public void Loading(UserInfo inf)
    {
        m_userName.text = inf.userName;
        m_Pwd.text = inf.password;
        m_Verify.text = inf.password;

        m_NameIpt.text = inf.Name;
        m_Gender.value = UIHelper.GetDropDownOptionIndex(m_Gender, inf.Gender);
        m_Age.text = inf.Age;
        m_IdCard.text = inf.idCoder;
        m_Identity.value = UIHelper.GetDropDownOptionIndex(m_Identity, inf.Identity);
        m_Contact.text = inf.Contact;
        m_ClassName.value = UIHelper.GetDropDownOptionIndex(m_ClassName, inf.className);
    }

    /// <summary>
    /// 将目前界面上的内容信息存储到服务器中。
    /// </summary>
    /// <returns></returns>
    public UserInfo Output()
    {
        UserInfo inf = new UserInfo
        {
            userName = m_userName.text,
            password = m_Pwd.text,

            Name = m_NameIpt.text,
            Gender = m_Gender.options[m_Gender.value].text,
            Identity = m_Identity.options[m_Identity.value].text,
            Age = m_Age.text,
            idCoder = m_IdCard.text,
            Contact = m_Contact.text,
        };

        if (Tools.checkList(m_ClassName.options, m_ClassName.value))
        {
            inf.className = m_Identity.value == 0 ? 
                m_ClassName.options[m_ClassName.value].text : "";
        }
            
        
        return inf;
    }


    /// <summary>
    /// 检查身份，不同的身份显示不同的UI控件。
    /// </summary>
    public void checkIdentity(int identityVal)
    {
        if (identityVal == 0)
        {
            m_StudentLayout.gameObject.SetActive(true);
        }
        else
        {
            m_StudentLayout.gameObject.SetActive(false);
        }
    }
}