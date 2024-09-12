using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LitJson;

public class FacultyEvent : BaseEvent
{
    public override async void GetInfoEvent(MessPackage mp)
    {
        FacultyPanel panel = UIHelper.FindPanel<FacultyPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void AddEvent(MessPackage mp)
    {
        FacultyPanel panel = UIHelper.FindPanel<FacultyPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void ReviseInfoEvent(MessPackage mp)
    {
        FacultyPanel panel = UIHelper.FindPanel<FacultyPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void DeleteInfoEvent(MessPackage mp)
    {
        FacultyPanel panel = UIHelper.FindPanel<FacultyPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void SearchInfoEvent(MessPackage mp)
    {
        FacultyPanel panel = UIHelper.FindPanel<FacultyPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();    
    }
}