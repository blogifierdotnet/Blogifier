using Xunit;

namespace Blogifier.Core.Tests.Extensions
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

        [Theory]
        [InlineData("http://foo/bar/img.jpg", "http://foo/bar/thumbs/img.jpg")]
        [InlineData("foo/bar//img-foo.jpg", "foo/bar//thumbs/img-foo.jpg")]
        [InlineData("foo/bar/img.one.png", "foo/bar/thumbs/img.one.png")]
        public void ShouldConvertImgPathToThumbPath(string img, string thumb)
        {
            Assert.Equal(img.ToThumb(), thumb);
        }
    }
}
