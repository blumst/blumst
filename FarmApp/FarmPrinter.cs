using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmApp
{
    public class FarmPrinter
    {
        public void PrintInfo(string operationType, int quantity, BoxType boxType, Farm farm)
        {
            if (operationType.ToLower().Trim() == "add")
                Console.WriteLine($"{quantity} boxes of {boxType} added to the farm.");
            else if (operationType.ToLower().Trim() == "remove")
                Console.WriteLine($"{quantity} boxes of {boxType} removed from the farm.");
            else
                throw new ArgumentException($"Unknown operation type - {operationType}.");

            PrintStorage(farm);
        }

        public void PrintStorage(Farm farm)
        {
            Console.WriteLine("\nCurrent storage status: ");
            Console.WriteLine($"Vegetable boxes: {farm.VeggieBoxes}");
            Console.WriteLine($"Fruit boxes: {farm.FruitBoxes}\n");
        }
    }
}
