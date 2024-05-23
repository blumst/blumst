namespace FarmApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var farm = new Farm();

            Console.WriteLine("Avaliable box types: \n fruit \n vegetable \n");
            Console.WriteLine("Enter a command (add [box type] [quantity] / remove [box type] [quantity] / exit): ");

            string input;
            
            while ((input = Console.ReadLine().ToLower()) != "exit")
            {
                if (!string.IsNullOrEmpty(input))
                {
                    var result = ProcessCommand(input, farm);
                    if (!result)
                        Console.WriteLine("Invalid command, box type or quantity. Try again.");
                } 
                else
                    Console.WriteLine("Input cannot be emty. Enter a valid command.");

                Console.WriteLine("Enter a command (add [box type] [quantity] / remove [box type] [quantity] / exit): ");
            }

            Console.WriteLine("Exiting the program...");
        }

        static bool ProcessCommand(string input, Farm farm)
        {
            string[] command = input.Split(' ');

            if (command.Length != 3)
                return false;

            if (!int.TryParse(command[2], out int quantity))
                return false;

            string operation = command[0];
            string boxType = command[1];

            if (boxType != "fruit" && boxType != "vegetable")
                return false;

            try
            {
                if (operation == "add")
                    farm.AddBox(boxType, quantity);
                else if (operation == "remove")
                    farm.RemoveBox(boxType, quantity);
                else
                    return false;

                return true;

            } catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return true;
        }
    }
}