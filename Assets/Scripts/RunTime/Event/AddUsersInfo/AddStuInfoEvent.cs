using UnityEngine;

public class AddStuInfoEvent : BaseEvent
{
    public override async void OnEvent(params object[] args)
    {
        MessPackage mp = args[0] as MessPackage;

        Debug.Log("AddStuInfoEvent..");
        StudentPanel panel = UITools.FindPanel<StudentPanel>();
        panel.Show(mp.ret);
    }
}