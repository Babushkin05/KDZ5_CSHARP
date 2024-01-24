namespace Recipes;
/// <summary>
/// Class to parse json.
/// </summary>
public static  partial class JsonParser
{
    /// <summary>
    /// Exception of class.
    /// </summary>
    class JSONParserException : Exception
    {
        public JSONParserException(string message)
            : base(message) { }
    }

    /// <summary>
    /// Read's json.
    /// </summary>
    /// <returns> List of recipes.</returns>
    /// <exception cref="JSONParserException"> Wrong construction of json.</exception>
    public static List<Recipe> ReadJSON()
    {
        // List to return.
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

        // Dictionary to add in list.
        Dictionary<string, object> dict = new Dictionary<string, object>();

        // Support list for ingredients and steps.
        List<string> strings = new List<string>();

        // Stack to control parenthesis sequence.
        Stack<char> stack = new Stack<char>();

        // Parts of reading json object. There is a photo to understand in directory of project.
        int partOfReading = 0;

        // Reading first char.
        char ch = (char)Console.Read();
        if (ch != '[')
            throw new JSONParserException("This is not array of recipes");

        stack.Push(ch);

        string savedString = "";
        string currentString = "";

        // Reading chars in cycle and process them.
        while (stack.Count>0)
        {
            // Read char.
            ch = (char)Console.Read();

            switch (partOfReading)
            {
                // Beginning of object.
                case 0:
                    // Skip useless chars.
                    if (ch == ' ' || ch == '\n'|| ch==',')
                        break;
                    // Begin new object.
                    if (ch == '{')
                    {
                        stack.Push(ch);

                        // Go to next part.
                        partOfReading++;
                    }

                    // Close old object.
                    else if (ch == '}')
                    {
                        stack.Pop();
                    }

                    // Close array.
                    else if (ch == ']')
                        stack.Pop();

                    break;

                // Key for dictionary.
                case 1:
                case 3:
                case 5:
                case 7:
                case 9:
                case 11:
                case 13:

                    // Catching breaking line.
                    if (currentString.Length > 0)
                    {
                        if (ch == '\n' && currentString[^1] != '"')
                            throw new JSONParserException("Line break in line");
                    }
                    // Add char to string.
                    currentString += ch;

                    // Key filled up.
                    if (ch == ':')
                    {
                        // Trim extra chars.
                        currentString = currentString.Trim(new char[] { '\n', ' ', ',', ':', '"' });

                        // Compare with keys.
                        savedString = partOfReading switch
                        {
                            1 => "recipe_id",
                            3 => "recipe_name",
                            5 => "chef",
                            7 => "cuisine",
                            9 => "difficulty",
                            11 => "ingredients",
                            13 => "steps",
                            _ => throw new JSONParserException("Something went wrong..")

                        };

                        // Wrong key.
                        if (currentString != savedString) throw new JSONParserException($"Wrong construction, must be {savedString}," +
                            $" but {currentString[..^1]}");

                        // Go to next part.
                        currentString = "";
                        partOfReading++;
                    }

                    break;

                // Integer.
                case 2:

                    // Fill digit.
                    if (char.IsDigit(ch))
                        currentString += ch;

                    // Integer ended.
                    else if (ch == ',')
                    {
                        // Go to next part.
                        partOfReading++;

                        int number;
                        if (!int.TryParse(currentString, out number))
                            throw new JSONParserException($"Wrong integer : {currentString}");
                        // Save idex in dict.
                        dict[savedString] = number;
                        currentString = "";
                    }

                    break;

                // String.
                case 4:
                case 6:
                case 8:
                case 10:
                    // String ended.
                    if (ch == ',' && currentString[^1] == '"')
                    {
                        // Trim useless chars.
                        dict[savedString] = currentString.Trim(new char[] { '\n', ' ', ',', ':', '"' });
                        currentString = "";

                        // Go to next Part.
                        partOfReading++;
                    }

                    // Fill chars in string.
                    if (currentString.Length > 0 || ch == '"')
                        currentString += ch;

                    break;

                // Arrays.
                case 12:
                case 14:

                    // Begining of array.
                    if (ch == '[')
                    {
                        // Wrong Construction.
                        if (stack.Count != 2)
                            throw new JSONParserException("Wrong Construction");

                        stack.Push(ch);
                    }

                    // Fill string.
                    else if ((currentString.Length>0 || ch == '"') && stack.Count == 3)
                    {
                        // Catch breaking in line.
                        if (ch == '\n' && currentString[0] == '"' && currentString[^1] != '"')
                            throw new JSONParserException("Line break in line");

                        // Fill chars in string.
                        currentString += ch;

                        // String end.
                        if (ch == ',')
                        {
                            // Add string to list.
                            strings.Add(currentString.Trim(new char[] {'\n',' ',',',':','"'}));
                            currentString = "";
                        }

                        // Array ended.
                        else if (ch == ']')
                        {
                            // Catch wrong parenthesisi sequence.
                            if (stack.Peek() != '[' || stack.Count != 3)
                                throw new JSONParserException("Incorrect parenthesis sequence");

                            stack.Pop();

                            // Add last string to list.
                            strings.Add(currentString.Trim(new char[] { '\n', ' ', ',', ':', '"',']' }));
                            currentString = "";
                            dict[savedString] = new List<string>(strings);

                            // Clear list for new strings.
                            strings.Clear();

                            // If last array of object.
                            if (partOfReading == 14)
                            {
                                // Catch exception.
                                if (stack.Count() != 2)
                                    throw new JSONParserException("Wrong construction");

                                // Go to next part.
                                partOfReading++;
                            }
                        }
                    }
                    // End of this array.
                    else if (ch == ',')
                    {
                        // catch exception.
                        if (stack.Count() != 2)
                            throw new JSONParserException("Wrong construction");

                        // Go to next Part.
                        partOfReading++;
                    }

                    break;

                // End of object.
                case 15:

                    // Useless chars.
                    if (ch == ' ' || ch == '\n')
                        break;

                    // Ending of object.
                    if (ch == '}')
                    {
                        stack.Pop();

                        // Go to next part.
                        partOfReading = 0;

                        // Add dict to list of dictionary'es.
                        list.Add(new Dictionary<string, object>(dict));
                        dict.Clear();
                    }

                    // Catch incorrect contruction of json.
                    else
                        throw new JSONParserException("Wrong Construction");
                    break;
            }
        }

        // List to return.
        List<Recipe> recipes = new List<Recipe>();
        foreach (var el in list)
        {
            recipes.Add(new Recipe(el));
        }
        // Setting standart input again.
        StreamReader standartInput = new StreamReader(Console.OpenStandardInput());
        Console.SetIn(standartInput);

        return recipes;

    }

}


