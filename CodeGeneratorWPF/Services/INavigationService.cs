using CodeGeneratorWPF.ViewModel;

/*
 * @Description:
 * @Author: DLiang
 * @Date: 2023/12/26 15:00:09
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
 */
namespace CodeGeneratorWPF
{
   public interface INavigationService
   {
       void Navigate<TViewModel>() where TViewModel : BaseViewModel;
       TViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel;
   }
}
