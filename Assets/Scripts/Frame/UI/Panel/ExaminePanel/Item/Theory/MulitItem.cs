
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 多选窗口题目Item
/// </summary>
public class MulitItem : BaseExamineItem
{
    public TextMeshProUGUI SerialNum;
    public Button Delete;
    public TMP_InputField TopicContent;
    public TMP_InputField Score;
    public TMP_Dropdown ChoiceNum;
    public ChoiceItem ChoiceItem;
    public Transform ParentTrans;

    private Dictionary<string, ChoiceItem> choicesItem = new Dictionary<string, ChoiceItem>();
    private MulitChoice m_mulitChoice = new MulitChoice();

    public void Start()
    {
        ChoiceNum.onValueChanged.AddListener((val) => 
        {
            InitItem(int.Parse(ChoiceNum.options[ChoiceNum.value].text));
        });
    }

    /// <summary>
    /// 空白初始化
    /// </summary>
    /// <param name="idx"></param>
    public void Init(int idx)
    {
        SerialNum.text = idx.ToString();
        InitItem(int.Parse(ChoiceNum.options[ChoiceNum.value].text));
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 装填式 初始化
    /// </summary>
    /// <param name="choice"> choice info </param>
    /// <param name="quesSeq"> 多选题序列 </param>
    /// <param name="idx"> 序号 </param>
    public void Init(MulitChoice choice, int idx)
    {
        m_mulitChoice = choice;
        SerialNum.text = idx.ToString();
        Score.text = choice?.Score.ToString();
        TopicContent.text = choice?.Topic;
        LoadInitItem(m_mulitChoice.Options.Count);
    }

    /// <summary>
    /// 空白初始化 Item
    /// </summary>
    /// <param name="choicesNum"></param>
    public void InitItem(int choicesNum)
    {
        choicesClear();
        for (int i = 0; i < choicesNum; ++i)
        {
            ChoiceItem item = Instantiate(ChoiceItem, ParentTrans);
            string serial = ((char)(65 + i)).ToString();
            item.Init(serial + ".");
            item.gameObject.SetActive(true);
            choicesItem.Add(serial, item);
        }
    }

    /// <summary>
    /// 装填式初始化 Item
    /// </summary>
    /// <param name="choicesNum"></param>
    public void LoadInitItem(int choicesNum)
    {
        choicesClear();
        for (int i = 0; i < choicesNum; ++i)
        {
            ChoiceItem item = Instantiate(ChoiceItem, ParentTrans);
            string serial = ((char)(65 + i)).ToString();
            item.Init(serial + ".", m_mulitChoice.Options[serial].m_content, m_mulitChoice.Options[serial].m_isOn);
            item.gameObject.SetActive(true);
            choicesItem.Add(serial, item);
        }        
    }

    public MulitChoice Output()
    {
        MulitChoice singleChoice = new MulitChoice()
        {
            Topic = TopicContent.text,
            Score = int.Parse(Score.text)
        };

        string answer = "";
        foreach (var pair in choicesItem)
        {
            ChoiceItem item = pair.Value;
            ItemChoice itemChoice = new ItemChoice(item.m_Content.text, item.m_toggle.isOn);
            singleChoice.Options.Add(pair.Key, itemChoice);
            if (item.m_toggle.isOn)
            {
                answer += item.Serial;
            }
        }
  
        singleChoice.Answer = answer;
        return singleChoice;
    }

    public void Clear()
    {
        SerialNum.text = "";
        Score.text = "";
        TopicContent.text = "";
        foreach (var item in choicesItem.Values)
        {
            item.Clear();
        }
        choicesItem.Clear();
    }    

    public void choicesClear()
    {
        foreach (var item in choicesItem.Values)
        {
            item.Clear();
            item.gameObject.SetActive(false);
            Destroy(item);
        }
        choicesItem.Clear();
    }
}