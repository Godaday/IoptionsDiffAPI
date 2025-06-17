namespace IoptionsDiffAPI;

public record AppSettingOption
{
    public static string SectionName => "AppSetting";

    public string Name { get; set; }
    public string Theme { get; set; }
    public string URL { get; set; }


}
