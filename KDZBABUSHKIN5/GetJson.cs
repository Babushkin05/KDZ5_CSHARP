using System;
using Recipes;
using System.Text;
using System.Xml.Linq;

namespace KDZBABUSHKIN5
{
    /// <summary>
    /// Class to communicate with user.
    /// </summary>
	public static partial class UserInteraction
	{
        /// <summary>
        /// Gets list of recipes from user.
        /// </summary>
        /// <returns>List of recipes.</returns>
        public static List<Recipe> GetJson()
        {
            // User choose way to deliviery Json.
            int choosen = Slider("How to deliever Json?", new string[] { "Take data from file", "Take data from Terminal" });
            Console.Clear();

            string path;
            // Taking data from file.
            if (choosen == 0)
            {
                // Getting correct filename.
                do
                {
                    Console.Write("Type name of file to read (with '.json') :: ");
                    char sep = Path.DirectorySeparatorChar;
                    path = $"..{sep}..{sep}..{sep}..{sep}{Console.ReadLine()}";
                } while (!File.Exists(path));
            }
            // Taking data from terminal.
            else
            {
                // Collecting json from terminal.
                Console.WriteLine("Input json data (type 'END' in last string):: ");
                StringBuilder json = new StringBuilder();

                string? readed;
                do
                {
                    readed = Console.ReadLine() + '\n';
                    json.Append(readed);
                } while (readed != "END\n");

                json = json.Remove(json.Length - 5, 5);
                path = "support.json";

                // Saving it in supported file.
                File.WriteAllText(path, json.ToString());
                Console.Clear();
            }

            // List to return.
            List<Recipe> recipes = new List<Recipe>();

            // Try's read json from file.
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    Console.SetIn(reader);
                    recipes = JsonParser.ReadJSON();
                }
            }

            // Reapet getting json if incorrect json in file.
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.WriteLine("Type some key to reapet");
                Console.ReadKey();
                GetJson();
            }

            return recipes;

        }
    }
}

