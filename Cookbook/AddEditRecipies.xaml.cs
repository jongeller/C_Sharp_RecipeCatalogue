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

namespace Cookbook
{
    /// <summary>
    /// Interaction logic for AddEditRecipies.xaml
    /// </summary>
    public partial class AddEditRecipies : Window
    {
        //This is used to persist the recipe, even if a method closes.
        private Recipe WorkingRecipe;

        //This bool is used to determine if the ingredient being edited in new or an update
        private bool NewIngredient;

        //This bool is used to see if the user clcked cancel or submit. If cancelled, this bool will remain false;
        public bool DoWork { get; set; }
        

        /// <summary>
        /// This window constructor will define the form for add new or update existing.
        /// </summary>
        /// <param name="RecipeToUpdate">If there is a recipe passed in, the form will assume it needs to edit, not add a new recipe.</param>
        public AddEditRecipies(Recipe RecipeToUpdate = null)
        {
            InitializeComponent();

            //Check to see if recipe has been passed in.
            if (RecipeToUpdate == null)
            {
                Title = "Add a new recipe.";

                //Assign the persistant recipe a new recipe object on the heap.
                WorkingRecipe = new Recipe();

                LoadForm();
            }
            else
            {
                Title = "Update " + RecipeToUpdate.Title + " recipe.";

                //Assign the persistant recipe the passed in recipe object on the heap.
                WorkingRecipe = RecipeToUpdate;

                //Call method to assign all widow fields to the values.
                LoadForm();
            }
        }

        /// <summary>
        /// Set the form if an existing recipe is passed into it.
        /// </summary>
        /// <param name="recipeToUpdate">The recipe to populate the form with.</param>
        private void LoadForm()
        {
            //Assign text box fields
            tbRecipeTitle.Text = WorkingRecipe.Title;
            tbRecipeType.Text = WorkingRecipe.RecipeType;
            tbRecipeYeild.Text = WorkingRecipe.Yield;
            tbRecipeServingSize.Text = WorkingRecipe.ServingSize;
            tbDirections.Text = WorkingRecipe.Directions;
            tbComment.Text = WorkingRecipe.Comment;
            
            //Set ingredients to listbox
            lstbxIngredients.DataContext = (from I in WorkingRecipe.Ingredients orderby I.Ingredient1 select I).ToList();

            //focuses the form on the submit button, so if anyone hits enter, it will submit the recipe.
            btnSubmit.Focus();
        }

        /// <summary>
        /// When an ingredient is selected, the edit button is enabled.
        /// </summary>
        private void IngredientSelected(object sender, SelectionChangedEventArgs e)
        {
            //enables the edit button when an ingredient is selected.
            btnEditIngredient.IsEnabled = true;
            tbIngredient.Text = (lstbxIngredients.SelectedValue as Ingredient).Ingredient1;
        }

        /// <summary>
        /// If cancel is clicked, the form will exit, and the bool to signal work is set to false.
        /// </summary>
        private void cancelClicked(object sender, RoutedEventArgs e)
        {
            //exits the form with no changes.
            DoWork = false;
            DialogResult = true;
        }

        /// <summary>
        /// If submit is clicked, the form will exit, and the bool to signal work is set to true.
        /// </summary>
        private void submitClicked(object sender, RoutedEventArgs e)
        {
            //exits the form with the bool set to indicate the recipe add or edit was complete
            UpdateWorkingRecipe();
            DoWork = true;
            DialogResult = true;
        }

        /// <summary>
        /// enables the field for editing an ingredient.
        /// </summary>
        private void EditIngredientClicked(object sender, RoutedEventArgs e)
        {

            tbIngredient.BorderBrush = Brushes.GreenYellow;


            //disabled ingredient buttons, and enables submit buttons, enables ingredient textbox for editing.
            btnSubmitIngredient.IsEnabled = true;
            btnEditIngredient.IsEnabled = false;
            btnAddIngredient.IsEnabled = false;
            tbIngredient.IsEnabled = true;

            NewIngredient = false;

        }


        /// <summary>
        /// enables the field for adding an ingredient.
        /// </summary>
        private void AddIngredientClicked(object sender, RoutedEventArgs e)
        {

            tbIngredient.BorderBrush = Brushes.GreenYellow;

            //disabled ingredient buttons, and enables submit buttons, enables ingredient textbox for editing.
            btnSubmitIngredient.IsEnabled = true;
            btnEditIngredient.IsEnabled = false;
            btnAddIngredient.IsEnabled = false;
            tbIngredient.IsEnabled = true;

            NewIngredient = true;
        }

        private void SubmitIngredientClicked(object sender, RoutedEventArgs e)
        {
            btnSubmitIngredient.IsEnabled = false;
            btnEditIngredient.IsEnabled = false;
            btnAddIngredient.IsEnabled = true;
            tbIngredient.IsEnabled = true;
            
            if(NewIngredient)
            {
                Ingredient newIngredientData = new Ingredient();
                newIngredientData.Ingredient1 = tbIngredient.Text;
                newIngredientData.RecipeID = WorkingRecipe.RecipeID;
                newIngredientData.Recipe = WorkingRecipe;
                WorkingRecipe.Ingredients.Add(newIngredientData);
            }
            else
            {
                //Update the ingredient that is bound to the collection.
                (lstbxIngredients.SelectedItem as Ingredient).Ingredient1 = tbIngredient.Text;
            }

            tbIngredient.Text = string.Empty;
            tbIngredient.BorderBrush = tbComment.BorderBrush;

            UpdateWorkingRecipe();

            LoadForm();
        }

        private void UpdateWorkingRecipe()
        {
            WorkingRecipe.Title = tbRecipeTitle.Text;
            WorkingRecipe.RecipeType = tbRecipeType.Text;
            WorkingRecipe.ServingSize = tbRecipeServingSize.Text;
            WorkingRecipe.Yield = tbRecipeYeild.Text;
            WorkingRecipe.Comment = tbComment.Text;
            WorkingRecipe.Directions = tbDirections.Text;
        }

        /// <summary>
        /// The added or updated recipe returned by the form.
        /// </summary>
        public Recipe Recipe
        {
            get { return WorkingRecipe; }
        }
            
    }
}
