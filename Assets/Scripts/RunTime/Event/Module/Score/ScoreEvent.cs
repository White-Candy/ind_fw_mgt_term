
using Cysharp.Threading.Tasks;
using LitJson;
using UnityEngine.Pool;
public class ScoreEvent : BaseEvent
{
    public override async void GetInfoEvent(MessPackage mp)
    {
        ScorePanel panel = UIHelper.FindPanel<ScorePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void AddEvent(MessPackage mp)
    {
        ScorePanel panel = UIHelper.FindPanel<ScorePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void ReviseInfoEvent(MessPackage mp)
    {
        ScorePanel panel = UIHelper.FindPanel<ScorePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void DeleteInfoEvent(MessPackage mp)
    {
        ScorePanel panel = UIHelper.FindPanel<ScorePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void SearchInfoEvent(MessPackage mp)
    {
        ScorePanel panel = UIHelper.FindPanel<ScorePanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}