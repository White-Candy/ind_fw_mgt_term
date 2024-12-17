
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UserItem : MonoBehaviour
{
    public Button UserName;
    public Button Name;
    public Button Gender;
    public Button IDCoder;
    public Button Identity;
    public Button UnitName;
    public Button Contact;
    public Button status;
    public Button Revise;
    public Button Delete;
    public Toggle delToggle;

    private UserInfo m_inf = new UserInfo();
    private UserPropertyDialog m_UserProDialog;
    private List<string> m_UsrStatus = new List<string>() { "激活", "未激活" };

    public void Start()
    {
        m_UserProDialog = UIHelper.FindPanel<UserPropertyDialog>();
        
        // 信息修改
        Revise.OnClickAsObservable().Subscribe(x => 
        {
            if (GlobalData.s_currUsrLevel == 1) return;
            m_UserProDialog.Init(m_inf, PropertyType.PT_USER_SET);
            m_UserProDialog.Active(true);
        });

        // 信息删除
        Delete.OnClickAsObservable().Subscribe(x => 
        {
            if (GlobalData.s_currUsrLevel == 1) return;
            DialogHelper helper = new DialogHelper();
            MessageDialog dialog = helper.CreateMessDialog("MessageDialog");
            dialog.Show("用户信息删除", "是否删除用户信息？", new ItemPackage("确定", ConfirmDelete), new ItemPackage("取消", null));     
        });
    }

    // 初始化
    public void Init(UserInfo inf)
    {
        m_inf = inf;

        // Debug.Log($"UserName: {inf.userName}, and after convert unicode: {Tools.Unicode2String(inf.userName)}");
        UserName.GetComponentInChildren<TextMeshProUGUI>().text = Tools.Unicode2String(inf.userName);
        Name.GetComponentInChildren<TextMeshProUGUI>().text = Tools.Unicode2String(inf.Name);
        Gender.GetComponentInChildren<TextMeshProUGUI>().text = Tools.Unicode2String(inf.Gender);
        IDCoder.GetComponentInChildren<TextMeshProUGUI>().text = Tools.Unicode2String(inf.idCoder);
        Identity.GetComponentInChildren<TextMeshProUGUI>().text = Tools.Unicode2String(inf.Identity);
        status.GetComponentInChildren<TextMeshProUGUI>().text = inf.login  == true ? m_UsrStatus[0] : m_UsrStatus[1];
        UnitName.GetComponentInChildren<TextMeshProUGUI>().text = Tools.Unicode2String(inf.UnitName);
        Contact.GetComponentInChildren<TextMeshProUGUI>().text = Tools.Unicode2String(inf.Contact);
        delToggle.gameObject.SetActive(false);
        // ClassName.GetComponentInChildren<TextMeshProUGUI>().text = inf.className;
        gameObject.SetActive(true);    
    }

    /// <summary>
    /// 确认删除
    /// </summary>
    public void ConfirmDelete()
    {
        NetHelper.OperateInfo(m_inf, EventType.UserEvent, OperateType.DELETE);
    }
}  
