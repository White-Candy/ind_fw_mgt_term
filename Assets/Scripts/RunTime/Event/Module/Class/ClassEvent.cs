using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LitJson;

public class ClassEvent : BaseEvent
{
    public override async void GetInfoEvent(MessPackage mp)
    {
        ClassPanel panel = UITools.FindPanel<ClassPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void AddEvent(MessPackage mp)
    {
        ClassPanel panel = UITools.FindPanel<ClassPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void ReviseInfoEvent(MessPackage mp)
    {
        ClassPanel panel = UITools.FindPanel<ClassPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void DeleteInfoEvent(MessPackage mp)
    {
        ClassPanel panel = UITools.FindPanel<ClassPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}