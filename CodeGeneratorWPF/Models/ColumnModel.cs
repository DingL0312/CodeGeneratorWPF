using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * @Description: 
 * @Author: DLiang
 * @Date: 2024/1/2 11:32:10
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
*/

namespace CodeGeneratorWPF.Models
{
    public class ColumnModel
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string? Field { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// 是否为空
        /// </summary>
        public string? Nullable { get; set; }
        /// <summary>
        /// 是否为主键 PRI
        /// </summary>
        public string? Key { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string? Default { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public string? Length { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string? Comment { get; set; }
    }
}
