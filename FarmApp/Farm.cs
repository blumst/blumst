namespace FarmApp
{
    public class Farm
    {
        private readonly List<Box> _boxes;

        public Farm(List<Box> boxes)
        {
            _boxes = boxes;
        }

        public void AddBox(BoxType boxType, int quantity)
        {
            var existingBox = _boxes.FirstOrDefault(x => x.BoxType == boxType);

            if(existingBox != null)
                existingBox.Quantity += quantity;
            else
            {
                var box = new Box { BoxType = boxType, Quantity = quantity, DateAdded = DateTime.Now};
                _boxes.Add(box);
            }
        }

        public bool RemoveBox(BoxType boxType, int quantity)
        {
            var existingBox = _boxes.FirstOrDefault(x => x.BoxType == boxType && x.Quantity >= quantity);

            if (existingBox == null || existingBox.Quantity < quantity)
                throw new InvalidOperationException("Not enough quantity in the box.");

            existingBox.Quantity -= quantity;

            if (existingBox.Quantity <= 0)
                _boxes.Remove(existingBox);

            return true;
        }

        public IEnumerable<Box> GetBoxes() => _boxes;

        public int GetTotalBoxes(BoxType boxtype)
        {  
           if (_boxes == null)
                return 0;

            return _boxes.Where(x => x.BoxType == boxtype).Sum(x => x.Quantity);
        }

        public int GetBoxesAddedOnDate(DateTime date) => _boxes.Where(x => x.DateAdded.Date == date.Date).Sum(x => x.Quantity);

        public Dictionary<DateTime, int> GetBoxCountByDate(BoxType boxType) => _boxes
            .Where(x => x.BoxType == boxType)
            .GroupBy(x => x.DateAdded.Date)
            .ToDictionary(i => i.Key, i => i.Sum(x => x.Quantity));
    }
}
