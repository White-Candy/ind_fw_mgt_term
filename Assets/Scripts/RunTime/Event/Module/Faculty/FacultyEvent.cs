using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LitJson;

public class CourseEvent : BaseEvent
{
    public override async void GetInfoEvent(MessPackage mp)
    {
        CoursePanel panel = UIHelper.FindPanel<CoursePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void AddEvent(MessPackage mp)
    {
        CoursePanel panel = UIHelper.FindPanel<CoursePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void ReviseInfoEvent(MessPackage mp)
    {
        CoursePanel panel = UIHelper.FindPanel<CoursePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void DeleteInfoEvent(MessPackage mp)
    {
        CoursePanel panel = UIHelper.FindPanel<CoursePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}