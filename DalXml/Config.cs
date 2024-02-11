namespace Dal;

internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }


    internal static DateTime? StartProject
    {
        set => XMLTools.SetDate(value, s_data_config_xml, "InitDate");

        get => XMLTools.GetDate(s_data_config_xml, "InitDate");
    }
    internal static DateTime? EndProject
    {
        set => XMLTools.SetDate(value, s_data_config_xml, "EndDate");

        get => XMLTools.GetDate(s_data_config_xml, "EndDate");
    }
}
