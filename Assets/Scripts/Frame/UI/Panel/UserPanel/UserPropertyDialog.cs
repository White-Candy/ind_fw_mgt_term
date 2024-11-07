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
    public TMP_Dropdown m_UnitName;

    // public static UserPropertyDialog instance;
    
    [HideInInspector]
    public PropertyType m_Type = PropertyType.PT_None; 

    private PD_BaseAction m_Action;

    public override void Awake()
    {
        base.Awake();

        // instance = this;

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
        m_UnitName = GameObject.Find("UNDrop").GetComponent<TMP_Dropdown>();
    }

    public void Start()
    {
        OK?.OnClickAsObservable().Subscribe(x => 
        {    
            if (!ValidateInputField()) return;
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

        Active(false);
    }

    /// <summary>
    /// 初始化界面
    /// </summary>
    /// <param name="inf"></param>
    /// <param name="t"></param>
    public void Init(UserInfo inf, PropertyType t)
    {
        // UIHelper.AddDropDownOptions(ref m_UnitName, GlobalData.classesList);

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
        m_UnitName.value = 0;
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
        m_UnitName.value = UIHelper.GetDropDownOptionIndex(m_UnitName, inf.UnitName);
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

        if (Tools.checkList(m_UnitName.options, m_UnitName.value)) { inf.UnitName = m_UnitName.options[m_UnitName.value].text; }     
        return inf;
    }


    /// <summary>
    /// 检查身份，不同的身份显示不同的UI控件。
    /// </summary>
    public void checkIdentity(int identityVal)
    {
        if (identityVal == 0)
            UIHelper.AddDropDownOptions(ref m_UnitName, GlobalData.classesList);
        else
            UIHelper.AddDropDownOptions(ref m_UnitName, GlobalData.facultiesList);
    }

    /// <summary>
    /// 验证信息是否有效
    /// </summary>
    /// <returns></returns>
    private bool ValidateInputField()
    {
        return  ValidataUserName() && ValidataPwd() && ValidataName() && ValidataAge() && ValidataIDCard() && ValidataContact();
    }
    
    /// <summary>
    /// 姓名验证
    /// </summary>
    /// <returns></returns>
    private bool ValidataUserName()
    {
        if (!UIHelper.InputFieldCheck(m_userName.text)) {  return false; }
        return true;
    }

    /// <summary>
    /// 密码验证
    /// </summary>
    /// <returns></returns>
    private bool ValidataPwd()
    {
        if (!(UIHelper.InputFieldCheck(m_Pwd.text) && UIHelper.InputFieldCheck(m_Verify.text))) {return false;}

        if (m_Pwd.text != m_Verify.text) 
        {
            UIHelper.ShowMessage("密码异常", "两次密码必须相等!", new ItemPackage("确定", () => {}));
            return false;
        }
        return true;
    }

    /// <summary>
    /// 年龄验证
    /// </summary>
    /// <returns></returns>
    private bool ValidataAge()
    {
        if (!UIHelper.InputFieldCheck(m_Age.text)) {  return false; }
        int i = 0;
        if (!int.TryParse(m_Age.text, out i))
        {
            UIHelper.ShowMessage("输入异常", "年龄格式输入错误!", new ItemPackage("确定", () => {}));
            return false;
        }

        if (i < 0)
        {
            UIHelper.ShowMessage("输入异常", "年龄不可为负数!", new ItemPackage("确定", () => {}));
            return false;
        }
        return true;
    }

    /// <summary>
    /// 姓名验证
    /// </summary>
    /// <returns></returns>
    private bool ValidataName()
    {
        if (!UIHelper.InputFieldCheck(m_NameIpt.text)) {  return false; }
        return true;
    }

    /// <summary>
    /// 身份证验证
    /// </summary>
    /// <returns></returns>
    private bool ValidataIDCard()
    {
        if (!ValidateHelper.IsIDCard(m_IdCard.text))
        {
            UIHelper.ShowMessage("身份证格式错误", "身份证格式错误!", new ItemPackage("确定", () => {}));
            return false;
        }
        return true;
    }

    private bool ValidataContact()
    {
        if (!UIHelper.InputFieldCheck(m_Contact.text)) return false;
        if (!ValidateHelper.IsMobile(m_Contact.text))
        {
            UIHelper.ShowMessage("联系方式异常", "联系方式格式错误!", new ItemPackage("确定", () => {}));
            return false;
        }
        return true; 
    }
}