using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook
{
    public partial class Recipe
    {

        public override string ToString()
        {
            RecipeOrganizerEntities RO = new RecipeOrganizerEntities();


            string searchValue = string.Empty;

            //All data in the recipe object class.
            searchValue = Title + " " + Directions + " " + RecipeType + " " + ServingSize + " " + Yield;


            //All data in the ingredient object class.
            List<Ingredient> ingredients = new List<Ingredient>();

            //ingredients = (from R in RO.Recipes from I in R.Ingredients where I.RecipeID == R.RecipeID && R.Title == this.Title select I).ToList();
            ingredients = (from I in this.Ingredients where I.RecipeID == this.RecipeID select I).ToList();

            foreach(Ingredient ingredient in ingredients)
            {
                searchValue += " " + ingredient.Ingredient1;
            }

            return searchValue;
        }

        public virtual string DisplayTitle
        {
            get { return this.Title; }
        }

       
    }

    public class Meal: Recipe
    {
        public override string DisplayTitle
        {
            get { return "M-" + this.Title; }
        }
    }

    public class Dessert:Recipe
    {
        public override string DisplayTitle
        {
            get { return "D-" + this.Title; }
        }
    }
}
