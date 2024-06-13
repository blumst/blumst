namespace FarmApp
{
    public static class ConsoleParserUtility
    {
        public const string add = "add";
        public const string remove = "remove";
        public const string boxAdded = "boxadded";
        public const string boxCountByDate = "boxcountbydate";

        public static bool CommandParser(string input, out CommandParameters parameters)
        {
            parameters = null!;
            
            string[] command = input.Split(' ');

            switch (command[0])
            {
                case add or remove:
                    return AddRemoveCommandsParser(command, out parameters);
                case boxAdded:
                    return BoxesAddedCommandParser(command, out parameters);
                case boxCountByDate:
                    return BoxCountByDateCommandParser(command, out parameters);
                default:
                    return false;
            } 
        }

        private static bool AddRemoveCommandsParser(string[] command, out CommandParameters parameters)
        {
            parameters = null!;

            if(command.Length != 3 || !int.TryParse(command[2], out int quantity) || !Enum.TryParse(command[1], true, out BoxType boxtype))
                return false;

            parameters = new CommandParameters()
            {
                Operation = command[0],
                BoxType = boxtype,
                Quantity = quantity
            };

            return true;
        }

        private static bool BoxesAddedCommandParser(string[] command, out CommandParameters parameters)
        {
            parameters = null!;

            if (command.Length != 2 || !DateTime.TryParse(command[1], out DateTime date))
                return false;

            parameters = new CommandParameters()
            {
                Operation = boxAdded,
                Date = date
            };

            return true;
        }

        private static bool BoxCountByDateCommandParser(string[] command, out CommandParameters parameters)
        {
            parameters = null!;

            if(command.Length != 2 || !Enum.TryParse(command[1], true, out BoxType boxType))
                return false;

            parameters = new CommandParameters()
            {
                Operation = boxCountByDate,
                BoxType = boxType
            };

            return true;
        }
    }
}
