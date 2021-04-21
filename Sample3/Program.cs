using System;

namespace Sample3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbcontext = new MyDbContext())
            {
                //dbcontext.Content.Add(new Model.Content() 
                //{
                //    title="efcore title1",
                //    content="efcore content1"
                //});

                //dbcontext.SaveChanges();

                foreach (var item in dbcontext.Content)
                {
                    Console.WriteLine($"{item.id}-{item.title}-{item.content}");
                }
            }
            Console.ReadKey();
        }
    }
}
