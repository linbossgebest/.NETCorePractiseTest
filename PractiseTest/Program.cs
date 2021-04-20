using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace PractiseTest
{
    class Program
    {
        //配置信息的根对象
        public static IConfigurationRoot Configuration { get; set; }


        static void Main(string[] args)
        {
            //var dic = new Dictionary<string, string>() {
            //    { "Class","配置信息" },
            //    { "Infor","User"},
            //    { "User:0:Name","xiaoming" },
            //    { "User:0:Age","20"},
            //    { "User:1:Name","xiaohong"},
            //    { "User:1:Age","18"}
            //};

            //var builder = new ConfigurationBuilder();
            //builder.AddInMemoryCollection(dic);

            //Configuration = builder.Build();

            //var user = new User();
            //Console.WriteLine("获取单个对象的配置信息");
            ////Configuration.GetSection("User:0").Bind(user);
            //var t = Configuration.GetSection("User:0:Name");
            //var Age = Configuration["User:0:Age"];
            //var infor = Configuration["Infor"];
            ////取值并转换
            ////var Age = Configuration.GetValue<int>("User:0:Age");
            //////取值并转换
            ////var Age0 = Configuration.GetValue("User:0:Age", 0);

            //Console.WriteLine($"Age--{Age}");
            //Console.WriteLine($"Info--{infor}");
            ////Console.WriteLine($"Age0 {Age0}");
            ///

            RequestDelegate app = context =>
            {
                context.Output.AppendLine("End of output.");
                return Task.CompletedTask;
            };

            Func<RequestDelegate, RequestDelegate> middleware1 = next => 
            {
                return (HttpContextSample context) => {
                    context.Output.AppendLine("Middleware 1 processing");

                    return next(context);
                };
            };


            Func<RequestDelegate, RequestDelegate> middleware2 = next => 
            {
                return context => {
                    context.Output.AppendLine("Middleware 2 processing");

                    return next(context);
                };
            };

            //var context1 = new HttpContextSample();
            //app(context1);
            //Console.WriteLine(context1.Output.ToString());

            //var pipeline1 = middleware1(app);
            //var context2 = new HttpContextSample();
            //pipeline1(context2);
            //Console.WriteLine(context2.Output.ToString());

            var step1 = middleware1(app);
            var pipeline2 = middleware2(step1);
            var context3 = new HttpContextSample();
            pipeline2(context3);
            Console.WriteLine(context3.Output.ToString());
        

            Console.ReadKey();
        }

        //Func<RequestDelegate, RequestDelegate> middleware2 = next =>
        //{
        //    return context => {
        //        context.Output.AppendLine("Middleware 2 processing");

        //        return next(context);
        //    };
        //};

        //Func<RequestDelegate, RequestDelegate> middleware3 = next(rd);

        //RequestDelegate next(RequestDelegate rd) {
        //    return new RequestDelegate((HttpContextSample context)=> 
        //    {
        //        return rd(context);
        //    });
        //}
    }

    internal class User
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }

}
