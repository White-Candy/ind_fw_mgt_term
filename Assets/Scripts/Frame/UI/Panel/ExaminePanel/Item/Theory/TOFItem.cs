using TMPro;
using UnityEngine.UI;

/// <summary>
/// 判断题题目Item
/// </summary>
public class TOFItem : BaseExamineItem
{
    public Button Delete;
    public TextMeshProUGUI SerialNum;
    public TMP_InputField Score;
    public TMP_InputField TopicContent;
    public ChoiceItem toA;
    public ChoiceItem toB;
}