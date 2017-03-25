using System;
using Xunit;

namespace Blogifier.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
			var x = Core.Common.ApplicationSettings.ContentRootPath;
			Assert.True(Core.Common.ApplicationSettings.Title.Length > 0);
        }

		[Fact]
		public void Test2()
		{
			Assert.True(!string.IsNullOrEmpty(Core.Common.ApplicationSettings.Title));
		}
	}
}
