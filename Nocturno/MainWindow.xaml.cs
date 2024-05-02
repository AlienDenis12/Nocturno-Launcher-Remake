using System.Text;
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
    public partial class MainWindow : FluentWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            NavFrame.Content = new Login();
        }

        private void UsernameTextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 0.4,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0.3))
            };

            NameTextBlock.BeginAnimation(OpacityProperty, opacityAnimation);
        }

        private void UsernameTextBlock_Leave(object sender, MouseEventArgs e)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0.4,
                Duration = new Duration(TimeSpan.FromSeconds(0.3))
            };

            NameTextBlock.BeginAnimation(OpacityProperty, opacityAnimation);
        }

    }
}