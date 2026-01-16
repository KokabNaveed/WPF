using System;
using System.Windows;
using SubsrciptionSystem.Models;

namespace SubsrciptionSystem
{
    /// <summary>
    /// Interaction logic for Domain.xaml
    /// </summary>
    public partial class Domain : Window
    {

        public Domain()
        {
            InitializeComponent();
        }

        private void tglAutoRenew_Checked(object sender, RoutedEventArgs e)
        {
            if (dpRegisteredDate.SelectedDate != null)
            {
                // Auto-set renewal date to 1 year later
                dpRenewalDate.SelectedDate = dpRegisteredDate.SelectedDate.Value.AddYears(1);
            }

            txtAutoRenewStatus.Text = "ON";
            // Lock renewal date when auto-renew is ON
            dpRenewalDate.IsEnabled = false;

        }

        private void tglAutoRenew_Unchecked(object sender, RoutedEventArgs e)
        {
            txtAutoRenewStatus.Text = "OFF";
            dpRenewalDate.IsEnabled = true;
        }



        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            bool isAutoRenew = tglAutoRenew.IsChecked == true;

            if (dpRegisteredDate.SelectedDate == null ||
                string.IsNullOrWhiteSpace(txtDomainName.Text) ||
                string.IsNullOrWhiteSpace(txtdomainRegistrar.Text) ||
                string.IsNullOrWhiteSpace(txtdns1.Text) ||
                string.IsNullOrWhiteSpace(txtdns2.Text) ||
                string.IsNullOrWhiteSpace(txtamount.Text))
            {
                MessageBox.Show("Please Enter all details.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime registeredDate = dpRegisteredDate.SelectedDate.Value;

            // AUTO RENEW ON => system controls renewal date
            if (isAutoRenew)
            {
                dpRenewalDate.SelectedDate = registeredDate.AddYears(1);
            }

            if (dpRenewalDate.SelectedDate == null)
            {
                MessageBox.Show("Please select renewal date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime renewalDate = dpRenewalDate.SelectedDate.Value;

            if (renewalDate <= registeredDate)
            {
                MessageBox.Show("Renewal date must be later than registered date.", "Invalid Date", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(txtamount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show(
                    "Please enter a valid amount.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            using (var db = new AppDbContext())
            {
                var domain = new DomainEntity
                {
                    DomainName = txtDomainName.Text.Trim(),
                    Registrar = txtdomainRegistrar.Text.Trim(),
                    RegisteredDate = dpRegisteredDate.SelectedDate.Value,
                    RenewalDate = dpRenewalDate.SelectedDate.Value,
                    NameServer1 = txtdns1.Text.Trim(),
                    NameServer2 = txtdns2.Text.Trim(),
                    Amount = amount,
                    AutoRenew = isAutoRenew
                };

                db.Domains.Add(domain);
                db.SaveChanges();
            }


            MessageBox.Show("Domain saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            if (!isAutoRenew)
            {
                MessageBox.Show("Auto Renew is OFF. Please renew before expiry.", "Reminder", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

    }
}
