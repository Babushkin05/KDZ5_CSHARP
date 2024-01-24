using System;
namespace Recipes
{
	/// <summary>
	/// Tools to change lists of recipes.
	/// </summary>
	public static class Tools
	{
		/// <summary>
		/// List filtration by field.
		/// </summary>
		/// <param name="selectedField">Field to filter.</param>
		/// <param name="nameToFilter">Filter by.</param>
		/// <param name="recipes">List to filter.</param>
		/// <returns> Filtered list.</returns>
		public static List<Recipe> Filtration(string selectedField, string? nameToFilter, List<Recipe> recipes)
		{
			// List to return.
			List<Recipe> filteredList = new List<Recipe>();

			// Filtration.
			for(int i = 0; i < recipes.Count; i++)
			{
				if ((string)recipes[i][selectedField] == nameToFilter)
					filteredList.Add(recipes[i]);
			}

			return filteredList;
		}

		/// <summary>
		/// Sort array by field.
		/// </summary>
		/// <param name="selectedField">Field to sort.</param>
		/// <param name="recipes">List to sort.</param>
		/// <param name="reversed">Is sorting should be reserved.</param>
		public static void Sorting(string selectedField, ref List<Recipe> recipes, bool reversed = false)
		{
			// Sorting.
			recipes.Sort(1, recipes.Count - 1, Comparer<Recipe>.Create((a, b) =>
				String.Compare(a[selectedField].ToString(), b[selectedField].ToString())));

			// Reversing.
			if (reversed)
				recipes.Reverse();
		}
	}
}

