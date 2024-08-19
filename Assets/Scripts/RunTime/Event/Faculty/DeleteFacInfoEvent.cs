using Cysharp.Threading.Tasks;
using LitJson;
using UnityEngine;

public class DeleteFacInfoEvent : BaseEvent
{
    public override async void OnEvent(params object[] args)
    {
        MessPackage mp = args[0] as MessPackage;

        FacultyPanel panel = UITools.FindPanel<FacultyPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}