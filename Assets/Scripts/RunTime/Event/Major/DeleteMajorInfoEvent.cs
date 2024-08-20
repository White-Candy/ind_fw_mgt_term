
using Cysharp.Threading.Tasks;

public class DeleteMajorInfoEvent : BaseEvent
{
    public override async void OnEvent(params object[] args)
    {
        MessPackage mp = args[0] as MessPackage;
        MajorPanel panel = UITools.FindPanel<MajorPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }
}