using UnityEngine;

public class SetAction : PD_BaseAction
{
    public override void Init(UserInfo inf)
    {
        Debug.Log("Set Init!");

        PropertyDialog.instance.Clear();

        PropertyDialog.instance.m_userName.enabled = false;
        PropertyDialog.instance.m_Pwd.enabled = true;
        PropertyDialog.instance.m_Verify.enabled = true;

        PropertyDialog.instance.m_NameIpt.enabled = true;
        PropertyDialog.instance.m_Gender.enabled = true;
        PropertyDialog.instance.m_Age.enabled = true;
        PropertyDialog.instance.m_IdCard.enabled = true;
        PropertyDialog.instance.m_Contact.enabled = true;
        PropertyDialog.instance.m_HeadTeacher.enabled = true;
        PropertyDialog.instance.m_ClassName.enabled = true;

        PropertyDialog.instance.Loading(inf);      
    }

    public override void Action(UserInfo inf)
    {
        base.Action(inf);

        // Debug.Log("Set Action!");
        TCPHelper.ReviseUserInfo(inf);
    }
}