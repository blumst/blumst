using Image_Processing_Pipeline.Classes;
using Image_Processing_Pipeline.Interfaces;

namespace Image_Processing_Pipeline
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var pipeline = new ImageProcessingPipeline();            

            string imageName = UserInputHandler.GetImageName();

            if (imageName == null)
                return;

            IPipelineContext context = new Picture
            {
                ImageName = imageName,
            };

            var stepHandler = new StepHandler(pipeline, context);

            pipeline.AddStep(new ImageLoadingStep());

            while (true)
            {
                string choice = UserInputHandler.GetUserChoice()!;

                if (choice == "6")
                    break;

                stepHandler.HandleUserChoice(choice);
            }

            pipeline.AddStep(new ImageSavingStep());

            try
            {
                await pipeline.ExecutePipelineAsync(context);
                Console.WriteLine("\nImage processing completed.");

            } catch (Exception ex)
            {
                Console.WriteLine($"\nAn error occurred during pipeline execution: {ex.Message}");
            }
        }
    }
}