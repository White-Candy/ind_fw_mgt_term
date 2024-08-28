using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LitJson;
using UnityEngine.Pool;
public class MajorEvent : BaseEvent
{
    public override async void GetInfoEvent(MessPackage mp)
    {
        MajorPanel panel = UIHelper.FindPanel<MajorPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void AddEvent(MessPackage mp)
    {
        MajorPanel panel = UIHelper.FindPanel<MajorPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void ReviseInfoEvent(MessPackage mp)
    {
        MajorPanel panel = UIHelper.FindPanel<MajorPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void DeleteInfoEvent(MessPackage mp)
    {
        MajorPanel panel = UIHelper.FindPanel<MajorPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}