namespace FarmApp
{
    public enum BoxType
    {
        Fruit,
        Vegetable
    }

    public class Farm
    {
        public int FruitBoxes { get; set; }
        public int VeggieBoxes { get; set; }

        public Farm()
        {
            FruitBoxes = 0;
            VeggieBoxes = 0;
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
