namespace RHLauncher.RHLauncher.Helper;

public class Configuration
{
    private static readonly string DefaultIniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.ini");

    public static readonly Configuration Default = new();

    public readonly IniFile iniFile = new(DefaultIniFilePath);

    public Configuration()
    {
        string apiUrl = iniFile.ReadValue("Info", "ServerURL");

        //Client endpoints
        GateXMLUrl = $"{apiUrl}/launcher/GetGatewayAction";
        GateInfoUrl = $"{apiUrl}/launcher/GetGatewayAction/info";
        GateStatusUrl = $"{apiUrl}/launcher/GetGatewayAction/status";

        //Launcher endpoints
        LoginUrl = $"{apiUrl}/launcher/LoginAction";
        RegisterUrl = $"{apiUrl}/launcher/SignupAction";
        SendCodeUrl = $"{apiUrl}/launcher/SendVerificationEmailAction";
        VerifyCodeUrl = $"{apiUrl}/launcher/VerifyCodeAction";
        SendPasswordCodeUrl = $"{apiUrl}/launcher/SendPasswordResetEmailAction";
        ChangePasswordUrl = $"{apiUrl}/launcher/ResetPasswordAction";
        LauncherNewsUrl = $"{apiUrl}/launcher/news";
        AgreementUrl = $"{apiUrl}/launcher/agreement";

        //Client download endpoints
        ClientPartsListUrl = $"{apiUrl}/launcher/client/download/filelist.txt";
        DownloadClientPartUrl = $"{apiUrl}/launcher/client/download";

        //Client update endpoints
        FileListUrl = $"{apiUrl}/launcher/patch/filelist.txt";
        DownloadUpdateFileUrl = $"{apiUrl}/launcher/patch";

        //Launcher update endpoints
        GetLauncherVersion = $"{apiUrl}/launcherAction/getLauncherVersion";
        UpdateLauncherVersion = $"{apiUrl}/launcherAction/updateLauncherVersion";

        //Launcher settings
        Lang = iniFile.ReadValue("Launcher", "Lang");
        Service = iniFile.ReadValue("Info", "Service");
    }

    public string GateXMLUrl { get; set; }
    public string GateInfoUrl { get; set; }
    public string GateStatusUrl { get; set; }
    public string LoginUrl { get; set; }
    public string RegisterUrl { get; set; }
    public string SendCodeUrl { get; set; }
    public string VerifyCodeUrl { get; set; }
    public string SendPasswordCodeUrl { get; set; }
    public string ChangePasswordUrl { get; set; }
    public string FileListUrl { get; set; }
    public string LauncherNewsUrl { get; set; }
    public string AgreementUrl { get; set; }
    public string GetLauncherVersion { get; set; }
    public string UpdateLauncherVersion { get; set; }
    public string DownloadUpdateFileUrl { get; set; }
    public string ClientPartsListUrl { get; set; }
    public string DownloadClientPartUrl { get; set; }
    public string Lang { get; set; }
    public string Service { get; set; }
}

