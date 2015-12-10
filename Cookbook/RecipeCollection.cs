using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace Cookbook
{
    class RecipeCollection : IEnumerable<Recipe>
    {
        private List<Recipe> recipeCollection = new List<Recipe>();
        private List<Recipe> filteredList = new List<Recipe>();
        private RecipeOrganizerEntities RO = new RecipeOrganizerEntities();


        //Constructor loads recipes from the database during instantiation
        public RecipeCollection()
        {
            LoadRecipes();
            CastRecipesByType();
            filteredList = recipeCollection;
        }

        /// <summary>
        /// Cast recipes by type - THis method converts recipe objects to meal item or dessert
        /// objects based on RecipeType
        /// </summary>
        private void CastRecipesByType()
        {
            for (int r=0;r<recipeCollection.Count;r++)
            {
                switch (this[r].RecipeType.ToUpper().Trim())
                {
                    case "MEAL ITEM":
                        Meal snack = new Meal();
                        snack.Comment = this[r].Comment;
                        snack.Ingredients = this[r].Ingredients;
                        snack.RecipeID = this[r].RecipeID;
                        snack.RecipeType = this[r].RecipeType;
                        snack.ServingSize = this[r].ServingSize;
                        snack.Title = this[r].Title;
                        snack.Yield = this[r].Yield;
                        snack.Directions = this[r].Directions;
                        this[r] = snack;
                        
                        break;
                    case "DESSERT":
                        Dessert dessert = new Dessert();
                        dessert.Comment = this[r].Comment;
                        dessert.Ingredients = this[r].Ingredients;
                        dessert.RecipeID = this[r].RecipeID;
                        dessert.RecipeType = this[r].RecipeType;
                        dessert.ServingSize = this[r].ServingSize;
                        dessert.Title = this[r].Title;
                        dessert.Yield = this[r].Yield;
                        dessert.Directions = this[r].Directions;
                        this[r] = dessert;
                        break;
                    default:
                        break;
                }

                //Cleanup recipe text - remove extra spaces
                Recipe toBeCleaned = new Recipe();
                toBeCleaned = this[r];
                this[r] = CleanupRecipeText(toBeCleaned);
            }
        }

        /// <summary>
        /// Trims leading and trailing spaces off of all fields
        /// </summary>
        /// <returns>Recipe object</returns>
        private Recipe CleanupRecipeText(Recipe recipeToBeCleaned)
        {
            //Cleanup leading and trailing spaces in strings
            if (recipeToBeCleaned.Title != null) recipeToBeCleaned.Title = recipeToBeCleaned.Title.Trim();
            if (recipeToBeCleaned.RecipeType != null) recipeToBeCleaned.RecipeType = recipeToBeCleaned.RecipeType.Trim();
            if (recipeToBeCleaned.Yield != null) recipeToBeCleaned.Yield = recipeToBeCleaned.Yield.Trim();
            if (recipeToBeCleaned.ServingSize != null) recipeToBeCleaned.ServingSize = recipeToBeCleaned.ServingSize.Trim();
            if (recipeToBeCleaned.Comment != null) recipeToBeCleaned.Comment = recipeToBeCleaned.Comment.Trim();
            if (recipeToBeCleaned.Directions != null) recipeToBeCleaned.Directions = recipeToBeCleaned.Directions.Trim();    

            return recipeToBeCleaned;
        }

        /// <summary>
        /// Adds a new recipe to the collection and to the database
        /// </summary>
        /// <param name="NewRecipe">Recipe object to be added</param>
        public void AddRecipe(Recipe NewRecipe)
        {
            using (RecipeOrganizerEntities roContext = new RecipeOrganizerEntities())
            {
                roContext.Recipes.Add(NewRecipe);
                foreach (Ingredient i in NewRecipe.Ingredients)
                {
                    roContext.Ingredients.Add(i);
                }

                roContext.SaveChanges();
            }
            
            LoadRecipes();
            CastRecipesByType();

            //Reset the filtered list so the UI is refreshed properly
            filteredList = recipeCollection;
            
        }

        /// <summary>
        /// Updates an exisiting recipe object in the collection and in the database
        /// </summary>
        /// <param name="r">Recipe object to be updated</param>
        public void UpdateRecipe(Recipe r)//, int collectionPostion)
        {
            //Update this recipe in the DB
            this[r.DisplayTitle] = r;

            using (RecipeOrganizerEntities roContext = new RecipeOrganizerEntities())
            {
                Recipe recipeInstance = (from rec in roContext.Recipes
                                         where rec.RecipeID == r.RecipeID
                                         select rec).First();
                //modify recipe
                recipeInstance.RecipeID = r.RecipeID;
                recipeInstance.Title = r.Title;
                recipeInstance.Yield = r.Yield;
                recipeInstance.ServingSize = r.ServingSize;
                recipeInstance.Directions = r.Directions;
                recipeInstance.Comment = r.Comment;
                recipeInstance.RecipeType = r.RecipeType;
                roContext.SaveChanges();

                //then add ALL ingredients' additions and changes
                using (RecipeOrganizerEntities riContext = new RecipeOrganizerEntities())
                {

                    //All data in the ingredient object class.
                    List<Ingredient> ingredients = new List<Ingredient>();
                    ingredients = (from i in ingredients where i.RecipeID == r.RecipeID select i).ToList();

                    //update ingredients
                    //List<Ingredient> ingredientUpdate = (from ingr in riContext.Ingredients
                    //                                     where (ingr.RecipeID == r.RecipeID)
                    //                                     select ingr).ToList();
                    Ingredient ingredientInstance = new Ingredient();

                    ResetIngrList(recipeInstance);//, collectionPostion);
                    foreach (Ingredient ingrUpdate in r.Ingredients)
                    {
                        ingredientInstance.RecipeID = r.RecipeID;
                        ingredientInstance.Ingredient1 = ingrUpdate.Ingredient1;
                        //System.Windows.MessageBox.Show(ingrUpdate.Ingredient1);
                        riContext.Ingredients.Add(ingredientInstance);
                        riContext.SaveChanges();
                    }
                }
            }

            LoadRecipes();
            CastRecipesByType();

            //Update the filtered list so the UI is refreshed
            Recipe rc = filteredList.First(i => i.RecipeID == r.RecipeID);
            Recipe rc2 = recipeCollection.First(i => i.RecipeID == rc.RecipeID);
            filteredList[filteredList.IndexOf(rc)] = rc2;

        }

        //hard delete ingredients list to capture edit and add ingredients at the same time during UpdateRecipe
        public void ResetIngrList(Recipe r)// int collectionPostion)
        {
            this[r.DisplayTitle] = r;
            //this.recipeCollection[collectionPostion] = r;
            using (RecipeOrganizerEntities rxContext = new RecipeOrganizerEntities())
            {
                List<Ingredient> ingrdInstance = (from rec in rxContext.Ingredients
                                                  where rec.RecipeID == r.RecipeID
                                                  select rec).ToList();
                //run delete only when DB record exists
                if (ingrdInstance.Count != 0)
                {
                    foreach (Ingredient i in ingrdInstance)
                    {
                        rxContext.Ingredients.Remove(i);
                    }
                    rxContext.SaveChanges();
                }
            }

        }

        //Indexer by list index
        public Recipe this[int r]
        {
            get
            {
                if (r > recipeCollection.Count - 1 || r < 0)
                {
                    Exception e = new Exception("There is no recipe at that location in the list.");
                    throw e;
                }

                return recipeCollection[r]; 
            }
            set 
            {
                if (r > recipeCollection.Count - 1 || r < 0)
                {
                    Exception e = new Exception("There is no recipe at that location in the list.");
                    throw e;
                }
                recipeCollection[r] = value; 
            }
        }

        //Indexer by displayTitle
        public Recipe this[string dispTitle]
        {
            get
            {
                return recipeCollection.Find(rv => rv.DisplayTitle == dispTitle);
            }
            set
            {
                Recipe recipeItem = recipeCollection.Find(rv => rv.DisplayTitle == dispTitle);
                recipeItem = value;
            }
        }


        //FilteredItems property - after the list has been filtered by searching or the recipetype filter the current active list will be here
        public List<Recipe> FilteredItems
        {
            get { return filteredList; }
            set { filteredList = value; }
        }

        //Items property
        public List<Recipe> Items
        {
            get { return recipeCollection; }
        }

        private void LoadRecipes()
        {
            //Load the recipes from the DB
            RO = new RecipeOrganizerEntities();
            recipeCollection = (from R in RO.Recipes select R).ToList();
            
        }

        IEnumerator<Recipe> IEnumerable<Recipe>.GetEnumerator()
        {
            int top = this.recipeCollection.Count;
            int i = 0;
            do{
                yield return this.recipeCollection[i];
                i++;
            }while(i < top);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        
    }
}
