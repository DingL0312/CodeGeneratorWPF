using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CodeGeneratorWPF.Helper;
using CodeGeneratorWPF.Models;
using CodeGeneratorWPF.Services;
using CodeGeneratorWPF.Services.Impl;
using CodeGeneratorWPF.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MySqlConnector;

/*
 * @Description:
 * @Author: DLiang
 * @Date: 2023/12/22 16:46:58
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
 */

namespace CodeGeneratorWPF.ViewModel
{
    public partial class MainWindowViewModel : BaseViewModel

    {
        private readonly ISqlService _sqlService;

        public MainWindowViewModel()
        {

        }
        public MainWindowViewModel(ISqlService sqlService)
        {
                tables = new();
            _sqlService = sqlService;

            ConnStr = "server=localhost;port=3308;database=sy_saas_main;user=root;password=123456;";
        }

        [ObservableProperty]
        private ObservableCollection<TableModel> tables;

        [ObservableProperty]
        private TableModel selectedTable;
        partial void OnSelectedTableChanged(TableModel selectedTable)
        {
            TableSelectedChanged();
        }

        [ObservableProperty]
        private string connStr;


        [ObservableProperty]
        private ObservableCollection<ColumnModel> columns;

        [RelayCommand]
        void ConnClick()
        {
            if (string.IsNullOrEmpty(connStr))
            {
                MessageBox.Show("连接字符串不能为空");
                return;
            }
            try
            {
                MysqlHelper.ConnStr = connStr;

                Tables = _sqlService.GetTables();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void TableSelectedChanged()
        {
            if (selectedTable != null)
            {
                Columns = _sqlService.GetColumns(selectedTable.Name);
            }
        }
    }
}
