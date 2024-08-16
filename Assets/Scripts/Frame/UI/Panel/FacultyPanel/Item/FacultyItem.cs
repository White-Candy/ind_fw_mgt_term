
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class FacultyItem : MonoBehaviour
{
    public GameObject Faculty;
    public GameObject ReigsterTime;
    public GameObject TeacherName;
    public Button Revise;
    public Button Delete;

    private FacultyInfo m_info = new FacultyInfo();

    public void Start()
    {
        Delete.OnClickAsObservable().Subscribe(_ => 
        {
            
        });

        Revise.OnClickAsObservable().Subscribe(_ => 
        {
            FacPropertyDialog.instance.Init(m_info, PropertyType.PT_FAC_SET);
            FacPropertyDialog.instance.Active(true);
        });
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="info"></param>
    public void Init(FacultyInfo info)
    {
        m_info = info;
        
        Faculty.GetComponentInChildren<TextMeshProUGUI>().text = info.Name;
        ReigsterTime.GetComponentInChildren<TextMeshProUGUI>().text = info.RegisterTime;
        TeacherName.GetComponentInChildren<TextMeshProUGUI>().text = info.TeacherName;

        gameObject.SetActive(true);
    }
}