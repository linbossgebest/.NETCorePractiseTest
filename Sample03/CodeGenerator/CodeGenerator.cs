using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Sample03.DbHelper;
using Sample03.Extensions;
using Sample03.Model;
using Sample03.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sample03.CodeGenerate
{
    /// <summary>
    /// 代码生成器
    /// </summary>
    public class CodeGenerator
    {
        private readonly string Delimiter = "\\";//windows的分隔符

        private static CodeGenerateOption _options;//代码生成选项

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public CodeGenerator(IOptions<CodeGenerateOption> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            _options = options.Value;
            foreach (var property in _options.GetType().GetProperties())
            {
                string value = property.GetValue(_options).ToString();
                if (value.IsNullOrWhiteSpace())//判断选项内容是否为空
                    throw new ArgumentNullException(property.Name);
            }
        }

        /// <summary>
        /// 根据数据库连接字符串生成对应模板代码文件
        /// </summary>
        /// <param name="isCoveredExsited"></param>
        public void GenerateTemplateCodesFromDatabase(bool isCoveredExsited = true)
        {
            DatabaseType dbType = ConnectionFactory.GetDataBaseType(_options.DbType);
            List<DbTable> tables = new List<DbTable>();
            using (var dbConnection = ConnectionFactory.CreateConnection(dbType, _options.ConnectionString))
            {
                tables = dbConnection.GetCurrentDatabaseTableList(dbType);
            }
            if (tables.Count > 0)
            {
                foreach (var table in tables)
                {
                    GenerateEntity(table, isCoveredExsited);
                    if (table.Columns.Any(f => f.IsPrimaryKey)) 
                    {
                        var pkTypeName = table.Columns.FirstOrDefault(k => k.IsPrimaryKey).CSharpType;
                        GenerateIRepository(table, pkTypeName, isCoveredExsited);
                        GenerateRepository(table, pkTypeName, isCoveredExsited);
                    }
                    GenerateIServices(table, isCoveredExsited);
                    GenerateServices(table, isCoveredExsited);
                }
            }
        }

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="isCoveredExsited">是否覆盖</param>
        private void GenerateEntity(DbTable table, bool isCoveredExsited = true)
        {
            var sb = new StringBuilder();
            foreach (var column in table.Columns)
            {
                var tmp = GenerateEntityProperty(column);
                sb.AppendLine(tmp);
            }
            GenerateModelPath(table, out string path, out string pathP);

            var content = ReadTemplate("ModelTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                             .Replace("{Author}", _options.Author)
                             .Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                             .Replace("{ModelsNamespace}", _options.ModelsNameSpace)
                             .Replace("{ModelName}", table.TableName)
                             .Replace("{ModelProperties}", sb.ToString());
            WriteAndSave(path, content);

            #region 部分类，用来扩展属性

            var contentP = ReadTemplate("ModelTemplate.txt");
            contentP = contentP.Replace("{Comment}", table.TableComment)
                             .Replace("{Author}", _options.Author)
                             .Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                             .Replace("{ModelsNamespace}", _options.ModelsNameSpace)
                             .Replace("{ModelName}", table.TableName)
                             .Replace("{ModelProperties}", "");
            WriteAndSave(pathP, contentP);

            #endregion
        }

        /// <summary>
        /// 生成IService层代码文件
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="isCoveredExsited">是否覆盖</param>
        private void GenerateIServices(DbTable table, bool isCoveredExsited = true)
        {
            var iServicePath = _options.OutputPath + Delimiter + "IServices";
            if (!Directory.Exists(iServicePath))
            {
                Directory.CreateDirectory(iServicePath);
            }
            var fullPath = iServicePath + Delimiter + "I" + table.TableName + "Service.cs";
            if (File.Exists(fullPath) && !isCoveredExsited)
                return;
            var content = ReadTemplate("IServicesTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                           .Replace("{Author}", _options.Author)
                           .Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                           .Replace("{IServicesNamespace}", _options.IServicesNameSpace)
                           .Replace("{ModelName}", table.TableName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成Service层代码
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="isCoveredExsited">是否覆盖</param>
        private void GenerateServices(DbTable table, bool isCoveredExsited = true)
        {
            var servicePath = _options.OutputPath + Delimiter + "Services";
            if (!Directory.Exists(servicePath))
            {
                Directory.CreateDirectory(servicePath);
            }
            var fullPath = servicePath + Delimiter + "Service.cs";
            if (File.Exists(fullPath) && !isCoveredExsited)
                return;
            var content = ReadTemplate("ServicesTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                         .Replace("{Author}", _options.Author)
                         .Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                         .Replace("{ServicesNamespace}", _options.ServicesNameSpace)
                         .Replace("{ModelName}", table.TableName)
                         .Replace("{IRepositoryNamespace}", _options.IRepositoryNameSpace)
                         .Replace("{IServicesNamespace}", _options.IServicesNameSpace);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成IRepository层代码
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="keyTypeName">主键类型名</param>
        /// <param name="isCoveredExsited">是否覆盖</param>
        private void GenerateIRepository(DbTable table, string keyTypeName, bool isCoveredExsited = true)
        {
            var iRepositoryPath = _options.OutputPath + Delimiter + "IRepository";
            if (!Directory.Exists(iRepositoryPath))
            {
                Directory.CreateDirectory(iRepositoryPath);
            }
            var fullPath = iRepositoryPath + Delimiter + "I" + table.TableName + "Repository.cs";
            if (File.Exists(fullPath) && !isCoveredExsited)
                return;
            var content = ReadTemplate("IRepositoryTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                         .Replace("{Author}", _options.Author)
                         .Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                         .Replace("{IRepositoryNamespace}", _options.IRepositoryNameSpace)
                         .Replace("{ModelName}", table.TableName)
                         .Replace("{RepositoryNamespace}", _options.RepositoryNameSpace)
                         .Replace("{ModelsNamespace}", _options.ModelsNameSpace)
                         .Replace("{KeyTypeName}", keyTypeName);
            WriteAndSave(fullPath, content);
        }

        /// <summary>
        /// 生成Repository层代码
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="keyTypeName">主键类型名</param>
        /// <param name="isCoveredExsited">是否覆盖</param>
        private void GenerateRepository(DbTable table, string keyTypeName, bool isCoveredExsited = true)
        {
            var repositoryPath = _options.OutputPath + Delimiter + "Repository";
            if (!Directory.Exists(repositoryPath))
            {
                Directory.CreateDirectory(repositoryPath);
            }
            var fullPath = repositoryPath + Delimiter + table.TableName + "Repository.cs";
            if (File.Exists(fullPath) && !isCoveredExsited)
                return;
            var content = ReadTemplate("RepositoryTemplate.txt");
            content = content.Replace("{Comment}", table.TableComment)
                         .Replace("{Author}", _options.Author)
                         .Replace("{GeneratorTime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                         .Replace("{RepositoryNamespace}", _options.RepositoryNameSpace)
                         .Replace("{ModelName}", table.TableName)
                         .Replace("{DbHelperNameSpace}", _options.DbHelperNameSpace)
                         .Replace("{OptionsNameSpace}", _options.OptionsNameSpace)
                         .Replace("{BaseRepositoryNameSpace}", _options.BaseRepositoryNameSpace)
                         .Replace("{RepositoryNamespace}", _options.RepositoryNameSpace)
                         .Replace("{ModelsNamespace}", _options.ModelsNameSpace)
                         .Replace("{KeyTypeName}", keyTypeName);

            WriteAndSave(fullPath, content);

        }

        /// <summary>
        /// 生成实体的属性
        /// </summary>
        /// <param name="column">列</param>
        private static string GenerateEntityProperty(DbTableColumn column)
        {
            var sb = new StringBuilder();
            if (!column.Comment.IsNullOrWhiteSpace())
            {
                sb.AppendLine("\t\t///<summary>");
                sb.AppendLine($"\t\t{column.Comment}");
                sb.AppendLine("\t\t///</summary>");
            }
            if (column.IsPrimaryKey)
            {
                sb.AppendLine("\t\t[Key]");
                sb.AppendLine($"\t\tpublic {column.CSharpType} Id " + "{get;set;}");
            }
            else
            {
                if (!column.IsNullable)
                {
                    sb.AppendLine("\t\t[Required]");
                }
                if (column.ColumnLength.HasValue && column.ColumnLength.Value > 0)
                {
                    sb.AppendLine($"\t\t[MaxLength({column.ColumnLength.Value})]");
                }
                var colType = column.CSharpType;
                //var type = System.Type.GetType(colType);//获取Type
                //if (column.IsNullable && type != null && type.IsNullType())//判断列是否可空&&属性对应的c#类型是可空类型
                if (colType.ToLower() != "string" && colType.ToLower() != "byte[]" && colType.ToLower() != "object" &&
             column.IsNullable)
                {
                    colType += "?";
                }
                sb.AppendLine($"\t\tpublic {colType} {column.ColName}" + "{get;set;}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成实体路径
        /// </summary>
        /// <param name="table">表信息</param>
        /// <param name="path">实体路径</param>
        /// <param name="pathP">部分类路径</param>
        private void GenerateModelPath(DbTable table, out string path, out string pathP)
        {
            var modelPath = _options.OutputPath + Delimiter + "Models";
            if (!Directory.Exists(modelPath))
            {
                Directory.CreateDirectory(modelPath);
            }
            StringBuilder fullPath = new StringBuilder();
            fullPath.Append(modelPath);
            fullPath.Append(Delimiter);
            fullPath.Append("Partial");
            if (!Directory.Exists(fullPath.ToString()))
            {
                Directory.CreateDirectory(fullPath.ToString());
            }
            fullPath.Append(Delimiter);
            fullPath.Append(table.TableName);
            fullPath.Append(".cs");
            pathP = fullPath.ToString();
            path = fullPath.Replace("Partial" + Delimiter, "").ToString();
        }

        /// <summary>
        /// 读取代码模板
        /// </summary>
        /// <param name="templateName">模板名称</param>
        /// <returns></returns>
        private string ReadTemplate(string templateName)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var text = string.Empty;
            var resourceName = $"{currentAssembly.GetName().Name}.CodeTemplate.{templateName}";
            using (var stream = currentAssembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        text = reader.ReadToEnd();
                    }
                }
            }
            return text;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文件内容</param>
        private static void WriteAndSave(string filePath, string content)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.Write(content);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
            }
        }

    }
}
