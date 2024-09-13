using System;
using UnityEngine;

public class SetUserAction : PD_BaseAction
{
    public override void Init(params object[] info)
    {
        //Debug.Log("Set Init!");

        UserInfo inf = info[0] as UserInfo;
        UserPropertyDialog.instance.Clear();

        UserPropertyDialog.instance.m_userName.enabled = false;
        UserPropertyDialog.instance.m_Pwd.enabled = true;
        UserPropertyDialog.instance.m_Verify.enabled = true;

        UserPropertyDialog.instance.m_NameIpt.enabled = true;
        UserPropertyDialog.instance.m_Gender.enabled = true;
        UserPropertyDialog.instance.m_Age.enabled = true;
        UserPropertyDialog.instance.m_Identity.enabled = true;
        UserPropertyDialog.instance.m_IdCard.enabled = true;
        UserPropertyDialog.instance.m_Contact.enabled = true;
        UserPropertyDialog.instance.m_ClassName.enabled = true;

        UserPropertyDialog.instance.Loading(inf);      
    }

    public override void Action(Action append, params object[] info)
    {
        base.Action(inf:info);

        UserInfo inf = info[0] as UserInfo;

        TCPHelper.OperateInfo(inf, EventType.UserEvent, OperateType.REVISE);
    }
}