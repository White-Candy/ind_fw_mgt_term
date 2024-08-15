
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SutItem : MonoBehaviour
{
    public Button UserName;
    public Button Name;
    public Button Gender;
    public Button IDCoder;
    public Button Age;
    public Button Contact;
    public Button HeadTeacher;
    public Button ClassName;
    public Button Revise;
    public Button Delete;

    private UserInfo m_inf = new UserInfo();

    public void Start()
    {
        // 信息修改
        Revise.OnClickAsObservable().Subscribe(x => 
        {
            StuPropertyDialog.instance.Init(m_inf, PropertyType.PT_STU_SET);
            StuPropertyDialog.instance.Active(true);
        });

        // 信息删除
        Delete.OnClickAsObservable().Subscribe(x => 
        {
            MessageDialog dialog = DialogHelper.Instance.CreateMessDialog("MessageDialog");
            dialog.Init("学生信息的删除", "是否删除学生信息？", new ItemPackage("确定", ConfirmDelete), new ItemPackage("取消", null));     
        });
    }

    // 初始化
    public void Init(UserInfo inf)
    {
        m_inf = inf;

        UserName.GetComponentInChildren<TextMeshProUGUI>().text = inf.userName;
        Name.GetComponentInChildren<TextMeshProUGUI>().text = inf.Name;
        Gender.GetComponentInChildren<TextMeshProUGUI>().text = inf.Gender;
        IDCoder.GetComponentInChildren<TextMeshProUGUI>().text = inf.idCoder;
        Age.GetComponentInChildren<TextMeshProUGUI>().text = inf.Age;
        Contact.GetComponentInChildren<TextMeshProUGUI>().text = inf.Contact;
        HeadTeacher.GetComponentInChildren<TextMeshProUGUI>().text = inf.HeadTeacher;
        ClassName.GetComponentInChildren<TextMeshProUGUI>().text = inf.className;
        gameObject.SetActive(true);    
    }

    /// <summary>
    /// 确认删除
    /// </summary>
    public void ConfirmDelete()
    {
        TCPHelper.DeleteUserInfo(m_inf);
    }
}  
