using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 选择题Item
/// </summary>
public class ChoiceItem : MonoBehaviour
{
    public TextMeshProUGUI Serial;
    public TMP_InputField m_Content;
    public Toggle m_toggle;

    public void Init(string serial)
    {
        Serial.text = serial;
    }

    public bool InputFieldCheck()
    {
        if (!UIHelper.InputFieldCheck(m_Content.text)) return false;
        return true;
    }

    public void Init(string serial, string content, bool ison)
    {
        Serial.text = serial;
        m_Content.text = content;
        m_toggle.isOn = ison;
    }

    public void Clear()
    {
        m_Content.text = "";
        m_toggle.isOn = false;
    }
}