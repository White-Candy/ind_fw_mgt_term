using System.Collections.Generic;

public static class GlobalData
{
    public static Dictionary<PropertyType, string> m_Enum2Type = new Dictionary<PropertyType, string>()
    {
        {PropertyType.PT_STU_ADDTO, "AddStuAction"}, {PropertyType.PT_STU_SET, "SetStuAction"},
        {PropertyType.PT_FAC_ADDTO, "AddFacAction"}, {PropertyType.PT_FAC_SET, "SetFacAction"},
        {PropertyType.PT_MAJ_ADDTO, "AddMajAction"}, {PropertyType.PT_MAJ_SET, "SetMajAction"}
    };

    public static Dictionary<string, int> m_StuGenderDic = new Dictionary<string, int>()
    {
        {"男", 0}, {"女", 1}
    };
}