
using LitJson;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsItem : MonoBehaviour
{
    public TextMeshProUGUI userName;
    public TextMeshProUGUI courseName;
    public TextMeshProUGUI useTime;
    public Button deleteButton;

    private UsrTimeInfo m_Info = new UsrTimeInfo();

    public void Start()
    {
        deleteButton.onClick.AddListener(() =>
        {
            if (GlobalData.s_currUsrLevel == 0) return;
            DialogHelper helper = new DialogHelper();
            MessageDialog dialog = helper.CreateMessDialog("MessageDialog");
            dialog.Show("��Ϣ��ɾ��", "�Ƿ�ɾ���ÿ���ͳ����Ϣ��", new ItemPackage("ȷ��", ConfirmDelete), new ItemPackage("ȡ��", null));
        });
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <param name="info"></param>
    public void Init(UsrTimeInfo info)
    {
        userName.text = info.usrName;
        courseName.text = info.moduleName;
        useTime.text = info.min.ToString();

        gameObject.SetActive(true);
        m_Info = info;
    }

    /// <summary>
    /// ȷ��ɾ��
    /// </summary>
    public async void ConfirmDelete()
    {
        string infStr = JsonMapper.ToJson(m_Info);
        await Client.m_Server.HttpRequest(GlobalData.IP + "DeleteUsrTime", infStr, (ret) =>
        {
            StatisticsPanel panel = UIHelper.FindPanel<StatisticsPanel>();
            panel.Show(ret);
        });
    }
}
