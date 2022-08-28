using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;

namespace WorkTimer
{
    class App : Application
    {
        [System.STAThreadAttribute()]
        public static void Main()
        {
            App app = new App();
            app.Run();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Views.Timer window = new Views.Timer();
            window.DataContext = new ViewModels.TimerViewModel();
            window.InitializeComponent();

            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    this.MainWindow.WindowState = WindowState.Normal; 
                    this.MainWindow.Activate();
                }
                );
            };

            window.Show();
        }

    }
}
