using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ScoreItem : MonoBehaviour
{
    public GameObject userName;
    public GameObject Name;
    public GameObject ClassName;
    public GameObject CourseName;
    public GameObject RegisterTime;
    public GameObject Theory;
    public GameObject Training;
    public GameObject Total;

    public Button Revise;
    public Button Delete;

    private ReviseDialog reivseDialog;

    private ScoreInfo m_info = new ScoreInfo();

    public void Start()
    {
        reivseDialog = UIHelper.FindPanel<ReviseDialog>();
        Delete.OnClickAsObservable().Subscribe(_ => 
        {
            if (GlobalData.s_currUsrLevel == 0) return;
            DialogHelper helper = new DialogHelper();
            MessageDialog dialog = helper.CreateMessDialog("MessageDialog");
            dialog.Show("成绩信息的删除", "是否删除该成绩信息？", new ItemPackage("确定", ConfirmDelete), new ItemPackage("取消", null));    
        });

        Revise.OnClickAsObservable().Subscribe(_ => 
        {
            if (GlobalData.s_currUsrLevel == 0) return;

            if (GlobalData.s_currUsrLevel == 1)
            {
                reivseDialog.Init(m_info);
                reivseDialog.Active(true);
            }
        });
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="info"></param>
    public void Init(ScoreInfo info)
    {
        m_info = info.Clone();
        float theoryScore = 0.0f, trainScore = 0.0f;
        float.TryParse(info.theoryScore, out theoryScore);
        float.TryParse(info.trainingScore, out trainScore);

        theoryScore = Tools.DigitsRetained(theoryScore, 1);
        trainScore = Tools.DigitsRetained(trainScore, 1);

        userName.GetComponentInChildren<TextMeshProUGUI>().text = info.userName;
        Name.GetComponentInChildren<TextMeshProUGUI>().text = info.Name;
        ClassName.GetComponentInChildren<TextMeshProUGUI>().text = info.className;
        CourseName.GetComponentInChildren<TextMeshProUGUI>().text = info.courseName;
        RegisterTime.GetComponentInChildren<TextMeshProUGUI>().text = info.registerTime;
        Theory.GetComponentInChildren<TextMeshProUGUI>().text = info.theoryScore.Count() == 0 ? "0" : theoryScore.ToString();
        Training.GetComponentInChildren<TextMeshProUGUI>().text = info.trainingScore.Count() == 0 ? "0" : trainScore.ToString();
        Total.GetComponentInChildren<TextMeshProUGUI>().text = (theoryScore + trainScore).ToString();

        gameObject.SetActive(true);
    }    

    /// <summary>
    /// 确认删除
    /// </summary>
    public void ConfirmDelete()
    { 
        NetHelper.OperateInfo(m_info, EventType.ScoreEvent, OperateType.DELETE);
    }    
}