using Cysharp.Threading.Tasks;
using LitJson;
using UnityEngine;

public class ReviseStuInfoEvent : BaseEvent
{
    public override async void OnEvent(params object[] args)
    {
        MessPackage mp = args[0] as MessPackage;

        StudentPanel panel = UITools.FindPanel<StudentPanel>();
        panel.Show(mp.ret);
    }
}