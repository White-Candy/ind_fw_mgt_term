using UnityEngine;

public class MessageDialog : BasePanel
{
    // 按钮对象池
    public ObjectPool<MessageDialogItem> m_Pool = new ObjectPool<MessageDialogItem>();
}