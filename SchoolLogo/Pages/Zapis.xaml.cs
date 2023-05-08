using SchoolLogo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SchoolLogo.Pages
{
    /// <summary>
    /// Логика взаимодействия для Zapis.xaml
    /// </summary>
    public partial class Zapis : Page
    {
        private Service _contextService;

        public Zapis(Service service)
        {
            InitializeComponent();
            _contextService = service;
            TBService.DataContext = _contextService;
            CBClient.ItemsSource = App.db.Client.ToList();
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {

            string errorMessage = "";
            string timeBuff = $"{TBTimeHour.Text}:{TBTimeMinute.Text}";
            if (CBClient.SelectedItem == null)
            {
                errorMessage += "Выберите клиента\n";
            }

            if (TBDate.SelectedDate == null
                || TBDate.SelectedDate.Value.Date
                < DateTime.Now.AddHours(DateTime.Now.Hour + 1).Date)
            {
                errorMessage += "Введите корректную дату\n";
            }

            if (TimeSpan.TryParse(timeBuff, out TimeSpan timeSpan) == false)
            {
                errorMessage += "Введите корректное время\n";
            }

            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                MessageBox.Show(errorMessage);
                return;
            }

            var date = TBDate.DisplayDate;

            App.db.ClientService.Add(new ClientService
            {
                Service = _contextService,
                Client = (Client)CBClient.SelectedItem,
                StartTime = new DateTime
                (
                    date.Year,
                    date.Month,
                    date.Day,
                    int.Parse(TBTimeHour.Text.ToString()),
                    int.Parse(TBTimeMinute.Text.ToString()),
                    0
                )

            });

            App.db.SaveChanges();
            NavigationService.Navigate(new ServicePage());
        }

        private void TBTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
                return;
            }


            if (textBox == TBTimeMinute
                && int.Parse($"{textBox.Text}{e.Text}") >= 60)
            {
                e.Handled = true;
            }

            if (textBox == TBTimeHour
                && int.Parse($"{textBox.Text}{e.Text}") > 16)
            {
                e.Handled = true;
            }
        }

        private void TBTimeMinute_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBTimeMinute.Text == "")
            {
                TBTimeMinute.Text = "00";
            }
            else if (int.Parse(TBTimeMinute.Text) < 10 && int.Parse(TBTimeMinute.Text) != 0)
                TBTimeMinute.Text = "0" + TBTimeMinute.Text;

        }

        private void TBTimeHour_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TBTimeHour.Text == "")
            {
                TBTimeHour.Text = "00";
            }
            else if (int.Parse(TBTimeHour.Text) < 10 && int.Parse(TBTimeHour.Text) != 0)
                TBTimeHour.Text = "0" + TBTimeHour.Text;
        }

        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
