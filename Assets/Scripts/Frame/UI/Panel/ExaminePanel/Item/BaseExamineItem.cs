using UnityEngine;
using UnityEngine.PlayerLoop;

public class BaseExamineItem : MonoBehaviour
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="inf"></param>
    public virtual void Init(BaseChoice inf) { }

    /// <summary>
    /// 打包输出
    /// </summary>
    /// <returns></returns>
    // public virtual BaseChoice Output() { return new BaseChoice(); }
}