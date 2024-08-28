
using Cysharp.Threading.Tasks;
using LitJson;

public class JsonHelper
{
    /// <summary>
    /// 异步object => string
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static async UniTask<string> AsyncToJson(object obj)
    {
        string str = "";
        await UniTask.RunOnThreadPool(() => 
        {
            str = JsonMapper.ToJson(obj);
        });
        return str;
    }
}