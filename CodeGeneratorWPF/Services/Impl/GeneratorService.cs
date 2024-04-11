using CodeGeneratorWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JinianNet.JNTemplate;
using System.Windows.Shapes;
using System.Collections;
using JinianNet.JNTemplate.Dynamic;
using CodeGeneratorWPF.Models.TemplateModel;

/*
 * @Description:
 * @Author: DLiang
 * @Date: 2024/1/4 15:01:20
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
 */

namespace CodeGeneratorWPF.Services.Impl
{
    public class GeneratorService : IGeneratorService
    {


        private const string doTemplatePath = "./resources/codetemplate/dotemplate.java";
        private const string doOutPath = "./resources/codetemplate";

        public async Task<bool> GeneratorDo(EntityModel entityModel)
        {
            try
            {
                var template = Engine.LoadTemplate(doTemplatePath);

                List<PropertyInfo> propertyInfos = entityModel.GetType().GetProperties().ToList();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    string name = propertyInfo.Name;
                    string typeName = propertyInfo.GetType().Name;
                    
                    if (propertyInfo.PropertyType.IsGenericType)
                    {
                        var value = propertyInfo.GetValue(entityModel) as IEnumerable<ColumnModel>;
                        template.Set(name, value.ToList());
                    }
                    else
                    {
                        string value = propertyInfo.GetValue(entityModel).ToString();
                        template.Set(name, value);
                    }

                }

                var result = await template.RenderAsync();

                bool res = false;
                List<string> content = new List<string>();
                using StreamWriter sw = new StreamWriter($"{doOutPath}/{entityModel.ClassName}.java");
                {

                    await sw.WriteLineAsync(result);

                    return res;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }
    }
}
