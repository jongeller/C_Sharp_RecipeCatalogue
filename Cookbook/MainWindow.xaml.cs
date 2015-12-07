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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;

using TSSearch;

namespace Cookbook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //RecipeOrganizerEntities RO = new RecipeOrganizerEntities();
        RecipeCollection RC = new RecipeCollection();

        private bool allowClosing = false;

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        private const uint MF_BYCOMMAND = 0x00000000;
        private const uint MF_GRAYED = 0x00000001;

        private const uint SC_CLOSE = 0xF060;

        private const int WM_SHOWWINDOW = 0x00000018;
        private const int WM_CLOSE = 0x10;

        public MainWindow()
        {
            InitializeComponent();
            mainWindow.Title = titleLabel.Content.ToString();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            allowClosing = true;
            mainWindow.Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (recipeList.SelectedIndex > -1)
            {
                //Added to support edit recipe
                editButton.IsEnabled = true;
                ////

                string selectedRecipeTitle = string.Empty;

                selectedRecipeTitle = recipeList.SelectedItem as string;

                ingrediantListBox.DataContext = (from R in RC.Items from I in R.Ingredients where R.DisplayTitle == selectedRecipeTitle && R.RecipeID == I.RecipeID select I).ToList();

                recipeStepListBox.DataContext = (from R in RC.Items where R.DisplayTitle == selectedRecipeTitle select R.Directions).ToList();

                commentListBox.DataContext = (from R in RC.Items where R.DisplayTitle == selectedRecipeTitle select R.Comment).ToList();

                //THIS MAKES A HARD-CODED ASSUMPTION, BEWARE!!!!!
                //It only works if there is only 1 item selected from the recipe list.
                List<Recipe> selectedRecipes = (from R in RC.Items where R.DisplayTitle == selectedRecipeTitle select R).ToList();
                descLabel.Content = selectedRecipes[0].Title;
                mainWindow.Title = selectedRecipes[0].Title;
                ///Ends hard coded assumption.
                ///

            }
            else
            {
                List<string> comments = new List<string>();
                comments.Add("Form has been reset");

                commentListBox.DataContext = comments;
            }
        }
        
        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            
            descLabel.Content = string.Empty;
            ingrediantListBox.DataContext = null;
            recipeStepListBox.DataContext = null;
            commentListBox.DataContext = null;

            mainWindow.Title = titleLabel.Content.ToString();
            
            recipeList.SelectedIndex = -1;


            LoadRecipes(RC.Items);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadRecipes(RC.Items);

            List<Recipe> iterateList = new List<Recipe>();

            iterateList = (from L in RC.Items orderby L.RecipeType, L.DisplayTitle select L).ToList();

            //foreach (Recipe r in iterateList)
            //{
            //    MessageBox.Show(r.DisplayTitle);
            //}
        }

        //Freeze the big X
        //Get the handle to the main window to intercept the WM_ClOSE event handler
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)
            {
                hwndSource.AddHook(HwndSourceHook);
            }

        }

        //Alter the WM_CLOSE event handler
        private IntPtr HwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_SHOWWINDOW:
                    {
                        IntPtr hMenu = GetSystemMenu(hwnd, false);
                        if (hMenu != IntPtr.Zero)
                        {
                            EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
                        }
                    }
                    break;
                case WM_CLOSE:
                    if (!allowClosing)
                    {
                        handled = true;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            
            //Initialize a search object
            ToStringSearch<Recipe> searchObj = new ToStringSearch<Recipe>();
            //Initialize a list for the search results
            List<Recipe> srchResults = new List<Recipe>();

            //Get the current list if items from the listbox
            //List<Recipe> recipes = (from R in RC.Items select R).ToList();

            //Display the search dialog
            SearchBox search = new SearchBox();

            if (search.ShowDialog() == true)
            {
                //Search for recipes that contain the search string
                srchResults = searchObj.SearchForString(search.SearchString, RC.Items);
                if (srchResults != null)
                {
                    //Update the listbox on the main window

                    object o = new object();
                    RoutedEventArgs REA = new RoutedEventArgs();
                    resetButton_Click(o, REA);
                    recipeList.DataContext = (from L in srchResults orderby L.RecipeType, L.DisplayTitle select L.DisplayTitle).ToList(); ;
                }
                else
                {
                    MessageBox.Show("No recipes were found.", "Cookbook Search");
                }
            }
        }

        //Added call options to support the select of added or updated recipe
        private void LoadRecipes(List<Recipe> recipes, Recipe SelectedRecipe = null)
        {
            recipeList.DataContext = (from L in recipes orderby L.RecipeType, L.DisplayTitle select L.DisplayTitle).ToList();

            //Added to support add/edit recipe
            if (SelectedRecipe != null)
            {
                recipeList.SelectedItem = SelectedRecipe.DisplayTitle;
            }
        }

        //Added to support Add recipe
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditRecipies AddRecipe = new AddEditRecipies();
            if (AddRecipe.ShowDialog() == true)
            {
                if (AddRecipe.DoWork)
                {
                    //Adds the recipe to the collection.
                    RC.AddRecipe(AddRecipe.Recipe);

                    //Cleans the form.
                    LoadRecipes(RC.Items, AddRecipe.Recipe);
                }
                else
                {
                    MessageBox.Show("Cancelled adding a new recipe.", "Cookbook Add Recipe");
                }
            }

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            List<Recipe> SelectedRecipe = (from R in RC where R.DisplayTitle == (string)recipeList.SelectedValue select R).ToList();

            AddEditRecipies EditRecipe = new AddEditRecipies(SelectedRecipe[0]);

            if (EditRecipe.ShowDialog() == true)
            {
                if (EditRecipe.DoWork)
                {
                    //Updated the recipe to the collection.
                    RC.UpdateRecipe(EditRecipe.Recipe,recipeList.SelectedIndex);

                    //Cleans the form.
                    LoadRecipes(RC.Items, EditRecipe.Recipe);
                }
                else
                {
                    MessageBox.Show("Cancelled adding a new recipe.", "Cookbook Add Recipe");
                }
            }
        }
    }

}
