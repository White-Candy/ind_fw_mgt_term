using Cysharp.Threading.Tasks;

public class UserEvent : BaseEvent
{
    public override async void GetInfoEvent(MessPackage mp)
    {         
        StudentPanel panel = UITools.FindPanel<StudentPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void AddEvent(MessPackage mp)
    {
        StudentPanel panel = UITools.FindPanel<StudentPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void ReviseInfoEvent(MessPackage mp)
    {
        StudentPanel panel = UITools.FindPanel<StudentPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void DeleteInfoEvent(MessPackage mp)
    {
        StudentPanel panel = UITools.FindPanel<StudentPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}