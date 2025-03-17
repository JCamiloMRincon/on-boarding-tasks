using Allure.Net.Commons;

[SetUpFixture]
public class AllureConfig
{
    [OneTimeSetUp]
    public void SetUp()
    {
        AllureLifecycle.Instance.CleanupResultDirectory();
    }
}