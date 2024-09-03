
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TheoryPanel : BasePanel
{
    public TMP_Dropdown Columns;
    public TMP_Dropdown Course;
    public TMP_InputField SingleNumber;
    public TMP_InputField MulitNumber;
    public TMP_InputField TOFNumber;
    public Button Single;
    public Button Muilt;
    public Button TOF;
    public SinglePanel m_SinglePanel;
    public MulitPanel m_MulitPanel;
    public TOFPanel m_TOFPanel;
    
    public static TheoryPanel inst;

    public override void Awake()
    {
        inst = this;
    }

    public void Start()
    {
        Single.OnClickAsObservable().Subscribe(_ =>
        {
            m_SinglePanel.Active(true);
            m_MulitPanel.Active(false);
            m_TOFPanel.Active(false);
        });

        Muilt.OnClickAsObservable().Subscribe(_ =>
        {
            m_SinglePanel.Active(false);
            m_MulitPanel.Active(true);
            m_TOFPanel.Active(false);
        });

        TOF.OnClickAsObservable().Subscribe(_ =>
        {
            m_SinglePanel.Active(false);
            m_MulitPanel.Active(false);
            m_TOFPanel.Active(true);
        });
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        Columns.value = 0;
        Course.value = 0;
        SingleNumber.text = "";
        MulitNumber.text = "";
        TOFNumber.text = "";
    }
}