namespace FarmApp
{
    public class CommandParameters
    {
        public string? Operation {  get; set; }
        public BoxType BoxType { get; set; }
        public int Quantity { get; set; }

        public DateTime Date { get; set; }
    }
}
