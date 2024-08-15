
using System.Collections.Generic;
using UniRx;
using UnityEngine.UI;

public class FacultyPanel : BasePanel
{
    // 学院信息列表
    public List<FacultyInfo> m_facultiesInfo = new List<FacultyInfo>();
    
    public static FacultyPanel instance;

    public Button AddTo;
    public Button Refresh;

    public override void Awake()
    {
        base.Awake();

        instance = this;
        Active(false);
    }
    
    public void Start()
    {
        AddTo.OnClickAsObservable().Subscribe(_ => 
        {
            
        });
    }

    public void Init()
    {

    }

    public void Clear()
    {

    }

    /// <summary>
    /// 关闭
    /// </summary>
    public void Close()
    {
        Active(false);
        Clear();
    }
}

/// <summary>
///  学院信息包
/// </summary>
public class FacultyInfo
{
    public string Name;
    public string RegisterTime;
    public string TeacherName;
}