namespace FarmApp
{
    public class ConsoleUtility
    {
        public const string add = "add";
        public const string remove = "remove";

        public static bool CommandParser(string input, out string operation, out BoxType boxType, out int quantity)
        {
            operation = string.Empty;
            boxType = default;
            quantity = 0; 
            
            string[] command = input.Split(' ');

            if (command.Length != 3)
                return false;

            operation = command[0];

            if (!int.TryParse(command[2], out quantity))
                return false;

            if (!Enum.TryParse(command[1], true, out boxType))
                return false;

            return true;
        }
    }
}
