using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sample03.Model;
using System.IO;

namespace Sample03
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
