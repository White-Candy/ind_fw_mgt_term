using System.Collections.Generic;

public static class GlobalData
{
    public static Dictionary<PropertyType, string> m_Enum2Type = new Dictionary<PropertyType, string>()
    {
        {PropertyType.PT_USER_ADDTO, "AddUserAction"}, {PropertyType.PT_USER_SET, "SetUserAction"},
        {PropertyType.PT_FAC_ADDTO, "AddFacAction"}, {PropertyType.PT_FAC_SET, "SetFacAction"},
        {PropertyType.PT_MAJ_ADDTO, "AddMajAction"}, {PropertyType.PT_MAJ_SET, "SetMajAction"},
        {PropertyType.PT_CLASS_ADDTO, "AddClassAction"}, {PropertyType.PT_CLASS_SET, "SetClassAction"},
    };

    public static List<string> facultiesList = new List<string>(); // 学院
    public static List<string> classesList = new List<string>(); // 班级
    public static List<string> majorList = new List<string>(); // 班级
    public static List<string> directorsList = new List<string>(); // 主任
    public static List<string> deanList = new List<string>(); // 院长
    public static List<string> teachersList = new List<string>(); // 老师
}