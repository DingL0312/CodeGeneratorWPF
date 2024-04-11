using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * @Description: 
 * @Author: DLiang
 * @Date: 2024/1/5 17:29:10
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
*/

namespace CodeGeneratorWPF.Models.TemplateModel
{
   public class BaseTemplateModel
    {
        public string Author { get; set; }
        public string Date { get; set; }
        public string Comment { get; set; }
        public string ClassName { get; set; }
        public string BasePackage { get; set; }
    }
}
