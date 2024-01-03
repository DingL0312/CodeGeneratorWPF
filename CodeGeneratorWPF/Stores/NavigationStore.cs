using System;
using CodeGeneratorWPF.ViewModel;

/*
 * @Description:
 * @Author: DLiang
 * @Date: 2023/12/26 15:03:44
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
 */

namespace CodeGeneratorWPF.Stores
{
    public class NavigationStore
    {
        private BaseViewModel curretnBaseViewModel;

        public BaseViewModel CurrentBaseViewModel
        {
            get =>curretnBaseViewModel;
            set
            {
                curretnBaseViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
