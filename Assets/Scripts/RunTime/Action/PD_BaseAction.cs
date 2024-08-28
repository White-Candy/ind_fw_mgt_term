
using System;

public abstract class PD_BaseAction
{
    public virtual void Init(params object[] inf) {}

    public virtual void Action(Action append = default, params object[] inf) {}
}