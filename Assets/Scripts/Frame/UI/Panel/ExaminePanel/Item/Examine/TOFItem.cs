using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 判断题题目Item
/// </summary>
public class TOFItem : MonoBehaviour
{
    public Button Delete;
    public TextMeshProUGUI SerialNum;
    public TMP_InputField Score;
    public TMP_InputField TopicContent;
    public ChoiceItem toA;
    public ChoiceItem toB;

    private ExamineDialog m_examinePanel;

    public void Start()
    {
        Delete.onClick.AddListener(() => 
        {
            m_examinePanel = UIHelper.FindPanel<ExamineDialog>();
            ExamineInfo bufInfo = new ExamineInfo(m_examinePanel.m_info);

            int idx = -1;
            int.TryParse(SerialNum.text, out idx);
            if (idx != -1 && idx - 1 < bufInfo.TOFChoices.Count)
            {
                bufInfo.TOFChoices.RemoveAt(idx - 1);
                bufInfo.TOFNum--;
                m_examinePanel.Loading(bufInfo);
            }
        });
    }

    public void Init(TOFChoice choice, int idx)
    {
        SerialNum.text = idx.ToString();
        Score.text = choice?.Score.ToString();
        TopicContent.text = choice?.Topic;
        toA.m_Content.text = choice?.toA.m_content;
        toB.m_Content.text = choice?.toB.m_content;

        toA.m_toggle.isOn = choice.toA.m_isOn;
        toB.m_toggle.isOn = choice.toB.m_isOn;        
        gameObject.SetActive(true);
    }

    public TOFChoice Output()
    {
        TOFChoice tofChoice = new TOFChoice()
        {
            Topic = TopicContent.text,
            toA = new ItemChoice(toA.m_Content.text, toA.m_toggle.isOn), 
            toB = new ItemChoice(toB.m_Content.text, toB.m_toggle.isOn), 
            Score = int.Parse(Score.text)
        };

        string answer = "";
        answer = toA.m_toggle.isOn ? "A" : answer;
        answer = toB.m_toggle.isOn ? "B" : answer;
        tofChoice.Answer = answer;

        return tofChoice;
    }

    public void Clear()
    {
        SerialNum.text = "";
        Score.text = "";
        TopicContent.text = "";

        toA.Clear();
        toB.Clear();
    }
}