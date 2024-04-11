using System;
using System.Windows;
using CodeGeneratorWPF.Services;
using CodeGeneratorWPF.Services.Impl;
using CodeGeneratorWPF.Stores;
using CodeGeneratorWPF.ViewModel;
using CodeGeneratorWPF.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGeneratorWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly IServiceProvider _serviceProvider;
        public new static App Current => (App)Application.Current;
        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<HomeStore>();

            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>()));

            services.AddTransient<ConnViewModel>();
            services.AddTransient<SettingViewModel>();
            services.AddTransient<HomeViewModel>();

            services.AddSingleton<ISqlService, SqlService>();
            services.AddSingleton<IGeneratorService, GeneratorService>();


            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService navigationService = _serviceProvider.GetRequiredService<INavigationService>();
            navigationService.Navigate<HomeViewModel>();

            base.OnStartup(e);
            MainWindow mw = _serviceProvider.GetRequiredService<MainWindow>();
            mw.Show();
        }
    }
}
