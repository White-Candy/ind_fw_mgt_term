using Cysharp.Threading.Tasks;
using UnityEngine;

public class AddFacInfoEvent : BaseEvent
{
    public override async void OnEvent(params object[] args)
    {
        MessPackage mp = args[0] as MessPackage;

        Debug.Log("AddFacInfoEvent..");
        FacultyPanel panel = UITools.FindPanel<FacultyPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}