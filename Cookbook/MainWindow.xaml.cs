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
            LoadRecipes(RC.Items);
            ToggleCover(true);
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            allowClosing = true;
            mainWindow.Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUI();
        }


        private void UpdateUI()
        {
            if (recipeList.SelectedIndex > -1)
            {
                //Hide cover image
                ToggleCover(false);

                //Get the currently selected recipe data
                string selectedRecipeTitle = string.Empty;
                selectedRecipeTitle = recipeList.SelectedItem as string;
                List<Recipe> selectedRecipes = (from R in RC.Items where R.DisplayTitle == selectedRecipeTitle select R).ToList();
                Recipe selectedRecipe = selectedRecipes[0];

                //Update the other controls with data from the selected recipe
                ingrediantListBox.DataContext = (from I in selectedRecipe.Ingredients where selectedRecipe.DisplayTitle == selectedRecipeTitle && selectedRecipe.RecipeID == I.RecipeID select I).ToList();

                recipeStepTextBox.Text = selectedRecipe.Directions;

                yieldTextBox.Text = "";
                servingTextBox.Text = "";
                List<String> stringList = new List<string>();


                if (selectedRecipe.Yield != null)
                {
                    yieldTextBox.Text = "Yield: " + selectedRecipe.Yield;
                }
                if (selectedRecipe.ServingSize != null)
                {
                    servingTextBox.Text = "Size: " + selectedRecipe.ServingSize;
                }

                commentTextBox.Text = selectedRecipe.Comment;


                //THIS MAKES A HARD-CODED ASSUMPTION, BEWARE!!!!!
                //It only works if there is only 1 item selected from the recipe list.
                descLabel.Content = selectedRecipe.Title;
                mainWindow.Title = selectedRecipe.Title;
                ///Ends hard coded assumption.
                ///

            }
            else
            {
                //Show cover image if no recipe is selected
                ToggleCover(true);
            }

            //Activate the edit button if an item is selected
            if (recipeList.SelectedIndex != -1)
            {
                editButton.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                editButton.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        
        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            //For Debug and testing 
            //MessageBox.Show("Width=" + mainWindow.ActualWidth.ToString());


            descLabel.Content = string.Empty;
            ingrediantListBox.DataContext = null;

            recipeStepTextBox.Text = "";
            commentTextBox.Text = "";
            yieldTextBox.Text = "";
            servingTextBox.Text = "";


            mainWindow.Title = titleLabel.Content.ToString();

            recipeList.SelectedIndex = -1;
            HideMessageBox();

            RC.FilteredItems = RC.Items;
            LoadRecipes(RC.Items);
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

            //Display the search dialog - Set the owner so it opens in the center of the main window
            SearchBox search = new SearchBox();
            search.Owner = this;

            if (search.ShowDialog() == true)
            {
                //Search the entire database
                if (search.SearchAllRecipes)
                {
                    //Search for recipes that contain the search string
                    srchResults = searchObj.SearchForString(search.SearchString, RC.Items, search.CaseSensitive, search.MatchAllTerms);
                }
                else //Search the filtered list
                {
                    if (messageText.Text == "No recipes were found.")
                    {
                        srchResults = null;
                    }
                    else
                    {
                        srchResults = searchObj.SearchForString(search.SearchString, RC.FilteredItems, search.CaseSensitive, search.MatchAllTerms);
                    }
                }

                if (srchResults != null)
                {
                    //Clear the controls on the main window
                    object o = new object();
                    RoutedEventArgs REA = new RoutedEventArgs();
                    resetButton_Click(o, REA);
                    messageText.Text = "";

                    //Update the filtered list with the search results
                    RC.FilteredItems = srchResults;
                    recipeList.DataContext = (from L in RC.FilteredItems orderby L.RecipeType, L.DisplayTitle select L.DisplayTitle).ToList(); ;
                }
                else
                {
                    ShowMessageBox("No recipes were found.");
                }
            }
        }

        private void LoadRecipes(List<Recipe> recipes)
        {
            recipeList.DataContext = (from L in recipes orderby L.RecipeType, L.DisplayTitle select L.DisplayTitle).ToList();
            List<string> tmp = new List<string>();
            tmp.Add("All");

            foreach (Recipe r in recipes)
            {
                if (!tmp.Contains(r.RecipeType)) tmp.Add(r.RecipeType);
            }
            tmp.Sort();
            filterCombo.DataContext = tmp; 
            filterCombo.SelectedIndex = 0;
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filterCombo.SelectedIndex != -1)
            {
                if (filterCombo.SelectedItem.ToString() == "All")
                {
                    recipeList.DataContext = (from L in RC.FilteredItems orderby L.DisplayTitle select L.DisplayTitle).ToList();
                }
                else
                {
                    recipeList.DataContext = (from L in RC.FilteredItems where L.RecipeType == filterCombo.SelectedItem.ToString() orderby L.DisplayTitle select L.DisplayTitle).ToList();
                }
            }
        }

        /// <summary>
        /// Scales the fontsize for the two title labels based on size of the mainWindow.
        /// This makes it possible for the entire recipe title to be visible regardless of window resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double controlSize = mainWindow.ActualWidth;
            if (controlSize>825)
            {
                descLabel.FontSize = 28;
                titleLabel.FontSize = 38;
            }
            else if ((controlSize > 690) && (controlSize <= 825))
            {
                descLabel.FontSize = 26;
                titleLabel.FontSize = 38;
            }
            else if ((controlSize > 560) && (controlSize <= 690))
            {
                descLabel.FontSize = 24;
                titleLabel.FontSize = 38;
            }
            else if (controlSize <= 560)
            {
                descLabel.FontSize = 22;
                titleLabel.FontSize = 38;
            }
        }

        /// <summary>
        /// Toggles the cover image on and off
        /// </summary>
        public void ToggleCover(bool makeVisible)
        {
            if (makeVisible)
            {
                coverImage.Visibility = Visibility.Visible;
            }
            else
            {
                coverImage.Visibility = Visibility.Hidden;
            }
        }

        public void ShowMessageBox(string msg)
        {
            messageText.Text = msg;
            recipeList.Visibility = Visibility.Hidden;
            messageText.Visibility = Visibility.Visible;
        }

        public void HideMessageBox()
        {
            recipeList.Visibility = Visibility.Visible;
            messageText.Visibility = Visibility.Hidden;
        }

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            //ShowMessageBox("No recipes were found.");
            //MessageBox.Show("Text=" + filterCombo.Text);

            //AddEditRecipies addRecipeDialog = new AddEditRecipies();
            //addRecipeDialog.ShowDialog();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //Open dialog with null value to add new recipe
            AddEditRecipies addRecipeDialog = new AddEditRecipies(null);
            CancelAndConfirm confirmDialog = new CancelAndConfirm();

            addRecipeDialog.Owner = this;
            confirmDialog.Owner = this;

            //if edits were made...            
            if ((bool)addRecipeDialog.ShowDialog())
            {
                if ((bool)confirmDialog.ShowDialog())
                {
                    //Update recipe in database
                    RC.AddRecipe(addRecipeDialog.Recipe);


                    //update displayed list of recipes
                    LoadRecipes(RC.FilteredItems);

                    //descLabel.Content=editRecipeDialog.Recipe.Title;
                    recipeList.SelectedIndex = -1;
                    UpdateUI();
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            int selectIndex = recipeList.SelectedIndex;

            //Open dialog and pass recipe to be updated
            AddEditRecipies editRecipeDialog = new AddEditRecipies(RC[recipeList.SelectedValue.ToString()]);
            CancelAndConfirm confirmDialog = new CancelAndConfirm();
            editRecipeDialog.Owner = this;
            confirmDialog.Owner = this;

            //if edits were made...            
            if ((bool)editRecipeDialog.ShowDialog())
            {
                if ((bool)confirmDialog.ShowDialog())
                {
                    //Update recipe in database
                    RC.UpdateRecipe(editRecipeDialog.Recipe);


                    //update displayed list of recipes
                    LoadRecipes(RC.FilteredItems);

                    //descLabel.Content=editRecipeDialog.Recipe.Title;
                    recipeList.SelectedIndex = selectIndex;
                    UpdateUI();
                }
            }

        }

        
    }

}
