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

            const string add = "add";
            const string remove = "remove";

            string[] command = input.Split(' ');

            if (command.Length != 3)
                return false;

            string operation = command[0];

            if (!int.TryParse(command[2], out int quantity))
                return false;
            
            if (!Enum.TryParse(command[1], true, out BoxType boxType))
                return false;

            try
            {
                if (operation == add)
                    farm.AddBox(boxType, quantity);
                else if (operation == remove)
                    farm.RemoveBox(boxType, quantity);
                else
                    return false;

                printer.PrintInfo(operation, quantity, boxType, farm);

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