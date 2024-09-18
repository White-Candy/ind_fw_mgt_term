using Cysharp.Threading.Tasks;
using LitJson;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserLoginEvent : BaseEvent
{
    public override async void OnEvent(MessPackage mp)
    {
        UserInfo inf = JsonMapper.ToObject<UserInfo>(mp.ret);
        if (inf.hint == "登录成功")
        {
            GlobalData.s_CurrUsrInf = inf.Clone();
            GlobalData.s_currUsrLevel = GlobalData.s_UsrLevel[GlobalData.s_CurrUsrInf.Identity];
            var asyncManager = SceneManager.LoadSceneAsync("Main");
            asyncManager.allowSceneActivation = true;
        }
        else
        {
            UIHelper.ShowMessage("登录失败", inf.hint, new ItemPackage("确定", () => {}));
            return;
        }
        await UniTask.Yield();
    }
}