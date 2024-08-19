using Cysharp.Threading.Tasks;
using UnityEngine;

public class GetFacInfoEvent : BaseEvent
{
    public override async void OnEvent(params object[] args)
    {
        MessPackage mp = args[0] as MessPackage;
        // Debug.Log("GetFacInfoEvent.." + mp.ret);
        FacultyPanel panel = UITools.FindPanel<FacultyPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}