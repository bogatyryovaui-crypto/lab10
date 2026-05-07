using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace lab10
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
     public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddSingleton<IDialogService, DialogService>();
            services.AddTransient<MainViewModel>();

            services.AddSingleton<MainWindow>(sp =>
            {
                var window = new MainWindow();
                window.DataContext = sp.GetRequiredService<MainViewModel>();
                return window;
            });

            var provider = services.BuildServiceProvider();

            var mainWindow = provider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
