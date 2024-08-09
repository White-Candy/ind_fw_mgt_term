using Cathei.BakingSheet.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Button Set;
    public Button Delete;

    public void Init(UserInfo inf)
    {
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
}  
