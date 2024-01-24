using System;
namespace Recipes
{
	public class Recipe
	{
		/// <summary>
		/// Exception for recipes.
		/// </summary>
        class RecipeException : Exception
        {
            public RecipeException(string message)
                : base(message) { }
        }

		// Index of recipe.
        public readonly int recipeId;

		// Name of recipe.
		public readonly string recipeName;

		// Chef of recipe.
		public readonly string chef;

		// Cuisine of recipe.
		public readonly string cuisine;

		// Difficulty of recipe.
		public readonly string difficulty;

		// List of ingredients of recipe.
		public readonly List<string> ingredients;

		// List of steps for cooking recipe.
		public readonly List<string> steps;

		/// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="dict"> Dictionary of recipe</param>
		/// <exception cref="RecipeException"> Wrong keys in dictionary.</exception>
		public Recipe(Dictionary<string,object> dict)
		{
			try
			{
				// Unpack data from dictionary.
                recipeId = (int)dict["recipe_id"];
                recipeName = (string)dict["recipe_name"];
                chef = (string)dict["chef"];
                cuisine = (string)dict["cuisine"];
                difficulty = (string)dict["difficulty"];
                ingredients = (List<string>)dict["ingredients"];
                steps = (List<string>)dict["steps"];
            }
			catch
			{
				throw new RecipeException("Wrong transferred has been getting in constructor");
			}
		}

		/// <summary>
		/// Indexator of recipe.
		/// </summary>
		/// <param name="name"> Name of field.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">Wrong field name.</exception>
		public object this[string name]
		{
			get
			{
				return name switch
				{
					"recipe_id" => recipeId,
					"recipe_name" => recipeName,
					"chef" => chef,
					"cuisine" => cuisine,
					"difficulty" => difficulty,
                    "ingredients" => ingredients,
                    "steps" => steps,
                    _ => throw new ArgumentException("Wrong name in endexator")

				};
			}
		}
	}
}

