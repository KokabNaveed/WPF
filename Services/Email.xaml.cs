using System.Text;
using System.Windows;
using SubsrciptionSystem.Models;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows.Input;


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

        private void OnlyLetters(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[A-Za-z]+$");
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFName.Text.Trim();
            string lastName = txtLName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string company = txtcompany.Text.Trim();
            string password = txtpassword.Password.Trim();

            if (string.IsNullOrWhiteSpace(firstName) ||
                    string.IsNullOrWhiteSpace(lastName) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(company) ||
                    string.IsNullOrWhiteSpace(txtStorage.Text) ||
                    string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please Enter all details.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // positive and a number

            if (!int.TryParse(txtStorage.Text, out int storageGb) || storageGb <= 0)
            {
                MessageBox.Show("Storage must be a positive number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);         
                return;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show(
                    "Please enter a valid email address.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }


            if (password.Length < 6)
            {
                MessageBox.Show(
                    "Password must be at least 6 characters long.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            using (var db = new AppDbContext())
            {
                bool emailExists = db.EmailUser.Any(x => x.EmailAddress == email);
                if (emailExists)
                {
                    MessageBox.Show(
                        "This email is already registered.",
                        "Validation Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                var emailEntity = new EmailEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = email,
                    Company = company,
                    Password = password,
                    StorageGB = storageGb
                };

                db.EmailUser.Add(emailEntity);
                db.SaveChanges();
            }



            MessageBox.Show( "Data saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information );

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }
    }
}
