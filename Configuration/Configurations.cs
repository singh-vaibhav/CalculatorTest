
namespace UiAutomationTests

{
    public class Environments
    {

        public string url { get; set; }

        public string browser { get; set; }

        public bool headless { get; set; }

    }

    public class Configurations
    {
        public Environments Environments { get; set; }
    }


}
