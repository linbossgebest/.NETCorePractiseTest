using System;
using System.Collections.Generic;
using System.Text;

namespace Sample03.Model
{
    [Serializable]
    public class DbTable
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表备注
        /// </summary>
        public string TableComment { get; set; }

        /// <summary>
        /// 字段集合
        /// </summary>
        public List<DbTableColumn> Columns { get; set; } = new List<DbTableColumn>();
    }
}
