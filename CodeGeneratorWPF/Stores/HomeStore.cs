using CodeGeneratorWPF.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * @Description: 
 * @Author: DLiang
 * @Date: 2024/1/6 10:25:43propDic
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
*/

namespace CodeGeneratorWPF.Stores
{
    public class HomeStore
    {
        /// <summary>
        /// 项目路径
        /// </summary>
        public string? ProjectPath { get; set; }

        /// <summary>
        /// Do路径
        /// </summary>
        public string? DoPath { get; set; }

        /// <summary>
        /// Dto路径
        /// </summary>
        public string? DtoPath { get; set; }

        /// <summary>
        /// Vo路径
        /// </summary>
        public string? VoPath { get; set; }

        /// <summary>
        /// Api路径
        /// </summary>
        public string? ApiPath { get; set; }

        /// <summary>
        /// Controller路径
        /// </summary>
        public string? ControllerPath { get; set; }
        /// <summary>
        /// Mapper路径
        /// </summary>
        public string? MapperPath { get; set; }

        /// <summary>
        /// MapStruct路径
        /// </summary>
        public string? MapStructPath { get; set; }
        /// <summary>
        /// Service路径
        /// </summary>
        public string? ServicePath { get; set; }

        /// <summary>
        /// ServiceImpl路径
        /// </summary>
        public string? ServiceImplPath { get; set; }


        /// <summary>
        /// 表集合
        /// </summary>
        public ObservableCollection<TableModel> Tables { get; set; }

        /// <summary>
        /// 选中的表
        /// </summary>
        public TableModel SelectedTable { get; set; }
    }
}
