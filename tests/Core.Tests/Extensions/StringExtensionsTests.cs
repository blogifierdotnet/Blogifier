using Xunit;

namespace Core.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("tes't, #one", "test-one")]
        [InlineData("{test [two?", "test-two")]
        [InlineData("test$ ~three!", "test-three")]
        [InlineData("Тест* для& --Кирил/лицы", "тест-для-кириллицы")]
        public void ShouldRemoveIlligalChars(string title, string slug)
        {
            Assert.Equal(title.ToSlug(), slug);
        }
    }
}