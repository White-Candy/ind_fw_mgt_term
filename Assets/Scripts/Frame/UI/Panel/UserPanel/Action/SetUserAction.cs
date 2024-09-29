using System;
using UnityEngine;

public class SetUserAction : PD_BaseAction
{   
    private UserPropertyDialog m_UserProDialog;

    public override void Init(params object[] info)
    {
        //Debug.Log("Set Init!");

        UserInfo inf = info[0] as UserInfo;
        
        m_UserProDialog = UIHelper.FindPanel<UserPropertyDialog>();
        m_UserProDialog.Clear();

        m_UserProDialog.m_userName.enabled = false;
        m_UserProDialog.m_Pwd.enabled = true;
        m_UserProDialog.m_Verify.enabled = true;

        m_UserProDialog.m_NameIpt.enabled = true;
        m_UserProDialog.m_Gender.enabled = true;
        m_UserProDialog.m_Age.enabled = true;
        m_UserProDialog.m_Identity.enabled = true;
        m_UserProDialog.m_IdCard.enabled = true;
        m_UserProDialog.m_Contact.enabled = true;
        m_UserProDialog.m_UnitName.enabled = true;

        m_UserProDialog.Loading(inf);      
    }

    public override void Action(Action append, params object[] info)
    {
        base.Action(inf:info);

        UserInfo inf = info[0] as UserInfo;

        NetHelper.OperateInfo(inf, EventType.UserEvent, OperateType.REVISE);
    }
}