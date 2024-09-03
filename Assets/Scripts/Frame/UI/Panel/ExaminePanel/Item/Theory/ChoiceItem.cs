using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 选择题Item
/// </summary>
public class ChoiceItem : MonoBehaviour
{
    public TMP_InputField m_Content;
    public Toggle m_toggle;

    public void Clear()
    {
        m_Content.text = "";
        m_toggle.isOn = false;
    }
}