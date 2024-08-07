
using Cysharp.Threading.Tasks;
using LitJson;

public static class TCPExp
{
    /// <summary>
    /// µÇÂ¼ÇëÇó¡£
    /// </summary>
    /// <param name="account"></param>
    /// <param name="pwd"></param>
    /// <param name="level"></param>
    public static void LoginReq(string account, string pwd, int level)
    {
        UserInfo inf = new UserInfo();
        inf.userName = account;
        inf.password = pwd;
        inf.level = level;

        string sJson = JsonMapper.ToJson(inf);
        TCP.SendAsync(sJson, EventType.UserLoginEvent);
    }
}