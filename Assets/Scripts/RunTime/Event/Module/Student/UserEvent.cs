using Cysharp.Threading.Tasks;

public class UserEvent : BaseEvent
{
    public override async void GetInfoEvent(MessPackage mp)
    {         
        UserPanel panel = UITools.FindPanel<UserPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void AddEvent(MessPackage mp)
    {
        UserPanel panel = UITools.FindPanel<UserPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void ReviseInfoEvent(MessPackage mp)
    {
        UserPanel panel = UITools.FindPanel<UserPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void DeleteInfoEvent(MessPackage mp)
    {
        UserPanel panel = UITools.FindPanel<UserPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}