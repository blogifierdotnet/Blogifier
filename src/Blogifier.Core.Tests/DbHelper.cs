using Blogifier.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Core.Tests
{
    public class DbHelper
    {
        public AppDbContext GetDbContext()
        {
            return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(GetDataSource()).Options);
        }

        private string GetDataSource()
        {
            string path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.IndexOf("Blogifier.Core.Tests"));
            return $"DataSource={path}Blogifier{Path.DirectorySeparatorChar}Blog.db";
        }
    }
}
