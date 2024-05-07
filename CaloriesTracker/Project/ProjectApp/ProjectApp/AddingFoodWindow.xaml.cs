using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AddingFoodWindow.xaml
    /// </summary>
    public partial class AddingFoodWindow : Window
    {
        Model1Container db;
        string selectedImagePath;
        int FoodTypeID; 

        public AddingFoodWindow(Model1Container db)
        {
            InitializeComponent();
            this.db = db;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from ft in db.FoodTypeSet
                        where (ft.Name != "All")
                        select ft;

            cbxFoodType.ItemsSource = query.ToList();
        }

        private void Button_Click(object sender, object e)
        {
            FoodItem toadd = new FoodItem()
            {
                Calories = Int32.Parse(txtCalories.Text),
                Name = txtName.Text,
                FoodItemImage = selectedImagePath,
                FoodTypeId = FoodTypeID
            };

            db.FoodItemSet.Add(toadd);
            db.SaveChanges();
            this.Close();   
        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog= new OpenFileDialog();

            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg;*.gif)|*.png;*.jpg;*.jpeg;*.gif|All files (*.*)|*.*";

            bool? result = openFileDialog.ShowDialog();

            if (result == true ) 
            {
                selectedImagePath = openFileDialog.FileName;

                imgSelectedImage.Source = new BitmapImage(new Uri(selectedImagePath));

                btnAdd.IsEnabled = true;
            }
        }

        private void FoodType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = cbxFoodType.SelectedItem as FoodType;
            FoodTypeID = selected.Id;
        }

        private void txtCalories_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox txt = (TextBox)sender;
                if (!double.TryParse(txt.Text, out double calories))
                {
                    MessageBox.Show("Please enter a valid calories number (numeric value).");
                    txt.Text = ""; // Clear the text box
                }
                else 
                {
                    txtCalories.IsEnabled = false;
                    btnSelect.IsEnabled = true; 
                }
            }
        }

        private void txtCalories_GotFocus(object sender, RoutedEventArgs e)
        {
            if(txtCalories.Text == "Calories / 100g") 
            {
                txtCalories.Text = string.Empty;
            }
        }

        private void txtCalories_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCalories.Text))
            {
                txtCalories.Text = "Calories / 100g";
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox txt = (TextBox)sender;
                if (txt.Text == string.Empty)
                    MessageBox.Show("Please enter a Name before pressing Enter.");
                else 
                {
                    txt.IsEnabled = false;
                    txtCalories.IsEnabled = true; 
                }
            }
        }

        private void txtName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "Food Name")
            {
                txtName.Text = string.Empty;
            }
        }

        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.Text = "Food Name";
            }
        }
    }
}
