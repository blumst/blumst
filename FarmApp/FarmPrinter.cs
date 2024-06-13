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
                Console.WriteLine($"\n{quantity} boxes of {boxType} added to the farm.");
            else if (operationType.ToLower().Trim() == "remove")
                Console.WriteLine($"\n{quantity} boxes of {boxType} removed from the farm.");
            else
                throw new ArgumentException($"Unknown operation type - {operationType}.");

            PrintStorage(farm);
        }

        public void PrintStorage(Farm farm)
        {
            Console.WriteLine("\nCurrent storage status: ");
            Console.WriteLine($"Vegetable boxes: {farm.GetTotalBoxes(BoxType.Vegetable)}");
            Console.WriteLine($"Fruit boxes: {farm.GetTotalBoxes(BoxType.Fruit)}\n");
        }

        public void PrintBoxesAddedOnDate(Farm farm, DateTime date)
        {
            int boxes = farm.GetBoxesAddedOnDate(date);
            Console.WriteLine($"\nBoxes added on {date.ToShortDateString()}: {boxes}\n");
        }

        public void PrintBoxesCountByDate(Farm farm, BoxType boxType)
        {
            var boxes = farm.GetBoxCountByDate(boxType);
            Console.WriteLine($"\nNumber of boxes for {boxType} by date: ");
            foreach ( var box in boxes)
                Console.WriteLine($"{box.Key.ToShortDateString()}: {box.Value} boxes\n");
        }
    }
}
