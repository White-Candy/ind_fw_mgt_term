
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
    public GameObject m_toA;
    public GameObject m_toB;
    public GameObject m_toC;
    public GameObject m_toD;
}