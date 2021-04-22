using System;
using System.Collections.Generic;
using System.Text;

namespace Sample03.Options
{
    public class CodeGenerateOption:DbOption
    {
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 代码生成时间
        /// </summary>
        public string GenerateTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 输出路径
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// 实体命名空间
        /// </summary>
        public string ModelsNameSpace { get; set; }

        /// <summary>
        /// 仓储接口命名空间
        /// </summary>
        public string IRepositoryNameSpace { get; set; }

        /// <summary>
        /// 仓储命令空间
        /// </summary>
        public string RepositoryNameSpace { get; set; }

        /// <summary>
        /// 服务接口命名空间
        /// </summary>
        public string IServicesNameSpace { get; set; }

        /// <summary>
        /// 服务命名空间
        /// </summary>
        public string ServicesNameSpace { get; set; }

        /// <summary>
        /// DbHelper命名空间
        /// </summary>
        public string DbHelperNameSpace { get; set; }

        /// <summary>
        /// Options命名空间
        /// </summary>
        public string OptionsNameSpace { get; set; }

        /// <summary>
        /// BaseRepository命名空间
        /// </summary>
        public string BaseRepositoryNameSpace { get; set; }

    }
}
