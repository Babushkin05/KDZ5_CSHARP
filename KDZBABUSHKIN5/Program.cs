using Recipes;
namespace KDZBABUSHKIN5
{
    /// <summary>
    /// Main class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Method that start program.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Reapeting of solution.
            do
            {
                // Clear Console for new solution.
                Console.Clear();

                // Getting JSon.
                List<Recipe> recipes = UserInteraction.GetJson();

                // Make Actions with recepts.
                UserInteraction.HandleHub(recipes);

                // Reapeting of solution.
                Console.Write("press 'y' to reapet or another button to stop ::  ");
            } while (Console.ReadKey().Key == ConsoleKey.Y);

        }
    }
}