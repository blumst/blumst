namespace FarmApp.Utilities
{
    public static class ConsoleHandlerUtility
    {
        public static bool HandleAddCommand(Farm farm, FarmPrinter printer, FarmFileManager fileManager, CommandParameters parameters)
        {
            farm.AddBox(parameters.BoxType, parameters.Quantity);
            LogAndSave(printer, fileManager, farm, parameters.Operation!, parameters.Quantity, parameters.BoxType);
            return true;
        }

        public static bool HandleRemoveCommand(Farm farm, FarmPrinter printer, FarmFileManager fileManager, CommandParameters parameters)
        {
            bool removed = farm.RemoveBox(parameters.BoxType, parameters.Quantity);

            if (removed)
            {
                LogAndSave(printer, fileManager, farm, parameters.Operation!, parameters.Quantity, parameters.BoxType);
                return true;
            }
            
            return false;
        }

        private static void LogAndSave(FarmPrinter printer, FarmFileManager fileManager, Farm farm, string operation, int quantity, BoxType boxType)
        {
            printer.PrintInfo(operation, quantity, boxType, farm);
            fileManager.SaveToFile(farm);
        }
    }
}

