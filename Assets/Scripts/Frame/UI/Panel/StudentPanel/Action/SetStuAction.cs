using UnityEngine;

public class SetStuAction : PD_BaseAction
{
    public override void Init(params object[] info)
    {
        Debug.Log("Set Init!");

        UserInfo inf = info[0] as UserInfo;
        StuPropertyDialog.instance.Clear();

        StuPropertyDialog.instance.m_userName.enabled = false;
        StuPropertyDialog.instance.m_Pwd.enabled = true;
        StuPropertyDialog.instance.m_Verify.enabled = true;

        StuPropertyDialog.instance.m_NameIpt.enabled = true;
        StuPropertyDialog.instance.m_Gender.enabled = true;
        StuPropertyDialog.instance.m_Age.enabled = true;
        StuPropertyDialog.instance.m_IdCard.enabled = true;
        StuPropertyDialog.instance.m_Contact.enabled = true;
        StuPropertyDialog.instance.m_HeadTeacher.enabled = true;
        StuPropertyDialog.instance.m_ClassName.enabled = true;

        StuPropertyDialog.instance.Loading(inf);      
    }

    public override void Action(params object[] info)
    {
        base.Action(info);

        UserInfo inf = info[0] as UserInfo;

        TCPHelper.OperateInfo(inf, EventType.UserEvent, OperateType.REVISE);
    }
}