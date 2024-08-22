
using System.Collections.Generic;
using UnityEngine;

public class AddUserAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {
        UserPropertyDialog.instance.Clear();

        UserPropertyDialog.instance.m_userName.enabled = true;
        UserPropertyDialog.instance.m_Pwd.enabled = true;
        UserPropertyDialog.instance.m_Verify.enabled = true;

        UserPropertyDialog.instance.m_NameIpt.enabled = true;
        UserPropertyDialog.instance.m_Gender.enabled = true;
        UserPropertyDialog.instance.m_Age.enabled = true;
        UserPropertyDialog.instance.m_Identity.enabled = true;
        UserPropertyDialog.instance.m_IdCard.enabled = true;
        UserPropertyDialog.instance.m_Contact.enabled = true;
        UserPropertyDialog.instance.m_ClassName.enabled = true;
    }

    public override void Action(params object[] info)
    {
        base.Action(info);

        UserInfo inf = info[0] as UserInfo;
        List<UserInfo> single = new List<UserInfo>
        {
            inf
        };
    
        TCPHelper.OperateInfo(single, EventType.UserEvent, OperateType.ADD);
    }
}