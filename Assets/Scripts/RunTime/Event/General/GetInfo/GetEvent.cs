using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LitJson;
using UnityEngine;

public class GetEvent : BaseEvent
{
    public override async void OnEvent(MessPackage mp)
    {        
        JsonData jd = JsonMapper.ToObject(mp.ret);

        GlobalData.facultiesList = JsonMapper.ToObject<List<string>>(jd["facultiesList"].ToString());
        GlobalData.classesList = JsonMapper.ToObject<List<string>>(jd["classesList"].ToString());
        GlobalData.majorList = JsonMapper.ToObject<List<string>>(jd["majorList"].ToString());
        GlobalData.directorsList = JsonMapper.ToObject<List<string>>(jd["directorsList"].ToString());
        GlobalData.deanList = JsonMapper.ToObject<List<string>>(jd["deanList"].ToString());
        GlobalData.teachersList = JsonMapper.ToObject<List<string>>(jd["teachersList"].ToString());
        GlobalData.columnsList = JsonMapper.ToObject<List<string>>(jd["columnsList"].ToString());
        GlobalData.coursesList = JsonMapper.ToObject<List<CourseInfo>>(jd["coursesList"].ToString());
        await UniTask.Yield();
    }
}