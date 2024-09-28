
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UserItem : MonoBehaviour
{
    public Button UserName;
    public Button Name;
    public Button Gender;
    public Button IDCoder;
    public Button Age;
    public Button Identity;
    public Button UnitName;
    public Button Contact;
    public Button Revise;
    public Button Delete;

    private UserInfo m_inf = new UserInfo();

    public void Start()
    {
        // 信息修改
        Revise.OnClickAsObservable().Subscribe(x => 
        {
            if (GlobalData.s_currUsrLevel == 1) return;
            UserPropertyDialog.instance.Init(m_inf, PropertyType.PT_USER_SET);
            UserPropertyDialog.instance.Active(true);
        });

        // 信息删除
        Delete.OnClickAsObservable().Subscribe(x => 
        {
            if (GlobalData.s_currUsrLevel == 1) return;
            DialogHelper helper = new DialogHelper();
            MessageDialog dialog = helper.CreateMessDialog("MessageDialog");
            dialog.Show("用户信息删除", "是否删除用户信息？", new ItemPackage("确定", ConfirmDelete), new ItemPackage("取消", null));     
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
        Identity.GetComponentInChildren<TextMeshProUGUI>().text = inf.Identity;
        Age.GetComponentInChildren<TextMeshProUGUI>().text = inf.Age;
        UnitName.GetComponentInChildren<TextMeshProUGUI>().text = inf.UnitName;
        Contact.GetComponentInChildren<TextMeshProUGUI>().text = inf.Contact;

        // ClassName.GetComponentInChildren<TextMeshProUGUI>().text = inf.className;
        gameObject.SetActive(true);    
    }

    /// <summary>
    /// 确认删除
    /// </summary>
    public void ConfirmDelete()
    {
        NetHelper.OperateInfo(m_inf, EventType.UserEvent, OperateType.DELETE);
    }
}  
