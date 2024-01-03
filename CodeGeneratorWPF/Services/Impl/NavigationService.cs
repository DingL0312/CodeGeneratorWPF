using Microsoft.Extensions.DependencyInjection;
using CodeGeneratorWPF.Stores;
using CodeGeneratorWPF.ViewModel;

/*
 * @Description:
 * @Author: DLiang
 * @Date: 2023/12/26 15:01:58
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
 */

namespace CodeGeneratorWPF
{
    public class NavigationService:INavigationService
    {
        private readonly NavigationStore _navigationStore;


        public NavigationService(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }
        public void Navigate<TViewModel>() where TViewModel : BaseViewModel
        {
            _navigationStore.CurrentBaseViewModel = App.Current._serviceProvider.GetRequiredService<TViewModel>();
        }

        public TViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel
        {
          return  (TViewModel)_navigationStore.CurrentBaseViewModel;
        }
    }
}
