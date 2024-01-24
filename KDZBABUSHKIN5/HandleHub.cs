using System;
using Recipes;
namespace KDZBABUSHKIN5
{
	/// <summary>
	/// Class to communicate with user.
	/// </summary>
	public static partial class UserInteraction
	{
		/// <summary>
		/// Handle Actions with list of recipes.
		/// </summary>
		/// <param name="recipes"> List of recipes.</param>
		public static void HandleHub(List<Recipe> recipes)
		{
			// Choosing action.
            int choosen = Slider("Actions with recipes:", new string[] { "Sorting", "Filtration", "Saving", "Exit menu" });

			// Exit choosen.
			if (choosen == 3) return;

			string[] keys;
			int choosenInd;

			// Switch actions.
			switch (choosen)
			{
				// Sorting.
				case 0:

					// Fields to sort.
					keys = new string[] { "recipe_id", "recipe_name", "chef","cuisine"};

					choosenInd = Slider("Field for sorting", keys);

					// Is reversed sorting.
					int isReversed = Slider("Reversed sorting?",new string[] { "Yes","No" });

					// Sorting.
					Tools.Sorting(keys[choosenInd], ref recipes, isReversed == 0);
					break;

				// Filtration.
				case 1:
					// Fields to filter.
                    keys = new string[] { "cuisine","difficulty" };
					choosenInd = Slider("Filter by :", keys);
					string field = keys[choosenInd];

					string nameToFilter;
					// Filter cousine by: 
					if (choosenInd == 0)
					{
						keys = new string[] { "Japanese", "Italian", "Indian", "Mexican" };
						choosenInd = Slider("Cuisine:", keys);
						nameToFilter = keys[choosenInd];
					}
					// Filter difficulty by:
					else
					{
                        keys = new string[] { "Easy", "Intermediate", "Advanced" };
                        choosenInd = Slider("Difficulty:", keys);
                        nameToFilter = keys[choosenInd];
                    }

					// Filtration.
					recipes = Tools.Filtration(field, nameToFilter, recipes);
					break;

				// Saving.
				case 2:

					// Getting name of file to save.
					string? path;
					do
					{
                        Console.Write("Type name to save json (with '.json') ::  ");
						path = Console.ReadLine();
					} while (!IsCorrectFileName(path));

                    char sep = Path.DirectorySeparatorChar;
                    path = $"..{sep}..{sep}..{sep}..{sep}{path}";

					// Savind.
					using (StreamWriter writer = new StreamWriter(path))
					{
						Console.SetOut(writer);
						JsonParser.WriteJson(recipes);
					}

					// Set standart output again.
					StreamWriter standartOutput = new StreamWriter(Console.OpenStandardOutput());
					Console.SetOut(standartOutput);
					return;


            }

			// Write's curretn list.
            JsonParser.WriteJson(recipes);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

			// Reapeting actions menu.
            HandleHub(recipes);
        }

		/// <summary>
		/// Check file name.
		/// </summary>
		/// <param name="Name"> Name to check.</param>
		/// <returns> Is name correct.</returns>
        public static bool IsCorrectFileName(string? name)
        {
			// Checking invalid chars in name.
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char chr in name)
            {
                if (invalidChars.Contains(chr))
                {
                    return false;
                }
            }

			// Right type.
            return name[^5..] == ".json";
        }
    }

}

