using System.Text;
using System.Windows;
using SubsrciptionSystem.Models;

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

            if (string.IsNullOrWhiteSpace(txtFName.Text) ||
                    string.IsNullOrWhiteSpace(txtLName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtcompany.Text) ||
                    string.IsNullOrWhiteSpace(txtStorage.Text) ||
                    string.IsNullOrWhiteSpace(txtpassword.Password))
            {
                MessageBox.Show("Please Enter all details.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // positive and a number

            if (!int.TryParse(txtStorage.Text, out int storageGb) || storageGb <= 0)
            {
                MessageBox.Show(
                    "Storage must be a positive number.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            string firstName = txtFName.Text;
            string lastName = txtLName.Text;
            string email = txtEmail.Text;
            string company = txtcompany.Text;
            string storage = txtStorage.Text;
            string password = txtpassword.Password;


            using (var db = new AppDbContext())
            {
                var emailEntity = new EmailEntity
                {
                    FirstName = txtFName.Text.Trim(),
                    LastName = txtLName.Text.Trim(),
                    EmailAddress = txtEmail.Text.Trim(),
                    Company = txtcompany.Text.Trim(),
                    Password = txtpassword.Password.Trim(),
                    StorageGB = storageGb,
                };

                db.EmailUser.Add(emailEntity);
                db.SaveChanges();
            }


            MessageBox.Show(
                "Data saved successfully!",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }
    }
}
