using Core.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Services
{
    public class WebServiceTests
    {
        private readonly Mock<IDataService> _unitOfWork = new Mock<IDataService>();
        private readonly Mock<IConfiguration> _config = new Mock<IConfiguration>();
        
        private WebService GetSut()
        {
            return new WebService(_unitOfWork.Object, _config.Object);
        }

        [Fact]
        public async Task CanImportFromRssFeed()
        {
            var sut = GetSut();

            var result = await sut.GetNotifications();

            Assert.NotNull(result);
        }
    }
}
