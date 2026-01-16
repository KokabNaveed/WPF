using System;
using System.Windows;
using System.Windows.Controls;
using SubsrciptionSystem.Models;

namespace SubsrciptionSystem
{
    /// <summary>
    /// Interaction logic for Software.xaml
    /// </summary>
    public partial class Software : Window
    {
        public Software()
        {
            InitializeComponent();
        }

        private void cmbPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPlan.SelectedItem is ComboBoxItem selected &&
                dpSubscribeddate.SelectedDate != null)
            {
                DateTime startDate = dpSubscribeddate.SelectedDate.Value;

                switch (selected.Content.ToString())
                {
                    case "Free":
                        txtamount.Text = "0";
                        txtamount.IsEnabled = false;
                        dpRenewalDate.SelectedDate = null;
                        dpRenewalDate.IsEnabled = false;
                        break;

                    case "Monthly":
                        txtamount.Text = "10";
                        txtamount.IsEnabled = false;
                        dpRenewalDate.IsEnabled = false;
                        dpRenewalDate.SelectedDate = startDate.AddMonths(1);
                        break;

                    case "Yearly":
                        txtamount.Text = "100";
                        txtamount.IsEnabled = false;
                        dpRenewalDate.IsEnabled = false;
                        dpRenewalDate.SelectedDate = startDate.AddYears(1);
                        break;
                }
            }
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if ( dpSubscribeddate.SelectedDate == null ||
                string.IsNullOrWhiteSpace(txtemail.Text ) || 
                string.IsNullOrWhiteSpace(txtSoftwareName.Text) )
            {
                MessageBox.Show("Please Enter all details.",
                                "Validation Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            if (cmbPlan.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a Plan Type.",
                                "Validation Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            if (cmbCategory.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a Category.",
                                "Validation Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            string plan = cmbPlan.Text;

            if (plan != "Free" && dpRenewalDate.SelectedDate == null)
            {
                MessageBox.Show("Renewal date is required for paid plans.",
                                "Validation Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
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

            DateTime registeredDate = dpSubscribeddate.SelectedDate.Value;

            if (plan == "Monthly")
            {
                dpRenewalDate.SelectedDate = registeredDate.AddMonths(1);
            }

            else if (plan == "Yearly")
            {
                dpRenewalDate.SelectedDate = registeredDate.AddYears(1);

            }
            else
            {
                dpRenewalDate.SelectedDate = null;
            }
            DateTime renewalDate = dpRenewalDate.SelectedDate.Value;


            using (var db = new AppDbContext())
            {
                var software = new SoftwareEntity
                {
                    SoftwareName = txtSoftwareName.Text.Trim(),
                    Email = txtemail.Text.Trim(),
                    SubscribedDate = dpSubscribeddate.SelectedDate.Value,
                    RenewalDate = dpRenewalDate.SelectedDate.Value,
                    Category = cmbCategory.Text.Trim(),
                    PlanType = cmbPlan.Text.Trim(),
                    Amount = amount,
                };

                db.Softwares.Add(software);
                db.SaveChanges();
            }


            MessageBox.Show("Software Subscription added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);


            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
