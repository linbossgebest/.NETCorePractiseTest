using Google.Protobuf.WellKnownTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using Sample03.Extensions;
using Sample03.Model;
using Sample03.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace Sample03.CodeGenerator
{
    /// <summary>
    /// 代码生成器
    /// </summary>
    public class CodeGenerate
    {
        private readonly string Delimiter = "\\";//windows的分隔符

        private static CodeGenerateOption _options;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public CodeGenerate(IOptions<CodeGenerateOption> options)
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
            //var content=
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
                var type = System.Type.GetType(colType);//获取Type
                if (column.IsNullable&& type.IsNullType())//判断列是否可空&&属性对应的c#类型是可空类型
                {
                    colType += "?";
                }
                sb.AppendLine($"\t\tpublic {colType} {column.ColName}"+"{get;set;}");
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
            path = fullPath.Replace("Partial"+Delimiter,"").ToString();
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

    }
}
