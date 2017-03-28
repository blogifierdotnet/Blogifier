using System;
using System.Security.Principal;
using System.Security.Claims;
using Xunit;
using Blogifier.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Test
{
    public class UnitTest1
    {
		public UnitTest1()
		{
			//var identiry = new GenericIdentity("test@us.com");
			//identiry.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "test@us.com"));
			//identiry.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));
			//var principal = new GenericPrincipal(identiry, null);

			//var options = new DbContextOptionsBuilder<BlogifierDbContext>().UseInMemoryDatabase().Options;
			//var db = new BlogifierDbContext(options);
			//var uow = new Core.Data.Repositories.UnitOfWork(db);

			//var controller = new Core.Controllers.AdminController(uow, null);
			//controller.User = principal;
		}

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
