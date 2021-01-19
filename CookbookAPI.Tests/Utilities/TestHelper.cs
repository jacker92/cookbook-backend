using CookbookAPI.Utilities;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookbookAPI.Tests.Utilities
{
    public static class TestHelper
    {
        public static IOptions<AppSettings> CreateTestAppSettings()
        {
            var appSettings = new Mock<IOptions<AppSettings>>();
            appSettings.Setup(x => x.Value).Returns(new AppSettings()
            {
                Secret = "ThisIsTheBestSecretOfAllTime"
            });
            return appSettings.Object;
        }
    }
}
