using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LitJson;
using UnityEngine;

public class GetEvent : BaseEvent
{
    public override async void OnEvent(MessPackage mp)
    {        
        JsonData jd = JsonMapper.ToObject(mp.ret);
        GlobalData.facultiesList = JsonMapper.ToObject<List<string>>(jd["facultiesList"].ToString());

        await UniTask.Yield();
    }
}