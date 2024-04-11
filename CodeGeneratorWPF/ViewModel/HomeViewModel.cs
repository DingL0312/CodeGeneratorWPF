using CodeGeneratorWPF.Models.TemplateModel;
using CodeGeneratorWPF.Models;
using CodeGeneratorWPF.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JinianNet.JNTemplate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeGeneratorWPF.Helper;
using CodeGeneratorWPF.Stores;

/*
 * @Description:
 * @Author: DLiang
 * @Date: 2024/1/6 9:55:50
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
 */

namespace CodeGeneratorWPF.ViewModel
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly ISqlService _sqlService;
        private readonly IGeneratorService _generatorService;
        private readonly INavigationService _navigationService;
        private readonly HomeStore _homeStore;

        public HomeViewModel()
        {

        }
        public HomeViewModel(ISqlService sqlService, IGeneratorService generatorService, INavigationService navigationService,HomeStore homeStore)
        {
            tables = new();
            _sqlService = sqlService;
            _generatorService = generatorService;
            _navigationService = navigationService;
            _homeStore = homeStore;
            ConnStr = "server=localhost;port=3308;database=sy_saas_main;user=root;password=123456;";
            InitData();
        }

        private string[] filterDirNames = new[]
        {
            ".git",
            "target",
            "resources",
            "test"
        };

        private string[] targetDirNames = new string[9]
        {
            "api",
            "entity",
            "dto",
            "vo",
            "controller",
            "mapper",
            "mapstruct",
            "service",
            "impl"
        };

        #region 属性

        /// <summary>
        /// 项目路径
        /// </summary>
        [ObservableProperty]
        private string _projectPath;

        /// <summary>
        /// Do路径
        /// </summary>
        [ObservableProperty]
        private string _doPath;

        /// <summary>
        /// Dto路径
        /// </summary>
        [ObservableProperty]
        private string _dtoPath;

        /// <summary>
        /// Vo路径
        /// </summary>
        [ObservableProperty]
        private string _voPath;

        /// <summary>
        /// Api路径
        /// </summary>
        [ObservableProperty]
        private string _apiPath;

        /// <summary>
        /// Controller路径
        /// </summary>
        [ObservableProperty]
        private string _controllerPath;
        /// <summary>
        /// Mapper路径
        /// </summary>
        [ObservableProperty]
        private string _mapperPath;

        /// <summary>
        /// MapStruct路径
        /// </summary>
        [ObservableProperty]
        private string _mapStructPath;
        /// <summary>
        /// Service路径
        /// </summary>
        [ObservableProperty]
        private string _servicePath;

        /// <summary>
        /// ServiceImpl路径
        /// </summary>
        [ObservableProperty]
        private string _serviceImplPath;


        /// <summary>
        /// 表集合
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<TableModel> tables;

        /// <summary>
        /// 选中的表
        /// </summary>
        [ObservableProperty]
        private TableModel selectedTable;
        partial void OnSelectedTableChanged(TableModel selectedTable)
        {
            if (selectedTable != null)
            {
                Columns = _sqlService.GetColumns(selectedTable.TableName);
            }
        }
        /// <summary>
        /// 连接字符串
        /// </summary>
        [ObservableProperty]
        private string connStr;

        /// <summary>
        /// 列集合
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<ColumnModel> columns;

        #endregion




        #region 命令

        /// <summary>
        /// 连接数据库
        /// </summary>
        [RelayCommand]
        void ConnClick()
        {
            if (string.IsNullOrEmpty(ConnStr))
            {
                MessageBox.Show("连接字符串不能为空");
                return;
            }
            try
            {
                MysqlHelper.ConnStr = ConnStr;

                var tableSchema = GetTableSchema();
                if (string.IsNullOrEmpty(tableSchema))
                {
                    MessageBox.Show("数据库名称获取失败");
                    return;
                }
                Tables = _sqlService.GetTables(tableSchema);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        [RelayCommand]
        void ProjectPathSelect()
        {
            ProjectPath = SelectDir();
            if (!string.IsNullOrEmpty(ProjectPath))
            {
                List<string> dirList = GetAllChildDir(ProjectPath);
                UpdatePathValue(dirList);
            }

        }

        [RelayCommand]
        void DoPathSelect()
        {
            DoPath = SelectDir();
            UpdateStoreData();
        }
        [RelayCommand]
        void DtoPathSelect()
        {
            DtoPath = SelectDir();
            UpdateStoreData();
        }
        [RelayCommand]
        void VoPathSelect()
        {
            VoPath = SelectDir();
            UpdateStoreData();
        }
        [RelayCommand]
        void ApiPathSelect()
        {
            ApiPath = SelectDir();
            UpdateStoreData();
        }
        [RelayCommand]
        void ControllerPathSelect()
        {
            ControllerPath = SelectDir();
            UpdateStoreData();
        }
        [RelayCommand]
        void MapperPathSelect()
        {
            MapperPath = SelectDir();
            UpdateStoreData();
        }
        [RelayCommand]
        void MapStructPathSelect()
        {
            MapStructPath = SelectDir();
            UpdateStoreData();
        }
        [RelayCommand]
        void ServicePathSelect()
        {
            ServicePath = SelectDir();
            UpdateStoreData();
        }
        [RelayCommand]
        void ServiceImplPathSelect()
        {
            ServiceImplPath = SelectDir();
            UpdateStoreData();
        }

        [RelayCommand]
        void GeneratorClick()
        {

            FileInfo file = new FileInfo("./resources/codetemplate/dotemplate.java");
            if (file.Exists)
            {

                EntityModel entityModel = new();
                entityModel.ClassName = SelectedTable.TableName;
                entityModel.list = Columns.ToList();
                entityModel.Author = "DLiang";
                entityModel.Date = DateTime.Now.ToShortDateString();
                entityModel.BasePackage = "cn.sanyi.education.tenant";
                entityModel.Comment = SelectedTable.TableComment;
                _generatorService.GeneratorDo(entityModel);
            }
            else
            {
                Console.WriteLine("文件不存在");
            }
        }
        [RelayCommand]
        void SettingClick()
        {
            _navigationService.Navigate<SettingViewModel>();
            UpdateStoreData();
        }

        #endregion


        string SelectDir()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }

            return string.Empty;
        }

        List<string> GetAllChildDir(string targetDir)
        {
            List<string> dirList = new();
            if (!Directory.Exists(targetDir))
            {
                return dirList;
            }

            string dirName;
            string[] childs = Directory.GetDirectories(targetDir);
            foreach (string child in childs)
            {
                dirName = GetDirectoryName(child);
                if (filterDirNames.Contains(dirName))
                {
                    continue;
                }
                if (targetDirNames.Contains(dirName))
                {
                    dirList.Add(@"{项目路径}\" + Path.GetRelativePath(ProjectPath, child));
                }
                dirList.AddRange(GetAllChildDir(child));
            }

            return dirList;
        }

        /// <summary>
        /// 获取数据库名
        /// </summary>
        /// <returns>string</returns>
        private string GetTableSchema()
        {
            if (string.IsNullOrEmpty(ConnStr)) return string.Empty;

            var strArr = ConnStr.Split(";");

            var dataBase = strArr.FirstOrDefault(x => x.ToLower().Contains("database"));
            if (string.IsNullOrEmpty(dataBase)) return string.Empty;
            var dbArr = dataBase.Split("=");
            if (dbArr.Length == 2)
            {
                return dbArr[1].Trim();
            }

            return string.Empty;
        }

        private string GetDirectoryName(string path)
        {
            string[] pathArr = path.Split(@"\");

            return pathArr.Last();
        }


        void UpdatePathValue(List<string> dirList)
        {
            if (dirList.Count == 0) return;

            ApiPath = dirList.FirstOrDefault(x => x.EndsWith("api"));
            ControllerPath = dirList.FirstOrDefault(x => x.EndsWith("controller"));
            DtoPath = dirList.FirstOrDefault(x => x.EndsWith("dto"));
            VoPath = dirList.FirstOrDefault(x => x.EndsWith("vo"));
            DoPath = dirList.FirstOrDefault(x => x.EndsWith("entity"));
            MapperPath = dirList.FirstOrDefault(x => x.EndsWith("mapper"));
            MapStructPath = dirList.FirstOrDefault(x => x.EndsWith("mapstruct"));
            ServicePath = dirList.FirstOrDefault(x => x.EndsWith("service"));
            ServiceImplPath = dirList.FirstOrDefault(x => x.EndsWith("impl"));
            UpdateStoreData();
        }

        void UpdateStoreData()
        {
            _homeStore.ApiPath=ApiPath;
            _homeStore.ControllerPath= ControllerPath;
            _homeStore.ServicePath= ServicePath;
            _homeStore.ServiceImplPath= ServiceImplPath;
            _homeStore.DtoPath= DtoPath;
            _homeStore.DoPath= DoPath;
            _homeStore.VoPath= VoPath;
            _homeStore.MapperPath= MapperPath;
            _homeStore.MapStructPath= MapStructPath;
            _homeStore.ProjectPath= ProjectPath;
            _homeStore.Tables= Tables;
            _homeStore.SelectedTable= SelectedTable;
        }

        void InitData()
        {
            ApiPath = _homeStore.ApiPath;
            ControllerPath = _homeStore.ControllerPath;
            ServicePath = _homeStore.ServicePath;
            ServiceImplPath = _homeStore.ServiceImplPath;
            DtoPath = _homeStore.DtoPath;
            DoPath = _homeStore.DoPath;
            VoPath = _homeStore.VoPath;
            MapperPath = _homeStore.MapperPath;
            MapStructPath = _homeStore.MapStructPath;
            ProjectPath = _homeStore.ProjectPath;
            Tables = _homeStore.Tables;
            SelectedTable = _homeStore.SelectedTable;
        }
    }
}
