namespace FarmApp
{
    public class ConsoleUtility
    {
        public const string add = "add";
        public const string remove = "remove";

        public static bool CommandParser(string input, out CommandParameters parameters)
        {
            parameters = null!;
            
            string[] command = input.Split(' ');

            if (command.Length != 3)
                return false;


            if (!int.TryParse(command[2], out int quantity))
                return false;

            if (!Enum.TryParse(command[1], true, out BoxType boxType))
                return false;

            parameters = new CommandParameters()
            {
                Operation = command[0],
                BoxType = boxType,
                Quantity = quantity
            };

            return true;
        }
    }
}
