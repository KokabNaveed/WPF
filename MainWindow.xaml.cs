using System.Linq;
using System.Windows;

namespace SubsrciptionSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EmailMenu_Click(object sender, RoutedEventArgs e)
        {
            Email emailWindow = new Email();
            emailWindow.Show();
            this.Close();
        }

        private void DomainMenu_Click(object sender, RoutedEventArgs e)
        {
            Domain domainWindow = new Domain();
            domainWindow.Show();
            this.Close();
        }

        private void SoftwareMenu_Click(object sender, RoutedEventArgs e)
        {
           Software softwareWindow = new Software();    
            softwareWindow.Show();
            this.Close();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void SoftwareData_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                dataGrid.ItemsSource = db.Softwares.ToList();
            }

            dataGrid.Visibility = Visibility.Visible;
        }
        private void EmailData_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                dataGrid.ItemsSource = db.EmailUser.ToList();
            }

            dataGrid.Visibility = Visibility.Visible;
        }
        private void DomainData_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {

                dataGrid.ItemsSource = db.Domains.ToList();
            }

            dataGrid.Visibility = Visibility.Visible;
        }

    }
}
