﻿using System;
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
        public AddEditRecipies(Recipe RecipeToUpdate = null, bool Copy = false)
        {

            //Check to see if recipe has been passed in.
            if (RecipeToUpdate == null)
            {
                Title = "Add a new recipe.";

                //Assign the persistant recipe a new recipe object on the heap.
                WorkingRecipe = new Recipe();

            }
            else if(Copy)
            {
                Title = "Creating Copy of " + RecipeToUpdate.Title;

                WorkingRecipe = CopyRecipe(RecipeToUpdate);

                WorkingRecipe.Title = "Copy of: " + WorkingRecipe.Title;
            }
            else
            {
                Title = "Update " + RecipeToUpdate.Title + " recipe.";

                //Assign the persistant recipe the passed in recipe object on the heap.
                WorkingRecipe = CopyRecipe(RecipeToUpdate);
            }

            InitializeComponent();

            tbRecipeTitle.DataContext = this;
            tbRecipeServingSize.DataContext = this;
            tbRecipeType.DataContext = this;
            tbRecipeYield.DataContext = this;
            tbDirections.DataContext = this;
            tbComment.DataContext = this;
            lstbxIngredients.DataContext = this;

        }

        /// <summary>
        /// Creates a duplicate of a recipe object.
        /// </summary>
        /// <param name="sourceRecipe"></param>
        /// <returns>Recipe object that is a copy of the source recipe</returns>
        private Recipe CopyRecipe(Recipe sourceRecipe)
        {
            Recipe newRecipe = new Recipe();
            //Duplicate all properties
            newRecipe.Comment = sourceRecipe.Comment;
            newRecipe.Directions = sourceRecipe.Directions;
            newRecipe.RecipeID = sourceRecipe.RecipeID;
            newRecipe.RecipeType = sourceRecipe.RecipeType;
            newRecipe.ServingSize = sourceRecipe.ServingSize;
            newRecipe.Title = sourceRecipe.Title;
            newRecipe.Yield = sourceRecipe.Yield;

            foreach (Ingredient i in sourceRecipe.Ingredients)
            {
                Ingredient newIng = new Ingredient();
                newIng.IngredientID = i.IngredientID;
                newIng.Ingredient1 = i.Ingredient1;
                newIng.RecipeID = i.RecipeID;

                newRecipe.Ingredients.Add(newIng);
            }

            return newRecipe;
        }

        /// <summary>
        /// When an ingredient is selected, the edit button is enabled.
        /// </summary>
        private void IngredientSelected(object sender, SelectionChangedEventArgs e)
        {
            //enables the edit button when an ingredient is selected.
            btnEditIngredient.IsEnabled = true;
            btnDeleteIngredient.IsEnabled = true;

            if (lstbxIngredients.SelectedIndex > -1)
            {
                tbIngredient.Text = (lstbxIngredients.SelectedValue as Ingredient).Ingredient1;
            }
            else
            {
                tbIngredient.Text = "";
            }
        }

        /// <summary>
        /// If cancel is clicked, the form will exit, and the bool to signal work is set to false.
        /// </summary>
        private void cancelClicked(object sender, RoutedEventArgs e)
        {
            //exits the form with no changes.
            DoWork = false;
            DialogResult = false;  //TODO:Should this be false? Changed from true to false
        }

        /// <summary>
        /// If submit is clicked, the form will exit, and the bool to signal work is set to true.
        /// </summary>
        private void submitClicked(object sender, RoutedEventArgs e)
        {

            if (RecipeValidation())
            {
                //exits the form with the bool set to indicate the recipe add or edit was complete
                DoWork = true;
                DialogResult = true;
            }
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
            btnDeleteIngredient.IsEnabled = false;
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
            btnDeleteIngredient.IsEnabled = false;
            tbIngredient.IsEnabled = true;

            NewIngredient = true;
        }

        private void DeleteIngredientClicked(object sender, RoutedEventArgs e)
        {
            Ingredient temp = new Ingredient();
            temp = lstbxIngredients.SelectedItem as Ingredient;

            WorkingRecipe.Ingredients.Remove(temp);
            
            lstbxIngredients.Items.Refresh();

            

        }

        private void SubmitIngredientClicked(object sender, RoutedEventArgs e)
        {
            if (IngredientValidation())
            {
                btnSubmitIngredient.IsEnabled = false;
                btnEditIngredient.IsEnabled = false;
                btnAddIngredient.IsEnabled = true;
                tbIngredient.IsEnabled = true;

                if (NewIngredient)
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

                lstbxIngredients.Items.Refresh();

                tbIngredient.Text = string.Empty;
                tbIngredient.BorderBrush = lstbxIngredients.BorderBrush;
            }
        }

        private bool IngredientValidation()
        {
            if(string.IsNullOrEmpty(tbIngredient.Text))
            {
                lblValidation.Content = "Ingredient cannot be empty.";
                tbIngredient.BorderBrush = Brushes.Red;
                lblValidation.Visibility = Visibility.Visible;
                return false;
            }

            tbIngredient.BorderBrush = lstbxIngredients.BorderBrush;
            lblValidation.Visibility = Visibility.Hidden;
            return true;
        }

        private bool RecipeValidation()
        {
            List<TextBox> FieldsToValidate = new List<TextBox>();
            FieldsToValidate.Add(tbRecipeTitle);
            FieldsToValidate.Add(tbRecipeType);
            FieldsToValidate.Add(tbDirections);

            //According to DB design these fields CAN be blank so they will not be validated
            //FieldsToValidate.Add(tbRecipeYield);
            //FieldsToValidate.Add(tbRecipeServingSize);
            //FieldsToValidate.Add(tbComment);

            foreach(TextBox tb in FieldsToValidate)
            {
                if (string.IsNullOrEmpty(tb.Text))
                {
                    lblValidation.Content = "Field cannot be empty.";
                    tb.BorderBrush = Brushes.Red;
                    lblValidation.Visibility = Visibility.Visible;
                    return false;
                }
                tb.BorderBrush = lstbxIngredients.BorderBrush;
            }

            //Validate RecipeType since other methods assume only two values exist
            if ((tbRecipeType.Text!="Dessert")&&(tbRecipeType.Text!="Meal Item"))
            {
                lblValidation.Content = "Recipe Type must be Dessert or Meal Item";
                tbRecipeType.BorderBrush = Brushes.Red;
                lblValidation.Visibility = Visibility.Visible;
                return false;
            }


            lblValidation.Visibility = Visibility.Hidden;
            return true;
        }
    

        /// <summary>
        /// The added or updated recipe returned by the form.
        /// </summary>
        public Recipe Recipe
        {
            get { return WorkingRecipe; }
        }

        public ICollection<Ingredient> RecipeIngredients
        {
            get { return WorkingRecipe.Ingredients; }
        }

        public string RecipeTitle
        {
            get { return WorkingRecipe.Title; }
            set { WorkingRecipe.Title = value; }
        }

        public string RecipeType
        {
            get { return WorkingRecipe.RecipeType; }
            set { WorkingRecipe.RecipeType = value; }
        }

        public string RecipeDirections
        {
            get { return WorkingRecipe.Directions; }
            set { WorkingRecipe.Directions = value; }
        }

        public string RecipeComment
        {
            get { return WorkingRecipe.Comment; }
            set { WorkingRecipe.Comment = value; }
        }

        public string RecipeServingSize
        {
            get { return WorkingRecipe.ServingSize; }
            set { WorkingRecipe.ServingSize = value; }
        }

        public string RecipeYield
        {
            get { return WorkingRecipe.Yield; }
            set { WorkingRecipe.Yield = value; }
        }

       
    }
}
