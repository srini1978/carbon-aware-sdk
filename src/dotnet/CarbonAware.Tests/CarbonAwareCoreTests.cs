﻿namespace CarbonAware.Tests;

public class CarbonAwareCoreTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void GetsDataAsExpected()
    {
        // Create a carbon aware core with a mock plugin
        // and data service
        var staticDataService = new MockDataService();
        var plugin = new MockLogicPlugin(staticDataService);
        var carbonAware = new CarbonAwareCore(plugin);

        // This is NOT a logic test, the carbon aware core
        // is a pass through that adds logging and consistency
        // This should return what the mock service returns
        // which is the entire list from the data source
        // with zero filtering or logic
        var list = carbonAware.GetBestEmissionsDataForLocationsByTime(new List<string> { "westus" }, DateTime.Now);
        var directList = staticDataService.GetData();
        Assert.AreEqual(list, directList);

        // ... regardless of parameters... 
        list = carbonAware.GetBestEmissionsDataForLocationsByTime(new List<string> { "eastus" }, DateTime.Now);
        directList = staticDataService.GetData();
        Assert.AreEqual(list, directList);
    }
}