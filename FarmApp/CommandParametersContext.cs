namespace FarmApp
{
    public class CommandParametersContext
    {
        public string? Operation {  get; set; }
        public BoxType BoxType { get; set; }
        public int Quantity { get; set; }

        public DateTime Date { get; set; }
    }
}
