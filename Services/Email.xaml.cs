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
using System.Windows.Shapes;

namespace SubsrciptionSystem
{
    /// <summary>
    /// Interaction logic for Email.xaml
    /// </summary>
    public partial class Email : Window
    {
        public Email()
        {
            InitializeComponent();
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFName.Text;
            string lastName = txtLName.Text;
            string email = txtEmail.Text;
            string company = txtcompany.Text;
            string storage = txtStorage.Text;
            string password = txtpassword.Password;

            StringBuilder details = new StringBuilder();
            details.AppendLine("Submitted Details:");
            details.AppendLine($"First Name: {firstName}");
            details.AppendLine($"Last Name: {lastName}");
            details.AppendLine($"Email: {email}");
            details.AppendLine($"Company: {company}");
            details.AppendLine($"Storage (GB): {storage}");
            details.AppendLine($"Password: {password}");

            MessageBox.Show(
                details.ToString(),
                "Form Submitted",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }
    }
}
