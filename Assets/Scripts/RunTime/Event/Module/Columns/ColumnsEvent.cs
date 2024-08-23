using Cysharp.Threading.Tasks;

public class ColumnsEvent : BaseEvent
{
    public override async void GetInfoEvent(MessPackage mp)
    {
        ColumnsPanel panel = UITools.FindPanel<ColumnsPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void AddEvent(MessPackage mp)
    {
        ColumnsPanel panel = UITools.FindPanel<ColumnsPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void ReviseInfoEvent(MessPackage mp)
    {
        ColumnsPanel panel = UITools.FindPanel<ColumnsPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void DeleteInfoEvent(MessPackage mp)
    {
        ColumnsPanel panel = UITools.FindPanel<ColumnsPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}