using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ProjectApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Model1Container db = new Model1Container();
        private bool CaloriesDone = false;
        private int DaysCompter = 0;
        List<string> foodAdded = new List<string>();

        internal ObservableCollection<double> LeftCalories = new ObservableCollection<double> {0,0};

        ProfileWindow profileWindow = new ProfileWindow();
        AddingFoodWindow addingFoodWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from ft in db.FoodTypeSet
                        select ft;

            cbxFoodType.ItemsSource = query.ToList();
        }

        //Load Profile Page
        private void Button_Click(object sender, RoutedEventArgs e)
        {    
            //establish connection
            profileWindow.Owner = this;

            if (profileWindow.Visibility == Visibility.Visible)
                profileWindow.ShowDialog();

            else
                profileWindow.Visibility = Visibility.Visible;
        }
        private void Add_FoodItem_Click(object sender, RoutedEventArgs e)
        {
            addingFoodWindow = new AddingFoodWindow(db);
            addingFoodWindow.Owner = this;
            addingFoodWindow.ShowDialog();
        }

        private void lbxFooditem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FoodItem food = lbxFooditem.SelectedItem as FoodItem;
            if (food != null)
            {
                txtQuantity.Visibility = Visibility.Visible;
                lblQuantity.Visibility = Visibility.Visible;

            }
        }

        //problem to fix when cbx is not selected yet
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            if (searchText != "")
            {
                 
                //Case where we selected a type of food 
                if (cbxFoodType != null) 
                {
                    int foodId = (int)cbxFoodType.SelectedValue;

                    List<FoodItem> filteredItems;

                    if (foodId == 9) 
                        filteredItems = db.FoodItemSet.Where(item => item.Name.ToLower().Contains(searchText)).ToList();
                    else 
                        filteredItems = db.FoodItemSet.Where(item => item.Name.ToLower().Contains(searchText) && item.FoodTypeId == foodId).ToList();
                    lbxFooditem.ItemsSource = filteredItems;
                }
                //Case where we didn't select any type of food 
                else 
                {
                    var filteredItems = db.FoodItemSet.Where(item => item.Name.ToLower().Contains(searchText)).ToList();
                    lbxFooditem.ItemsSource = filteredItems;
                }


            }
           
        }

        private void TxtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Search Food")
            {
                txtSearch.Text = "";
            }
        }

        private void TxtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search Food";
            }
        }

        private void cbxFoodType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSearch.IsEnabled = true;
            IOrderedQueryable<FoodItem> query;
            int foodId = (int)cbxFoodType.SelectedValue;
            if (foodId != null)
            {
                if (foodId == 9) 
                {
                    query = from ft in db.FoodItemSet 
                            orderby ft.Calories
                            select ft;
                }
                else 
                {
                    query = from ft in db.FoodItemSet
                            where ft.FoodTypeId == foodId
                            orderby ft.Calories
                            select ft;
                }
                

                lbxFooditem.ItemsSource = query.ToList();
            }
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox txt = (TextBox)sender;
                if (!double.TryParse(txt.Text, out double quantity))
                {
                    MessageBox.Show("Please enter a valid quantity (numeric value).");
                    txt.Text = ""; // Clear the text box
                }
                else 
                {
                    FoodItem food = lbxFooditem.SelectedItem as FoodItem;
                    LeftCalories[0] -= Math.Round((quantity * food.Calories)/100);
                    if (LeftCalories[0] > 0)
                        lblCaloriesLeft.Content = "My Left Calories : " + LeftCalories[0];
                    else
                        lblCaloriesLeft.Content = "No Calories left!!";
                    txtQuantity.Visibility = Visibility.Hidden;
                    lblQuantity.Visibility = Visibility.Hidden;

                    txt.Text = "";
                    string new_food_eaten = food.Name + " - " + Math.Round((quantity * food.Calories) / 100).ToString() + " kcal";
                    foodAdded.Add(new_food_eaten);
                    LbxFoodAdded.ItemsSource = foodAdded.ToList();
                }
            }
        }

        private void Reset_Day_On_Click(object sender, RoutedEventArgs e)
        {
            LeftCalories[0] = LeftCalories[1];

            foodAdded = new List<string>();
            LbxFoodAdded.ItemsSource = foodAdded.ToList();

            lblCaloriesLeft.Content = "My Left Calories : " + LeftCalories[0];

            DaysCompter++;
            lblDays.Content = "Day " + DaysCompter;
        }

        
    }
}
