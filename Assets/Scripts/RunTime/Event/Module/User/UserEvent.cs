using Cysharp.Threading.Tasks;
using UnityEngine;

public class UserEvent : BaseEvent
{
    public override async void GetInfoEvent(MessPackage mp)
    {         
        UserPanel panel = UIHelper.FindPanel<UserPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void AddEvent(MessPackage mp)
    {
        UserPanel panel = UIHelper.FindPanel<UserPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void ReviseInfoEvent(MessPackage mp)
    {
        UserPanel panel = UIHelper.FindPanel<UserPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void DeleteInfoEvent(MessPackage mp)
    {
        UserPanel panel = UIHelper.FindPanel<UserPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();
    }

    public override async void SearchInfoEvent(MessPackage mp)
    {
        //Debug.Log($"User search event return some message: {mp.ret}");
        UserPanel panel = UIHelper.FindPanel<UserPanel>();
        panel.Show(mp.ret);

        await UniTask.Yield();    
    }
}