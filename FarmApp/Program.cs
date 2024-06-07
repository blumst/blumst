namespace FarmApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var printer = new FarmPrinter();
            var fileManager = new FarmFileManager();

            var farmData = fileManager.LoadFromFile();

            var farm = new Farm(farmData.FruitBoxes, farmData.VeggieBoxes);
            

            Console.WriteLine("Do you want to see current storage? y/n");

            if (Console.ReadLine().ToLower() == "y")
                printer.PrintStorage(farm);
            
            
            Console.WriteLine("Avaliable box types: \n fruit \n vegetable \n");
            Console.WriteLine("Enter a command (add [box type] [quantity] / remove [box type] [quantity] / exit): ");

            string input;
            
            while ((input = Console.ReadLine().ToLower()) != "exit")
            {
                if (!string.IsNullOrEmpty(input))
                {
                    var result = ProcessCommand(input, farm, printer, fileManager);
                    if (!result)
                        Console.WriteLine("\nInvalid command, box type or quantity. Try again.\n");
                } 
                else
                    Console.WriteLine("Input cannot be emty. Enter a valid command.");

                Console.WriteLine("Enter a command (add [box type] [quantity] / remove [box type] [quantity] / exit): ");
            }

            Console.WriteLine("Exiting the program...");
        }

        static bool ProcessCommand(string input, Farm farm, FarmPrinter printer, FarmFileManager fileManager)
        {
            if(!ConsoleUtility.CommandParser(input, out CommandParameters parameters))
                return false;

            try
            {
                if (parameters.Operation == ConsoleUtility.add)
                    farm.AddBox(parameters.BoxType, parameters.Quantity);
                else if (parameters.Operation == ConsoleUtility.remove)
                    farm.RemoveBox(parameters.BoxType, parameters.Quantity);
                else
                    return false;

                printer.PrintInfo(parameters.Operation, parameters.Quantity, parameters.BoxType, farm);

                fileManager.SaveToFile(farm);

                return true;

            } catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}