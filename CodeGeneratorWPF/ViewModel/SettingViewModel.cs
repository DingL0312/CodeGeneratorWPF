using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

/*
 * @Description:
 * @Author: DLiang
 * @Date: 2024/1/6 9:40:55
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
 */

namespace CodeGeneratorWPF.ViewModel
{
    public partial class SettingViewModel :BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public SettingViewModel()
        {
            
        }

        public SettingViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        [RelayCommand]
        void BackClick()
        {
            _navigationService.Navigate<HomeViewModel>();
        }
    }
}
