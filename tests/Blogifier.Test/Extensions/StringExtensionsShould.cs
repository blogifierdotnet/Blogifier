using Blogifier.Core.Extensions;
using Xunit;

namespace Blogifier.Test.Extensions
{
    public class StringExtensionsShould
    {
        [Theory]
        [InlineData("")]
        [InlineData("abcdefg")]
        public void IgnoreConnectionStringWithNoPassword(string conn)
        {
            Assert.Equal(conn, conn.RemovePassword());
        }

        [Theory]
        [InlineData("Persist Security Info=False;User ID=tester;Password=One@pass99;MultipleActiveResultSets=False;")]
        [InlineData("Persist Security Info=False;User ID=tester;MultipleActiveResultSets=False;Password=One@pass99;")]
        [InlineData("Password=One@pass99;Persist Security Info=False;User ID=tester;MultipleActiveResultSets=False;")]
        public void ReplacePasswordFromConnectionString(string conn)
        {
            var result = conn.RemovePassword();
            Assert.False(result.Contains("One@pass99"));
            Assert.True(result.Contains("Password=******"));
        }
    }
}
