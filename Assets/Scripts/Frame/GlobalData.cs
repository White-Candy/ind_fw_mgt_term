using System.Collections.Generic;

public static class GlobalData
{
    public static Dictionary<PropertyType, string> m_Enum2Type = new Dictionary<PropertyType, string>()
    {
        {PropertyType.PT_STU_ADD, "AddStuAction"}, {PropertyType.PT_STU_SET, "SetStuAction"}
    };
}