using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sample3.Model;
using System.IO;

namespace Sample3
{
    public class MyDbContext:DbContext
    {
        public DbSet<Content> Content { get; set; }

        private IConfiguration configuration;

        public MyDbContext()
        {
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                      .AddJsonFile("appsettings.json")
                                                      .Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(configuration.GetConnectionString("Default"));
        }
    }
}
