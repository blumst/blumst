namespace FarmApp
{
    public class Farm
    {
        private int _fruitBoxes;
        private int _veggieBoxes;

        public Farm()
        {
            _fruitBoxes = 0;
            _veggieBoxes = 0;
        }

        public void AddBox(string boxType, int quantity)
        {
            try
            {
                switch(boxType.ToLower().Trim())
                {
                    case "fruit":
                        _fruitBoxes += quantity;
                        break;
                    case "vegetable":
                        _veggieBoxes += quantity;
                        break;
                    default:
                        throw new ArgumentException($"Unknown type of box - {boxType}.");
                }

                PrintInfo("add", quantity, boxType);

            } catch (Exception ex)
            {
                Console.WriteLine($"An error occured while adding boxes: {ex.Message}");
            }
        }

        public void RemoveBox(string boxType, int quantity)
        {
            try
            {
                switch (boxType.ToLower().Trim())
                {
                    case "fruit":
                        if (_fruitBoxes >= quantity)
                            _fruitBoxes -= quantity;
                        else
                            throw new InvalidOperationException($"Not enough {boxType} boxes to remove.");
                        break;
                    case "vegetable":
                        if (_veggieBoxes >= quantity)
                            _veggieBoxes -= quantity;
                        else
                            throw new InvalidOperationException($"Not enough {boxType} boxes to remove.");
                        break;
                    default:
                        throw new ArgumentException("Unknown type of box.");
                }

                PrintInfo("remove", quantity, boxType);


            } catch (Exception ex)
            {
                Console.WriteLine($"An error occured while removing boxes: {ex.Message}");
            }

        }

        public void PrintInfo(string operationType, int quantity, string boxType)
        {
            if (operationType.ToLower().Trim() == "add")
                Console.WriteLine($"{quantity} boxes of {boxType} added to the farm.");
            else if (operationType.ToLower().Trim() == "remove")
                Console.WriteLine($"{quantity} boxes of {boxType} removed from the farm.");
            else
                throw new ArgumentException($"Unknown operation type - {operationType}.");

            PrintStorage();
        }

        public void PrintStorage()
        {
            Console.WriteLine("Current storage status: ");
            Console.WriteLine($"Vegetable boxes: {_veggieBoxes}.");
            Console.WriteLine($"Fruit boxes: {_fruitBoxes}.");
        }
    }
}
