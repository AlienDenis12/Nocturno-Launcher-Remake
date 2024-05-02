using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace Nocturno
{
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();

            usernametextbox.Text = Properties.Settings.Default.Username;
            gamepathtextbox.Text = Properties.Settings.Default.gamePath;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.NavFrame != null)
            {
                mainWindow.NavFrame.Navigate(new Main());
            }
            else
            {
                System.Windows.MessageBox.Show("MainFrame is null. Navigation failed.");
            }

        }
        private void LogoutButton_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0.4,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0.3))
            };

            LogoutButton.BeginAnimation(OpacityProperty, opacityAnimation);
        }

        private void LogoutButton_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0.4,
                Duration = new Duration(TimeSpan.FromSeconds(0.3))
            };

            LogoutButton.BeginAnimation(OpacityProperty, opacityAnimation);
        }

        private void LaunchHide_Click(object sender, RoutedEventArgs e)
        {
            if (LauncherHideText.Text == "ON")
            {
                LauncherHideText.Text = "OFF";
                LauncherHideButton.MouseOverBackground = new SolidColorBrush(Color.FromRgb(33, 33, 33));
                LauncherHideButton.Background = new SolidColorBrush(Color.FromRgb(33, 33, 33));
                return;
            }
            else
            {
                LauncherHideText.Text = "ON";
                LauncherHideButton.MouseOverBackground = new SolidColorBrush(Color.FromRgb(6, 225, 6));
                LauncherHideButton.Background = new SolidColorBrush(Color.FromRgb(6, 225, 6));
            }

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.NavFrame != null)
            {
                mainWindow.NavFrame.Navigate(new Login());
            }
            else
            {
                System.Windows.MessageBox.Show("MainFrame is null. Navigation failed.");
            }
        }

        private void PathButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            string Appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Nocturno";
             
            Process.Start("explorer.exe", Appdata);
        }

        private void PathButton_Click(object sender, RoutedEventArgs e)
        {
           var openFileDialog = new OpenFolderDialog
           {
              Title = "Select a Folder"
           };

           bool? result = openFileDialog.ShowDialog();
           bool executableExists = System.IO.File.Exists(System.IO.Path.Combine(openFileDialog.FolderName, "FortniteGame\\Binaries\\Win64\\FortniteClient-Win64-Shipping.exe"));

           if (executableExists)
           {
              Properties.Settings.Default.gamePath = openFileDialog.FolderName;
              Properties.Settings.Default.Save();
              gamepathtextbox.Text = openFileDialog.FolderName;

           }
            
        }

        private void usernametextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.Username = usernametextbox.Text;
            Properties.Settings.Default.Save();
        }
    }
}
