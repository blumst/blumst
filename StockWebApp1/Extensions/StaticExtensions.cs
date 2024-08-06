namespace StockWebApp1.Extensions
{
    public static class StaticExtensions
    {
        public static void EnsureFound<T>(this T obj, string message = "Resource not found") /*where T : class*/
        {
            if(obj == null)
                throw new NotFoundException(message);
        }
    }

    public class NotFoundException(string message) : Exception(message)
    {

    }
}
