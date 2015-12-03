using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook
{
    class RecipeCollection : IEnumerable<Recipe>
    {
        private List<Recipe> recipeCollection = new List<Recipe>();
        private RecipeOrganizerEntities RO = new RecipeOrganizerEntities();


        //Constructor loads recipes from the database during instantiation
        public RecipeCollection()
        {
            LoadRecipes();
            CastRecipesByType();
        }

        public void AddRecipe(Recipe NewRecipe)
        {
            recipeCollection.Add(NewRecipe);
        }

        //Cast recipes by type
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
            }
        }

        //Adding to the Collection
        public void AddRecipe(Recipe r)
        {
            //Create a recipe validation method
            //Validate that this recipe is good

            this.recipeCollection.Add(r);

            //Add this recipe to the DB
        }

        public void UpdateRecipe(Recipe r, int collectionPostion)
        {
            //validate that this recipe is good

            this.recipeCollection[collectionPostion] = r;

            //Update this recipe in the DB
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

        //Items property
        public List<Recipe> Items
        {
            get { return recipeCollection; }
        }

        private void LoadRecipes()
        {
            //Load the recipes from the DB
       
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
