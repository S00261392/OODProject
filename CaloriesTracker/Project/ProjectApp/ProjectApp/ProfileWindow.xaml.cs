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

namespace ProjectApp
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    /// 
    public partial class ProfileWindow : Window
    {
        private double MB = 0;
        


        public ProfileWindow()
        {
            InitializeComponent();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {

            MainWindow main = this.Owner as MainWindow;

            main.LeftCalories[0] = MB;
            main.LeftCalories[1] = MB;

            main.lblCaloriesLeft.Content = "My Left Calories : " + MB;

            this.Visibility = Visibility.Collapsed;


        }


        private void cmbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            cmb.IsEnabled = false; 
        }

        private void cmbActivityLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            cmb.IsEnabled = false; 
        }

        private void txtAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox txt = (TextBox)sender;
                if (!int.TryParse(txt.Text, out int age))
                {
                    MessageBox.Show("Please enter a valid age (numeric value).");
                    txt.Text = ""; // Clear the text box
                }
                else txtAge.IsEnabled = false;
            }
        }

        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox txt = (TextBox)sender;
                if (!double.TryParse(txt.Text, out double weight))
                {
                    MessageBox.Show("Please enter a valid weight (numeric value).");
                    txt.Text = ""; // Clear the text box
                }
                else txtWeight.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cmbActivityLevel.IsEnabled = true;
            cmbGender.IsEnabled = true;
            txtAge.IsEnabled = true;
            txtWeight.IsEnabled = true;
            txtHeight.IsEnabled = true;
            btnCalculate.IsEnabled = true;
            CaloriesDisplayed.Content = "My Calories : ";
        }


        //Calculating the calories
        private void Calories_Click(object sender, RoutedEventArgs e)
        {
            if (!cmbActivityLevel.IsEnabled && !cmbGender.IsEnabled && !txtAge.IsEnabled && !txtWeight.IsEnabled && !txtHeight.IsEnabled)
            {
                ComboBoxItem selectedItem = cmbGender.SelectedItem as ComboBoxItem;
                double MB_base = 0;
                double age = Double.Parse(txtAge.Text);
                double Weight = Double.Parse(txtWeight.Text);
                double Height = Double.Parse(txtHeight.Text);
                if (selectedItem.Content.ToString() == "Male")
                {
                    MB_base = (Weight * 13.707) + (Height * 429.3) - (age * 6.673) + 77.607;
                    selectedItem = cmbActivityLevel.SelectedItem as ComboBoxItem;
                    if (selectedItem.Content.ToString() == "No / Almost No Sport") 
                    {
                        MB_base *= 1.3;
                    }
                    else if (selectedItem.Content.ToString() == "1 to 3 Days a Week") 
                    {
                        MB_base *= 1.375;
                    }
                    else 
                    {
                        MB_base *= 1.5;
                    }
                }
                else if (selectedItem.Content.ToString() == "Female")
                {
                    MB_base = (Weight * 9.740) + (Height * 172.9) - (4.737 * age) + 667.051;
                    selectedItem = cmbActivityLevel.SelectedItem as ComboBoxItem;
                    if (selectedItem.Content.ToString() == "No / Almost No Sport")
                    {
                        MB_base *= 1.3;
                    }
                    else if (selectedItem.Content.ToString() == "1 to 3 Days a Week")
                    {
                        MB_base *= 1.375;
                    }
                    else
                    {
                        MB_base *= 1.5;
                    }
                }
                MB = MB_base;
                MB = Math.Round(MB);
                CaloriesDisplayed.Content += MB.ToString();
                btnCalculate.IsEnabled = false;
            }
            else MessageBox.Show("You need to fill every infos !");
        }

        private void txtHeight_Copy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox txt = (TextBox)sender;
                if (!double.TryParse(txt.Text, out double weight))
                {
                    MessageBox.Show("Please enter a valid weight (numeric value).");
                    txt.Text = ""; // Clear the text box
                }
                else txtHeight.IsEnabled = false;
            }
        }

        private void txtAge_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtAge.Text == "Age")
            {
                txtAge.Text = "";
            }
        }

        private void txtAge_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                txtAge.Text = "Age";
            }
        }

        private void txtWeight_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtWeight.Text == "Weight")
            {
                txtWeight.Text = "";
            }
        }

        private void txtWeight_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                txtWeight.Text = "Weight";
            }
        }

        private void txtHeight_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtHeight.Text == "Height")
            {
                txtHeight.Text = "";
            }
        }

        private void txtHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHeight.Text))
            {
                txtHeight.Text = "Height";
            }
        }
    }
}
