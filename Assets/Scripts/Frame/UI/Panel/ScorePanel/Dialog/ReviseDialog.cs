

using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ReviseDialog : BasePanel
{
    public Button OK;
    public Button Cancel;
    public TMP_InputField userName;
    public TMP_InputField Name;
    public TMP_InputField theoryScore;
    public TMP_InputField trainingScore;
    public TMP_InputField totalScore;

    private ScoreInfo m_inf = new ScoreInfo();

    public void Start()
    {
        OK.onClick.AddListener(() => 
        {
            if (!InputFieldCheck()) return;
            m_inf.theoryScore = theoryScore.text;
            m_inf.trainingScore = trainingScore.text;
            TCPHelper.OperateInfo(m_inf, EventType.ScoreEvent, OperateType.REVISE);
            Close();
        });

        Cancel.onClick.AddListener(() => { Close(); });

        theoryScore.onValueChanged.AddListener((str) => 
        {
            if (Tools.isDigit(theoryScore.text) && Tools.isDigit(trainingScore.text)) 
            {
                totalScore.text = (float.Parse(theoryScore.text) + float.Parse(trainingScore.text)).ToString(); 
            }  
            else if (!Tools.isDigit(theoryScore.text)) theoryScore.text = "0";
        });

        trainingScore.onValueChanged.AddListener((str) => 
        {
            if (Tools.isDigit(theoryScore.text) && Tools.isDigit(trainingScore.text)) 
            {
                totalScore.text = (float.Parse(theoryScore.text) + float.Parse(trainingScore.text)).ToString(); 
            }
            else if (!Tools.isDigit(trainingScore.text)) trainingScore.text = "0";
        });

        totalScore.enabled = false;
        userName.enabled = false;
        Name.enabled = false;

        Close();
    }

    public bool InputFieldCheck()
    {
        if (!UIHelper.InputFieldCheck(userName.text) || !UIHelper.InputFieldCheck(Name.text) 
            || !ValidateHelper.IsNumber(theoryScore.text)  || !UIHelper.InputFieldCheck(trainingScore.text)) return false;
        return true;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="inf"></param>
    public void Init(ScoreInfo inf)
    {
        m_inf = inf.Clone();
        userName.text = inf.userName;
        Name.text = inf.Name;
        theoryScore.text = inf.theoryScore;
        trainingScore.text = inf.trainingScore;
        totalScore.text = (float.Parse(inf.theoryScore) + float.Parse(inf.trainingScore)).ToString();
    }

    /// <summary>
    /// 清空
    /// </summary>
    public override void Close()
    {
        userName.text = "";
        Name.text = "";
        theoryScore.text = "";
        trainingScore.text = "";
        totalScore.text = "";
        Active(false);
    }
}