
using System.Collections.Generic;
using UnityEngine;

public class AddStuAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {
        StuPropertyDialog.instance.Clear();

        StuPropertyDialog.instance.m_userName.enabled = true;
        StuPropertyDialog.instance.m_Pwd.enabled = true;
        StuPropertyDialog.instance.m_Verify.enabled = true;

        StuPropertyDialog.instance.m_NameIpt.enabled = true;
        StuPropertyDialog.instance.m_Gender.enabled = true;
        StuPropertyDialog.instance.m_Age.enabled = true;
        StuPropertyDialog.instance.m_IdCard.enabled = true;
        StuPropertyDialog.instance.m_Contact.enabled = true;
        StuPropertyDialog.instance.m_HeadTeacher.enabled = true;
        StuPropertyDialog.instance.m_ClassName.enabled = true;
    }

    public override void Action(params object[] info)
    {
        base.Action(info);

        UserInfo inf = info[0] as UserInfo;
        List<UserInfo> single = new List<UserInfo>
        {
            inf
        };
  
        TCPHelper.AddUsersInfo(single);
    }
}