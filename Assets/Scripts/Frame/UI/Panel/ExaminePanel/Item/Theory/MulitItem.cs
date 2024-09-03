
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
    public TextMeshProUGUI m_SerialNum;
    public Button Delete;
    public TMP_InputField m_TopicContent;
    public TMP_InputField m_Score;
    public TMP_Dropdown m_ChoiceNum;
    public GameObject m_ChoiceItem;
    public Transform m_ParentTrans;
    private Dictionary<string, GameObject> m_ChoicesItem = new Dictionary<string, GameObject>();
}