
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  单选题题目Item
/// </summary>
public class SingleItem : BaseExamineItem
{
    public TextMeshProUGUI m_SerialNum;
    public Button Delete;
    public TMP_InputField m_Score;
    public TMP_InputField m_TopicContent;
    public ChoiceItem m_toA;
    public ChoiceItem m_toB;
    public ChoiceItem m_toC;
    public ChoiceItem m_toD;

    public void Init(SingleChoice choice, int idx)
    {
        m_SerialNum.text = idx.ToString();
        m_Score.text = choice?.Score.ToString();
        m_TopicContent.text = choice?.Topic;
        m_toA.m_Content.text = choice?.toA;
        m_toB.m_Content.text = choice?.toB;
        m_toC.m_Content.text = choice?.toC;
        m_toD.m_Content.text = choice?.toD;
    }

    public SingleChoice Output()
    {
        SingleChoice singleChoice = new SingleChoice()
        {
            Topic = m_TopicContent.text,
            toA = m_toA.m_Content.text,
            toB = m_toB.m_Content.text,
            toC = m_toC.m_Content.text,
            toD = m_toD.m_Content.text,
            Score = int.Parse(m_Score.text)
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
;    }
}