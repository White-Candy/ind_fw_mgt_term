using Cysharp.Threading.Tasks;
using LitJson;
using UnityEngine;

public class UserLoginEvent : BaseEvent
{
    public override async void OnEvent(params object[] args)
    {
        MessPackage mp = args[0] as MessPackage;
        UserInfo inf = JsonMapper.ToObject<UserInfo>(mp.ret);
        Debug.Log(inf.hint);

        await UniTask.Yield();
    }
}