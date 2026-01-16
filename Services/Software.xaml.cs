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
            if (cmbPlan.SelectedItem is ComboBoxItem selected)
            {
                switch (selected.Content.ToString())
                {
                    case "Free":
                        txtamount.Text = "0";
                        dpRenewalDate.IsEnabled = false;
                        break;

                    case "Monthly":
                        txtamount.Text = "10";
                        dpRenewalDate.IsEnabled = true;
                        break;

                    case "Yearly":
                        txtamount.Text = "100";
                        dpRenewalDate.IsEnabled = true;
                        break;
                }
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (dpSubscribeddate.SelectedDate == null ||dpRenewalDate.SelectedDate == null  || txtemail.Text == null || txtSoftwareName.Text == null || cmbCategory.SelectedItem == null || cmbPlan.SelectedItem == null)
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

            DateTime registeredDate = dpSubscribeddate.SelectedDate.Value;
            DateTime renewalDate = dpRenewalDate.SelectedDate.Value;

            if (renewalDate <= registeredDate)
            {
                MessageBox.Show("Renewal date must be later than registered date.",
                                "Invalid Date",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
