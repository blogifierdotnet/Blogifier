using System;
using Xunit;

namespace Blogifier.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
			Assert.True(Core.Common.Settings.Title.Length > 0);
        }

		[Fact]
		public void Test2()
		{
			Assert.True(!string.IsNullOrEmpty(Core.Common.Settings.Title));
		}
	}
}
