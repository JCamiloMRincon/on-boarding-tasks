using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.IO;
using Allure.Commons;

[SetUpFixture]
public class TestSetup
{
    [OneTimeSetUp]
    public void Setup()
    {
        string resultsDirectory = Path.Combine(TestContext.CurrentContext.WorkDirectory, "allure-results");
        Environment.SetEnvironmentVariable("ALLURE_RESULTS_DIRECTORY", resultsDirectory);
    }
}

