using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample03.CodeGenerate;
using Sample03.Model;
using Sample03.Options;
using System;

namespace Sample04
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = Common.BuildService();
            var codeGenerator = serviceProvider.GetRequiredService<CodeGenerator>();
            codeGenerator.GenerateTemplateCodesFromDatabase(true);

            Console.WriteLine("代码生成完毕");
            Console.ReadKey();
        }
    }

    class Common
    {
        public static IServiceProvider BuildService()
        {
            var services = new ServiceCollection();
            services.Configure<CodeGenerateOption>(options =>
            {
                options.ConnectionString = "Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=cms;Pooling=true;Max Pool Size=100;Charset=utf8";
                options.DbType = DatabaseType.MySQL.ToString();
                options.Author = "linyong";
                options.OutputPath = "E:\\myProject\\.netcore\\GenerateCode";
                options.ModelsNameSpace = "LinTest.Cms.Models";
                options.IRepositoryNameSpace = "LinTest.Cms.IRepository";
                options.RepositoryNameSpace = "LinTest.Cms.Repository";
                options.ServicesNameSpace = "LinTest.Cms.IServices";
                options.IServicesNameSpace = "LinTest.Cms.Services";
                options.DbHelperNameSpace = "LinTest.Cms.DbHelperNameSpace";
                options.OptionsNameSpace = "LinTest.Cms.OptionsNameSpace";
                options.BaseRepositoryNameSpace = "LinTest.Cms.BaseRepositoryNameSpace";
            });
            services.Configure<DbOption>("LinCms", options => GetConfiguration().GetSection("DbOption"));
            services.AddScoped<CodeGenerator>();
            return services.BuildServiceProvider();//构建服务提供程序
        }

        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
