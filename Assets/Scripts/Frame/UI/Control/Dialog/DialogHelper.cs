using Unity.VisualScripting;
using UnityEngine;

public class DialogHelper
{
    public ObjectPool<MessageDialog> m_Pool = new ObjectPool<MessageDialog>();

    public GameObject m_Prefabs;

    public DialogHelper()
    {

    }

    /// <summary>
    /// 对话框实例化方法
    /// </summary>
    /// <returns></returns>
    public MessageDialog MessDialogInstance()
    {
        Transform parent = GameObject.Find("Canvas").gameObject.transform;
        m_Prefabs = Resources.Load("Prefabs/UI/Dialog/MessageDialog") as GameObject;
        MessageDialog dialog = UnityEngine.Object.Instantiate(m_Prefabs, parent)?.gameObject.GetComponent<MessageDialog>();
        return dialog;
    }

    /// <summary>
    /// 创建一个 对话窗口
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public MessageDialog CreateMessDialog(string name)
    {
        m_Pool.RegisterInstance(MessDialogInstance);
        MessageDialog dialog = m_Pool.Create(name).GetComponent<MessageDialog>();
        return dialog;
    }
}