using SchoolLogo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolLogo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            MyFrame.NavigationService.Navigate(new ServicePage());

            MyFrame.Navigated += MyFrame_Navigated;
        }

        private void MyFrame_Navigated(object sender, NavigationEventArgs e)
        {
            TitleTb.Text = (MyFrame.Content as Page).Title;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoBack)
                MyFrame.GoBack();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
