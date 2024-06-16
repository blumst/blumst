namespace FarmApp
{
    public static class ConsoleParserUtility
    {
        public const string Add = "add";
        public const string Remove = "remove";
        public const string BoxAdded = "boxadded";
        public const string BoxCountByDate = "boxcountbydate";

        public static bool TryParseCommandParameters(string input, out CommandParametersContext parameters)
        {
            parameters = null!;
            
            string[] command = input.Split(' ');

            return command[0] switch
            {
                Add or Remove => TryParseAddRemoveCommands(command, out parameters),
                BoxAdded => TryParseBoxesAddedCommand(command, out parameters),
                BoxCountByDate => TryParseBoxCountByDateCommand(command, out parameters),
                _ => false,
            };
        }

        private static bool TryParseAddRemoveCommands(string[] command, out CommandParametersContext parameters)
        {
            parameters = null!;

            if(command.Length != 3 || !int.TryParse(command[2], out int quantity) || !Enum.TryParse(command[1], true, out BoxType boxtype))
                return false;

            parameters = new CommandParametersContext()
            {
                Operation = command[0],
                BoxType = boxtype,
                Quantity = quantity
            };

            return true;
        }

        private static bool TryParseBoxesAddedCommand(string[] command, out CommandParametersContext parameters)
        {
            parameters = null!;

            if (command.Length != 2 || !DateTime.TryParse(command[1], out DateTime date))
                return false;

            parameters = new CommandParametersContext()
            {
                Operation = BoxAdded,
                Date = date
            };

            return true;
        }

        private static bool TryParseBoxCountByDateCommand(string[] command, out CommandParametersContext parameters)
        {
            parameters = null!;

            if(command.Length != 2 || !Enum.TryParse(command[1], true, out BoxType boxType))
                return false;

            parameters = new CommandParametersContext()
            {
                Operation = BoxCountByDate,
                BoxType = boxType
            };

            return true;
        }
    }
}
