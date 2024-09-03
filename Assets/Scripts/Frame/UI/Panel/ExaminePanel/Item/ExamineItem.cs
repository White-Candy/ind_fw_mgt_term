
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamineItem : MonoBehaviour
{
    public GameObject ID;
    public GameObject Course;
    public GameObject Register;
    public GameObject Status;
    public GameObject ClassNum;
    public Button Revise;
    public Button Delete;

    private ExamineInfo m_info = new ExamineInfo();

    public void Init(ExamineInfo inf)
    {
        m_info = inf;

        ID.GetComponentInChildren<TextMeshProUGUI>().text = inf.id;
        Course.GetComponentInChildren<TextMeshProUGUI>().text = inf.CourseName;
        Register.GetComponentInChildren<TextMeshProUGUI>().text = inf.RegisterTime;
        Status.GetComponentInChildren<TextMeshProUGUI>().text = inf.Status ? "开启" : "关闭";
        ClassNum.GetComponentInChildren<TextMeshProUGUI>().text = inf.ClassNum.ToString();
    }
}