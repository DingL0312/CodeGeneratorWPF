using CodeGeneratorWPF.Models;
using CodeGeneratorWPF.Models.TemplateModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * @Description: 
 * @Author: DLiang
 * @Date: 2024/1/4 15:00:13
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
*/
namespace CodeGeneratorWPF.Services
{
   public interface IGeneratorService
    {
        Task<bool>  GeneratorDo(EntityModel entityModel);
    }
}
