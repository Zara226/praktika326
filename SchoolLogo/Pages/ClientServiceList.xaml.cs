using SchoolLogo.Model;
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
using System.Windows.Threading;

namespace SchoolLogo.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientServiceList.xaml
    /// </summary>
    public partial class ClientServiceList : Page
    {
        public ClientServiceList()
        {
            InitializeComponent();
            Refresh();
            var timerRefresh = new DispatcherTimer();
            timerRefresh.Tick += new EventHandler(TimerRefresh_Tick);
            timerRefresh.Interval = new TimeSpan(0, 0, 30);
            timerRefresh.Start();
        }

        private void Refresh()
        {
            DateTime tomorrow = DateTime.Today.AddDays(1).AddHours(DateTime.Now.Hour);
            IEnumerable<ClientService> buffer = App.db.ClientService.Where(
                x => x.StartTime >= DateTime.Now
                && x.StartTime <= tomorrow
                ).ToList().OrderBy(x => x.StartTime);

            if (buffer.Count() == 0)
                LVService.Visibility = Visibility.Hidden;
            else
                LVService.Visibility = Visibility.Visible;

            LVService.ItemsSource = buffer.ToList();
        }

        private void TimerRefresh_Tick(object sender, EventArgs e)
        {
            Refresh();
            CommandManager.InvalidateRequerySuggested();
        }
    }
}