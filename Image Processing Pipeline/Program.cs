using Image_Processing_Pipeline.Classes;
using Image_Processing_Pipeline.Classes.Steps;
using Image_Processing_Pipeline.Interfaces;

namespace Image_Processing_Pipeline
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using CancellationTokenSource cancelTokenSource = new();
            CancellationToken token = cancelTokenSource.Token;

            var pipeline = new ImageProcessingPipeline();

            string imageName = UserInputHandler.GetImageName();

            if (imageName == null)
                return;

            IPipelineContext context = new Picture
            {
                ImageName = imageName,
            };

            pipeline.AddStep(new ImageLoadingStep());

            while (true)
            {
                string choice = UserInputHandler.GetUserChoice()!;

                if (choice == "6")
                    break;

                HandleUserChoice(choice, pipeline, context);
            }

            pipeline.AddStep(new ImageSavingStep());

            try
            {
                await pipeline.ExecutePipelineAsync(context, token);
                Console.WriteLine("\nImage processing completed.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred during pipeline execution: {ex.Message}");
            }
        }

        private static void HandleUserChoice(string choice, ImageProcessingPipeline pipeline, IPipelineContext context)
        {
            try
            {
                switch (choice)
                {
                    case "1":
                        pipeline.AddStep(new ImageFilteringStep());
                        Console.WriteLine("\nFilter added.\n");
                        break;
                    case "2":
                        int width = UserInputHandler.GetIntegerInput("\nEnter the width to resize:");
                        int height = UserInputHandler.GetIntegerInput("\nEnter the height to resize:");

                        context.Width = width;
                        context.Height = height;

                        pipeline.AddStep(new ImageResizingStep());
                        Console.WriteLine("\nImage resized.\n");
                        break;
                    case "3":
                        pipeline.AddStep(new ImageReversingStep());
                        Console.WriteLine("\nImage reversed.\n");
                        break;
                    case "4":
                        int degrees = UserInputHandler.GetIntegerInput("\nEnter the degrees to rotate:");

                        context.Degrees = degrees;

                        pipeline.AddStep(new ImageRotatingStep());
                        Console.WriteLine("\nImage rotated.\n");
                        break;
                    case "5":
                        string watermark = UserInputHandler.GetStringInput("\nEnter the watermark text:");

                        context.Watermark = watermark;

                        pipeline.AddStep(new ImageWatermarkingStep());
                        Console.WriteLine("\nWatermark added.\n");
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice, please select a valid option.\n");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred: {ex.Message}");
            }
        }
    }
}   