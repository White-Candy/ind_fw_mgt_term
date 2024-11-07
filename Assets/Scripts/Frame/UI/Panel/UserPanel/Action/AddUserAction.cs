
using System;
using System.Collections.Generic;
using UnityEngine;

public class AddUserAction : PD_BaseAction
{
    private UserPropertyDialog m_UserProDialog;
    public override void Init(params object[] inf)
    {
        m_UserProDialog = UIHelper.FindPanel<UserPropertyDialog>();
        m_UserProDialog.Clear();

        m_UserProDialog.m_userName.enabled = true;
        m_UserProDialog.m_Pwd.enabled = true;
        m_UserProDialog.m_Verify.enabled = true;

        m_UserProDialog.m_NameIpt.enabled = true;
        m_UserProDialog.m_Gender.enabled = true;
        m_UserProDialog.m_Age.enabled = true;
        m_UserProDialog.m_Identity.enabled = true;
        m_UserProDialog.m_IdCard.enabled = true;
        m_UserProDialog.m_Contact.enabled = true;
        m_UserProDialog.m_UnitName.enabled = true;
    }

    public override void Action(Action append = default, params object[] info)
    {
        base.Action(inf:info);

        UserInfo inf = info[0] as UserInfo;
        List<UserInfo> single = new List<UserInfo>
        {
            inf
        };
    
        NetHelper.OperateInfo(single, EventType.UserEvent, OperateType.ADD);
    }
}