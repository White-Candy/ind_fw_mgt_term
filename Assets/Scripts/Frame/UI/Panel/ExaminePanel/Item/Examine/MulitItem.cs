
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 多选窗口题目Item
/// </summary>
public class MulitItem : MonoBehaviour
{
    public TextMeshProUGUI SerialNum;
    public Button Delete;
    public TMP_InputField TopicContent;
    public TMP_InputField Score;
    public TMP_Dropdown ChoiceNum;
    public ChoiceItem ChoiceItem;
    public Transform ParentTrans;

    private List<ChoiceItem> choicesItem = new List<ChoiceItem>();
    private List<MulitChoiceItem> choicesItemInfo = new List<MulitChoiceItem>();
    private MulitChoice m_mulitChoice = new MulitChoice();

    private ExamineDialog m_examinePanel;

    public void Start()
    {
        ChoiceNum.onValueChanged.AddListener((val) => 
        {
            InitItem(int.Parse(ChoiceNum.options[ChoiceNum.value].text));
        });

        Delete.onClick.AddListener(() => 
        {
            m_examinePanel = UIHelper.FindPanel<ExamineDialog>();
            ExamineInfo bufInfo = m_examinePanel.m_info.Clone();
            int idx = -1;
            int.TryParse(SerialNum.text, out idx);
            if (idx != -1 && idx - 1 < bufInfo.MulitChoices.Count)
            {
                bufInfo.MulitChoices.RemoveAt(idx - 1);
                bufInfo.MulitNum--;
                m_examinePanel.Loading(bufInfo);
            }
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
        gameObject.SetActive(true);
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

            choicesItem.Add(item);
            choicesItemInfo.Add(new MulitChoiceItem(serial, item.m_Content.text, item.m_toggle.isOn));
        }
    }

    /// <summary>
    /// 装填式初始化 Item
    /// </summary>
    /// <param name="choicesNum"></param>
    public void LoadInitItem(int choicesNum)
    {
        choicesClear();
        UIHelper.SetDropDown(ref ChoiceNum, choicesNum.ToString());
        for (int i = 0; i < choicesNum; ++i)
        {
            string serial = m_mulitChoice.Options[i].Serial;
            string content = m_mulitChoice.Options[i].Content;
            bool isOn = m_mulitChoice.Options[i].isOn;

            ChoiceItem item = Instantiate(ChoiceItem, ParentTrans);
            item.Init(serial, content, isOn);
            item.gameObject.SetActive(true);

            choicesItem.Add(item);
            choicesItemInfo.Add(new MulitChoiceItem(serial, content, isOn));
        }        
    }

    public MulitChoice Output()
    {
        MulitChoice mulitChoice = new MulitChoice()
        {
            Topic = TopicContent.text,
            Score = Score.text
        };

        string answer = "";
        foreach (var item in choicesItem)
        {
            mulitChoice.Options.Add(new MulitChoiceItem(item.Serial.text, item.m_Content.text, item.m_toggle.isOn));
            if (item.m_toggle.isOn)
            {
                string[] split = item.Serial.text.Split(".");
                answer += split[0];
            }
        }
        mulitChoice.Answer = answer;
        return mulitChoice;
    }

    public void Clear()
    {
        SerialNum.text = "";
        Score.text = "";
        TopicContent.text = "";
        choicesItem.Clear();
        choicesItemInfo.Clear();
    }    

    public void choicesClear()
    {
        foreach (var item in choicesItem)
        {
            item.Clear();
            item.gameObject.SetActive(false);
            Destroy(item);
        }
        choicesItem.Clear();
        choicesItemInfo.Clear();
    }
}