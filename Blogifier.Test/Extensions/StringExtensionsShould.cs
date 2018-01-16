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
            Assert.Equal(conn, conn.MaskPassword());
        }

        [Theory]
        [InlineData("Persist Security Info=False;User ID=tester;Password=One@pass99;MultipleActiveResultSets=False;")]
        [InlineData("Persist Security Info=False;User ID=tester;password=One@pass99;MultipleActiveResultSets=False;")]
        [InlineData("Persist Security Info=False;User ID=tester;PASSWORD=One@pass99;MultipleActiveResultSets=False;")]
        [InlineData("Persist Security Info=False;User ID=tester;MultipleActiveResultSets=False;Password=One@pass99;")]
        [InlineData("Password=One@pass99;Persist Security Info=False;User ID=tester;MultipleActiveResultSets=False;")]
        public void ReplacePasswordFromConnectionString(string conn)
        {
            var result = conn.MaskPassword();
            Assert.DoesNotContain("One@pass99", result);
            Assert.Contains("Password=******", result);
        }
    }
}
