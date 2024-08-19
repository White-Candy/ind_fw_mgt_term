using Cysharp.Threading.Tasks;
using UnityEngine;

public class ReviseFacInfoEvent : BaseEvent
{
    public override async void OnEvent(params object[] args)
    {
        MessPackage mp = args[0] as MessPackage;
        Debug.Log("ReviseFacInfoEvent.." + mp.ret);
        FacultyPanel panel = UITools.FindPanel<FacultyPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}