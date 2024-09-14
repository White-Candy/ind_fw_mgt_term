using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LitJson;

public class ExamineEvent : BaseEvent
{
    public override async void GetInfoEvent(MessPackage mp)
    {
        ExaminePanel panel = UIHelper.FindPanel<ExaminePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void AddEvent(MessPackage mp)
    {
        ExaminePanel panel = UIHelper.FindPanel<ExaminePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void ReviseInfoEvent(MessPackage mp)
    {
        ExaminePanel panel = UIHelper.FindPanel<ExaminePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void DeleteInfoEvent(MessPackage mp)
    {
        ExaminePanel panel = UIHelper.FindPanel<ExaminePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void SearchInfoEvent(MessPackage mp)
    {
        ExaminePanel panel = UIHelper.FindPanel<ExaminePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();    
    }
}