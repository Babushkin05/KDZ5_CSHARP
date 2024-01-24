using System;
namespace Recipes
{
	/// <summary>
	/// Class to parse json.
	/// </summary>
	public static partial class JsonParser
	{
		/// <summary>
		/// Writes list of recipes in json format.
		/// </summary>
		/// <param name="list">List of recipes.</param>
		public static void WriteJson(List<Recipe> list)
		{
			Console.Clear();

			// Tabulation.
			int tab = 1;

			// Begin array.
			Console.WriteLine("[");
			foreach(var recipe in list)
			{
				// Tabulation.
				for (int i = 0; i < tab; i++) Console.Write("  ");

				// Beginning of object.
				Console.WriteLine('{');

				// List of fields in recipe.
				List<string> keys = new List<string> { "recipe_id", "recipe_name", "chef","cuisine",
					"difficulty","ingredients", "steps" };

				// Go to next level.
				tab++;
				for(int i = 0; i < keys.Count; i++)
				{
					// Tabulation.
                    for (int j = 0; j < tab; j++) Console.Write("  ");

					// Key.
					Console.Write($"\"{keys[i]}\": ");

					// Value.
					if (i == 0)
						Console.WriteLine($"{recipe[keys[0]]},");
					else if(i<keys.Count-2)
						Console.WriteLine($"\"{recipe[keys[i]]}\",");

					// Array.
					else
					{
						// Begining of array.
						Console.WriteLine("[");

						tab++;

						foreach(var str in (List<string>)recipe[keys[i]])
						{
							// Tabulation.
                            for (int j = 0; j < tab; j++) Console.Write("  ");

							// Array element.
							Console.Write($"\"{str}\"");
							// Comma.
							if (str != ((List<string>)recipe[keys[i]])[^1])
								Console.Write(",");
							Console.Write("\n");
                        }

						// Tabulation.
						tab--;
                        for (int j = 0; j < tab; j++) Console.Write("  ");

						// Ending of array.
						Console.Write(']');

						// Comma.
						if (i != keys.Count - 1)
							Console.Write(',');
						Console.Write('\n');
                    }
				}

				// Tabulation.
				tab--;
                for (int i = 0; i < tab; i++) Console.Write("  ");

				// Ending of object.
				Console.Write('}');

				// Comma.
				if (recipe != list[^1])
					Console.Write(',');
				Console.Write('\n');
            }

			// Ending of list.
			Console.WriteLine(']');
		}
    }
}

