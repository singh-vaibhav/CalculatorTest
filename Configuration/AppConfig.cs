

namespace UiAutomationTests
{
    public class AppConfig
    {
        public string Url { get; set; }
        public string Browser { get; set; }
        public bool Headless { get; set; }
        public string Environment { get; set; }




        public AppConfig()
        {
            //Application level  if we need to run the test on different environment{dev,qa,stage}
           this.Environment = "qa";
            this.Url = "https://vast-dawn-73245.herokuapp.com/";
            this.Browser = "Chrome";
            this.Headless = false;
        }
    }
}
