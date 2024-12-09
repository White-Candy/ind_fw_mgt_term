
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  单选题题目Item
/// </summary>
public class SingleItem : MonoBehaviour
{
    public TextMeshProUGUI m_SerialNum;
    public Button Delete;
    public TMP_InputField m_Score;
    public TMP_InputField m_TopicContent;
    public ChoiceItem m_toA;
    public ChoiceItem m_toB;
    public ChoiceItem m_toC;
    public ChoiceItem m_toD;

    private ExamineDialog m_examinePanel;

    public void Start()
    {
        Delete.onClick.AddListener(() => 
        {
            m_examinePanel = UIHelper.FindPanel<ExamineDialog>();
            ExamineInfo bufInfo = m_examinePanel.m_info.Clone();
            int idx = -1;
            int.TryParse(m_SerialNum.text, out idx);
            if (idx != -1 && idx - 1 < bufInfo.SingleChoices.Count)
            {
                bufInfo.SingleChoices.RemoveAt(idx - 1);
                bufInfo.SingleNum--;
                m_examinePanel.Loading(bufInfo);
            }
        });
    }

    public void Init(SingleChoice choice, int idx)
    {
        m_SerialNum.text = idx.ToString();
        m_Score.text = choice?.Score.ToString();
        m_TopicContent.text = choice?.Topic;
        m_toA.m_Content.text = choice?.toA.m_content;
        m_toB.m_Content.text = choice?.toB.m_content;
        m_toC.m_Content.text = choice?.toC.m_content;
        m_toD.m_Content.text = choice?.toD.m_content;

        m_toA.m_toggle.isOn = choice.toA.m_isOn;
        m_toB.m_toggle.isOn = choice.toB.m_isOn;
        m_toC.m_toggle.isOn = choice.toC.m_isOn;
        m_toD.m_toggle.isOn = choice.toD.m_isOn;      
        gameObject.SetActive(true);
    }

    public bool InputFieldCheck()
    {
        if (!UIHelper.InputFieldCheck(m_TopicContent.text) || !ValidateHelper.IsNumberDecimal(m_Score.text)) return false;
        return true;
    }

    public SingleChoice Output()
    {
        SingleChoice singleChoice = new SingleChoice()
        {
            Topic = m_TopicContent.text,
            toA = new ItemChoice(m_toA.m_Content.text, m_toA.m_toggle.isOn), 
            toB = new ItemChoice(m_toB.m_Content.text, m_toB.m_toggle.isOn), 
            toC = new ItemChoice(m_toC.m_Content.text, m_toC.m_toggle.isOn), 
            toD = new ItemChoice(m_toD.m_Content.text, m_toD.m_toggle.isOn), 
            Score = m_Score.text
        };

        string answer = "";
        answer = m_toA.m_toggle.isOn ? "A" : answer;
        answer = m_toB.m_toggle.isOn ? "B" : answer;
        answer = m_toC.m_toggle.isOn ? "C" : answer;
        answer = m_toD.m_toggle.isOn ? "D" : answer;
        singleChoice.Answer = answer;

        return singleChoice;
    }

    public void Clear()
    {
        m_SerialNum.text = "";
        m_Score.text = "";
        m_TopicContent.text = "";

        m_toA.Clear();
        m_toB.Clear();
        m_toC.Clear();
        m_toD.Clear();
    }
}