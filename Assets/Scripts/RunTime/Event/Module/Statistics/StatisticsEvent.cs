using Cysharp.Threading.Tasks;
using UnityEngine;

public class StatisticsEvent : BaseEvent
{
    public override async void GetInfoEvent(MessPackage mp)
    {
        StatisticsPanel panel = UIHelper.FindPanel<StatisticsPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void AddEvent(MessPackage mp)
    {
        StatisticsPanel panel = UIHelper.FindPanel<StatisticsPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void ReviseInfoEvent(MessPackage mp)
    {
        StatisticsPanel panel = UIHelper.FindPanel<StatisticsPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void DeleteInfoEvent(MessPackage mp)
    {
        StatisticsPanel panel = UIHelper.FindPanel<StatisticsPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void SearchInfoEvent(MessPackage mp)
    {
        //Debug.Log($"User search event return some message: {mp.ret}");
        StatisticsPanel panel = UIHelper.FindPanel<StatisticsPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}