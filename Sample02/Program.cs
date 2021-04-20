using Dapper;
using MySql.Data.MySqlClient;
using Sample02.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //test_insert();
            //test_mult_insert();

            //test_del();
            //test_mutl_del();

            //test_update();
            //test_mult_update();

            test_select_one();
            test_select_list();

            test_select_content_with_comment();
        }

        static void test_insert()
        {
            var content = new Content()
            {
                title = "标题1",
                content = "内容1"
            };

            using (var conn = new MySqlConnection("Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=cms;Pooling=true;Max Pool Size=100;Charset=utf8"))
            {
                string sql_insert = @"INSERT INTO Content
                (title, content, status, add_time, modify_time)
VALUES   (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"test_insert：插入了{result}条数据！");
            }
        }

        static void test_mult_insert()
        {
            List<Content> contents = new List<Content>() {
            new Content()
            {
                title="批量插入标题1",
                content="批量插入内容1"
            },
            new Content()
            {
                title="批量插入标题2",
                content="批量插入内容2"
            }
            };

            using (var conn = new MySqlConnection("Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=cms;Pooling=true;Max Pool Size=100;Charset=utf8"))
            {
                string sql_insert = @"INSERT INTO Content
                (title, content, status, add_time, modify_time)
VALUES   (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_insert：插入了{result}条数据！");
            }

        }

        static void test_del()
        {
            var content = new Content()
            {
                id = 6
            };

            using (var conn = new MySqlConnection("Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=cms;Pooling=true;Max Pool Size=100;Charset=utf8"))
            {
                string sql_insert = @"Delete from Content where id=@id";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"test_del：删除了{result}条数据！");
            }

        }

        static void test_mutl_del()
        {
            List<Content> contents = new List<Content>()
            {
                new Content()
                {
                    id=7
                },
                new Content()
                {
                    id=8
                }

            };

            using (var conn = new MySqlConnection("Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=cms;Pooling=true;Max Pool Size=100;Charset=utf8"))
            {
                string sql_insert = @"Delete from Content where id=@id";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mutl_del：删除了{result}条数据！");
            }
        }

        static void test_update()
        {
            var content = new Content
            {
                id = 9,
                title = "update title1",
                content = "update content1"
            };

            using (var conn = new MySqlConnection("Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=cms;Pooling=true;Max Pool Size=100;Charset=utf8"))
            {
                string sql_insert = @"Update  Content  Set title=@title,content=@content,modify_time=now() where id=@id";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"test_update：修改了{result}条数据！");
            }
        }

        static void test_mult_update()
        {
            List<Content> contents = new List<Content>() {
            new Content()
            {
                id=10,
                title="批量修改标题1",
                content="批量修改内容1"
            },
            new Content()
            {
                id=11,
                title="批量修改标题2",
                content="批量修改内容2"
            }
            };

            using (var conn = new MySqlConnection("Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=cms;Pooling=true;Max Pool Size=100;Charset=utf8"))
            {
                string sql_insert = @"Update  Content  Set title=@title,content=@content,modify_time=now() where id=@id";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_update：修改了{result}条数据！");
            }
        }

        static void test_select_one()
        {
            using (var conn = new MySqlConnection("Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=cms;Pooling=true;Max Pool Size=100;Charset=utf8"))
            {
                string sql_insert = @"select * from content where id=@id";
                var result = conn.QueryFirstOrDefault<Content>(sql_insert, new { id=9});
                Console.WriteLine($"test_select_one：查到了{result}条数据！");
            }
        }

        static void test_select_list()
        {
            using (var conn = new MySqlConnection("Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=cms;Pooling=true;Max Pool Size=100;Charset=utf8"))
            {
                string sql_insert = @"select * from content where id in @ids";
                var result = conn.Query<Content>(sql_insert, new { ids= new int[] { 10,11} });
                Console.WriteLine($"test_select_one：查到了{result}条数据！");
            }
        }

        static void test_select_content_with_comment() 
        {
            using (var conn = new MySqlConnection("Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=cms;Pooling=true;Max Pool Size=100;Charset=utf8"))
            {
                string sql_insert = @"select * from content where id=@id;
                                      select * from comment where id=@id;";
                using (var result = conn.QueryMultiple(sql_insert, new { id = 9 }))
                {
                    var content = result.ReadFirstOrDefault<ContentWithComment>();
                    content.comments = result.Read<Comment>();
                    Console.WriteLine($"test_select_content_with_comment:id=9的评论数量{content.comments.Count()}");
                }
            }
        }
    }
}
