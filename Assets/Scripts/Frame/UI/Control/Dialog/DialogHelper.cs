
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DialogHelper : Singleton<DialogHelper>
{
    public ObjectPool<MessageDialog> m_Pool = new ObjectPool<MessageDialog>();

    public GameObject m_Prefabs;

    public override void Awake()
    {
        base.Awake();

        m_Pool.RegisterInstance(MessDialogInstance);
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 对话框实例化方法
    /// </summary>
    /// <returns></returns>
    public MessageDialog MessDialogInstance()
    {
        Transform parent = GameObject.Find("Canvas").gameObject.transform;
        m_Prefabs = Resources.Load("Prefabs/UI/Dialog/MessageDialog") as GameObject;
        MessageDialog dialog = Instantiate(m_Prefabs, parent)?.gameObject.GetComponent<MessageDialog>();
        return dialog;
    }

    /// <summary>
    /// 创建一个 对话窗口
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public MessageDialog CreateMessDialog(string name)
    {
        MessageDialog dialog = m_Pool.Create(name).GetComponent<MessageDialog>();
        return dialog;
    }
}