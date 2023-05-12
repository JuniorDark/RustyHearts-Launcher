namespace RHLauncher.RHLauncher.Helper;

public class Configuration
{
    private static readonly string DefaultIniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");

    public static readonly Configuration Default = new();

    private readonly IniFile iniFile = new(DefaultIniFilePath);

    public Configuration()
    {
        string apiUrl = iniFile.ReadValue("Info", "LoginURL");

        //Client endpoints
        GateXMLUrl = $"{apiUrl}/serverApi/gateway";
        GateInfoUrl = $"{apiUrl}/serverApi/gateway/info";
        GateStatusUrl = $"{apiUrl}/serverApi/gateway/status";

        //Launcher endpoints
        LoginUrl = $"{apiUrl}/accountApi/login";
        RegisterUrl = $"{apiUrl}/accountApi/register";
        SendCodeUrl = $"{apiUrl}/accountApi/sendVerificationEmail";
        VerifyCodeUrl = $"{apiUrl}/accountApi/codeVerification";
        SendPasswordCodeUrl = $"{apiUrl}/accountApi/sendPasswordResetEmail";
        ChangePasswordUrl = $"{apiUrl}/accountApi/changePassword";
        LauncherNewsUrl = $"{apiUrl}/launcher/news";
        AgreementUrl = $"{apiUrl}/launcher/agreement";

        //Client update endpoints
        FileListUrl = $"{apiUrl}/launcher/patch/filelist.txt";
        DownloadUpdateFileUrl = $"{apiUrl}/launcher/patch";

        //Launcher update endpoints
        GetLauncherVersion = $"{apiUrl}/launcherApi/launcherUpdater/getLauncherVersion";
        UpdateLauncherVersion = $"{apiUrl}/launcherApi/launcherUpdater/updateLauncherVersion";
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
}

