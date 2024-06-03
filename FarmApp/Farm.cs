namespace FarmApp
{
    public class Farm
    {
        public int FruitBoxes { get; private set; }
        public int VeggieBoxes { get; private set; }

        public Farm(int fruitBoxes, int veggieBoxes)
        {
            FruitBoxes = fruitBoxes;
            VeggieBoxes = veggieBoxes;
        }

        public void AddBox(BoxType boxType, int quantity)
        {
            switch(boxType)
            {
                case BoxType.Fruit:
                    FruitBoxes += quantity;
                    break;
                case BoxType.Vegetable:
                    VeggieBoxes += quantity;
                    break;
                default:
                    throw new ArgumentException($"Unknown type of box - {boxType}.");
            }
        }

        public void RemoveBox(BoxType boxType, int quantity)
        {
            switch (boxType)
            {
                case BoxType.Fruit:
                    if (FruitBoxes >= quantity)
                        FruitBoxes -= quantity;
                    else
                        throw new InvalidOperationException($"Not enough {boxType} boxes to remove.");
                    break;
                case BoxType.Vegetable:
                    if (VeggieBoxes >= quantity)
                        VeggieBoxes -= quantity;
                    else
                        throw new InvalidOperationException($"Not enough {boxType} boxes to remove.");
                    break;
                default:
                    throw new ArgumentException("Unknown type of box.");
            }
        }
    }
}
