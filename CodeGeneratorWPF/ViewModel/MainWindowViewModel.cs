using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using CodeGeneratorWPF.Helper;
using CodeGeneratorWPF.Models;
using CodeGeneratorWPF.Models.TemplateModel;
using CodeGeneratorWPF.Services;
using CodeGeneratorWPF.Services.Impl;
using CodeGeneratorWPF.Stores;
using CodeGeneratorWPF.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JinianNet.JNTemplate;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MessageBox = System.Windows.MessageBox;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

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
        private readonly NavigationStore _navigationStore;

        [ObservableProperty]
        private BaseViewModel? currentViewModel;

        public MainWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            CurrentViewModel = _navigationStore.CurrentBaseViewModel;
            _navigationStore.CurrentViewModelChanged += () =>
            {
                CurrentViewModel = _navigationStore.CurrentBaseViewModel;
            };
        }

    }
}
