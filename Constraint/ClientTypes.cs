namespace Api.Security.Constraint
{
    //Bulk datas
    public abstract class ClientTypes
    {
        public const string DesktopA = "desktop/win32";
        public const string DesktopB = "desktop/macx";
        public const string DesktopC = "desktop/linux";

        public const string MobileA = "mobile/android";
        public const string MobileB = "mobile/ios";
        public const string MobileC = "mobile/win32";

        public const string BrowserA = "browser/chrome";
        public const string BrowserB = "browser/edge";
        public const string BrowserC = "browser/firefox";
    }
}
