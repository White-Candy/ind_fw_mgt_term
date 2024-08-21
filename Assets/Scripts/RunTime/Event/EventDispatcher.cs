using System;
using System.Collections.Generic;

public class DispatcherEvent
{
    private Dictionary<string, Action<BaseEvent, MessPackage>> MethodDic = new Dictionary<string, Action<BaseEvent, MessPackage>>()
    {
        {"GET", (_event, pkg) => _event.GetInfoEvent(pkg) },
        {"ADD", (_event, pkg) => _event.AddEvent(pkg) },
        {"REVISE", (_event, pkg) => _event.ReviseInfoEvent(pkg) },
        {"DELETE", (_event, pkg) => _event.DeleteInfoEvent(pkg) },
        {"NONE", (_event, pkg) => _event.OnEvent(pkg) },
    };

    public void Dispatcher(params object[] objs)
    {
        MessPackage pkg = objs[0] as MessPackage;
        string operateType = pkg.operate_type;
        string eventType = pkg.event_type;
        BaseEvent _event = Tools.CreateObject<BaseEvent>(eventType);

        Action<BaseEvent, MessPackage> action;
        MethodDic.TryGetValue(operateType, out action);
        action(_event, pkg);
    }
}