using Cysharp.Threading.Tasks;
using UnityEngine;

public class GetMajorInfoEvent : BaseEvent
{
    public override async void OnEvent(params object[] args)
    {
        MessPackage mp = args[0] as MessPackage;
        MajorPanel panel = UITools.FindPanel<MajorPanel>();
        Debug.Log("GetMajorInfoEvent : " + mp.ret);
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}