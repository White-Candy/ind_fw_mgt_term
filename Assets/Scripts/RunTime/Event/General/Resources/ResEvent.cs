using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LitJson;

public class ResEvent : BaseEvent
{
    public override async void GetInfoEvent(MessPackage mp)
    {
        ResDeletePanel panel = UIHelper.FindPanel<ResDeletePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void AddEvent(MessPackage mp)
    {
        ResDeletePanel panel = UIHelper.FindPanel<ResDeletePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void ReviseInfoEvent(MessPackage mp)
    {
        ResDeletePanel panel = UIHelper.FindPanel<ResDeletePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void DeleteInfoEvent(MessPackage mp)
    {
        ResDeletePanel panel = UIHelper.FindPanel<ResDeletePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void SearchInfoEvent(MessPackage mp)
    {
        ResDeletePanel panel = UIHelper.FindPanel<ResDeletePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();   
    }
}