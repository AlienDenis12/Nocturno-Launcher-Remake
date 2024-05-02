using System;
using System.Collections.Generic;
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

namespace Nocturno
{
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();

            InfoFrame.Content = new Info();

            string[] imagePaths = new string[]
            {
                "../Assets/Legendary_constructor_limited_edition_skin.png",
                "../Assets/Legendary_outlander_founders_pack.png",
                "../Assets/Nocturno.exe.00743a78_0012b36d.png",
                "../Assets/Outlander.png",
                "../Assets/6pcx461s00o01-ezgif.com-webp-to-png-converter.png"
            };

            Random rnd = new Random();
            int randomIndex = rnd.Next(imagePaths.Length);

            HeroImage.Source = new BitmapImage(new Uri(imagePaths[randomIndex], UriKind.Relative));
        }

        private void DiscordButton_Clicked(object sender, RoutedEventArgs e)
        {
            string url = "https://discord.gg/TTQu3uan";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void SettingsButton_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.NavFrame != null)
            {
                mainWindow.NavFrame.Navigate(new Settings());
            }
            else
            {
                System.Windows.MessageBox.Show("MainFrame is null. Navigation failed.");
            }
        }

        private void LaunchButton_Clicked(object sender, RoutedEventArgs e)
        {
            Fortnite.Launch(Properties.Settings.Default.gamePath, Properties.Settings.Default.Username + "@.", "sigmarizzler");

            LaunchStatus.Visibility = Visibility.Hidden;
            LaunchStatus1.Visibility = Visibility.Visible;

            LaunchButton.MouseOverBackground = new SolidColorBrush(Color.FromRgb(48, 50, 60));
            LaunchButton.Background = new SolidColorBrush(Color.FromRgb(48, 50, 60));

            LaunchButton.IsHitTestVisible = false;

            LaunchStatus1.Visibility = Visibility.Hidden;
            LaunchStatus2.Visibility = Visibility.Visible;
        }

        private void LaunchButton_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0.8,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0.3))
            };

            LaunchButton.BeginAnimation(OpacityProperty, opacityAnimation);
        }

        private void LaunchButton_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0.8,
                Duration = new Duration(TimeSpan.FromSeconds(0.3))
            };

            LaunchButton.BeginAnimation(OpacityProperty, opacityAnimation);
        }
    }
}
 