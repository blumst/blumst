namespace Image_Processing_Pipeline.Classes
{
    public class UserInputHandler
    {
        public static string GetImageName()
        {
            string imageName;

            while (true)
            {
                Console.WriteLine("Enter the name of the image (without extension) or type 'exit' to quit:");
                imageName = Console.ReadLine()!;

                if (imageName.ToLower() == "exit")
                    return null;

                if (File.Exists(imageName + ".jpg"))
                    return imageName;
                else
                    Console.WriteLine($"File {imageName}.jpg does not exist. Please try again. \n");
            }
        }

        public static int GetIntegerInput(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;

                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        public static string GetStringInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine()!;
        }

        public static string GetUserChoice()
        {
            Console.WriteLine("\nChoose option: ");
            Console.WriteLine("1 - Add filter");
            Console.WriteLine("2 - Resize");
            Console.WriteLine("3 - Reverse");
            Console.WriteLine("4 - Rotate");
            Console.WriteLine("5 - Add watermark");
            Console.WriteLine("6 - Exit\n");

            return Console.ReadLine()!;
        }
    }
}
