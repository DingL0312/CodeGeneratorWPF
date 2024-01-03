using System;
using System.Windows;
using CodeGeneratorWPF.Services;
using CodeGeneratorWPF.Services.Impl;
using CodeGeneratorWPF.Stores;
using CodeGeneratorWPF.ViewModel;
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

            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>()));

            services.AddTransient<ConnViewModel>();

            services.AddSingleton<ISqlService, SqlService>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow mw = _serviceProvider.GetRequiredService<MainWindow>();
            mw.Show();
        }
    }
}
