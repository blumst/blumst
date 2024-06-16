using FarmApp.Utilities;

namespace FarmApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var printer = new FarmPrinter();
            var fileManager = new FarmFileManager();

            var farmData = fileManager.LoadFromFile();

            var farm = new Farm(farmData.Boxes ?? new List<Box>());


            Console.WriteLine("Do you want to see current storage? y/n");

            if (Console.ReadLine()?.ToLower() == "y")
                printer.PrintStorage(farm);
            
            
            Console.WriteLine("Avaliable box types: \n fruit \n vegetable \n");
            Console.WriteLine("Enter a command (add [box type] [quantity] / remove [box type] [quantity] / boxadded [date] / boxcountbydate [box type] / exit): ");

            string input;
            
            while ((input = Console.ReadLine()!.ToLower()) != "exit")
            {
                if (!string.IsNullOrEmpty(input))
                {
                    var result = ProcessCommand(input, farm, printer, fileManager);
                    if (!result)
                        Console.WriteLine("\nInvalid command. Try again.\n");
                } 
                else
                    Console.WriteLine("Input cannot be emty. Enter a valid command.");

                Console.WriteLine("Enter a command (add [box type] [quantity] / remove [box type] [quantity] / boxadded [date (dd.mm.yyyy) ] / boxcountbydate [box type] / exit): ");
            }

            Console.WriteLine("Exiting the program...");
        }

        static bool ProcessCommand(string input, Farm farm, FarmPrinter printer, FarmFileManager fileManager)
        {
            if(!ConsoleParserUtility.TryParseCommandParameters(input, out CommandParametersContext parameters))
                return false;

            try
            {
                bool result = false;

                switch (parameters.Operation)
                {
                    case ConsoleParserUtility.Add:
                        result = ConsoleHandlerUtility.HandleAddCommand(farm, printer, fileManager, parameters);
                        break;

                    case ConsoleParserUtility.Remove:
                        result = ConsoleHandlerUtility.HandleRemoveCommand(farm, printer, fileManager, parameters);
                        
                        if (!result)
                            Console.WriteLine($"Not enough boxes of {parameters.BoxType} available.\n");
                        break;

                    case ConsoleParserUtility.BoxAdded:
                        printer.PrintBoxesAddedOnDate(farm, parameters.Date);
                        result = true;
                        break;

                    case ConsoleParserUtility.BoxCountByDate:
                        printer.PrintBoxesCountByDate(farm, parameters.BoxType);
                        result = true;
                        break;

                    default:
                        return false;
                }

                return result;

            } catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}