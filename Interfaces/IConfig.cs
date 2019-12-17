using UiAutomationTests.Configuration;

namespace UiAutomationTests.Interfaces
{
    public interface IConfig
    {
        BrowserType? GetBrowser();
        string GetUrl ();
        int GetPageLoadTimeOut();
        int GetElementLoadTimeOut();

    }
}
