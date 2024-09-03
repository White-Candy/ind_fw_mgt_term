
using TMPro;
using UnityEngine;

public class TrainingPanel : BasePanel
{
    public TMP_Dropdown Columns;
    public GameObject m_Item;
    public Transform m_Parent;
    
    public static TrainingPanel inst;

    public override void Awake()
    {
        inst = this;
    }
}