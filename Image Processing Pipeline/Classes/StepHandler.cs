using Image_Processing_Pipeline.Classes.Steps;
using Image_Processing_Pipeline.Interfaces;

namespace Image_Processing_Pipeline.Classes
{
    public class StepHandler
    {
        private readonly ImageProcessingPipeline _pipeline;
        private readonly IPipelineContext _context;

        public StepHandler(ImageProcessingPipeline pipeline, IPipelineContext context)
        {
            _pipeline = pipeline;
            _context = context;
        }

        public void HandleUserChoice(string choice)
        {
            try
            {
                switch (choice)
                {
                    case "1":
                        AddFilterStep();
                        break;
                    case "2":
                        AddResizeStep();
                        break;
                    case "3":
                        AddReverseStep();
                        break;
                    case "4":
                        AddRotateStep();
                        break;
                    case "5":
                        AddWatermarkStep();
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

        private void AddFilterStep()
        {
            _pipeline.AddStep(new ImageFilteringStep());
            Console.WriteLine("\nFilter added.\n");
        }

        private void AddResizeStep()
        {
            var userInputHandler = new UserInputHandler();
            int width = UserInputHandler.GetIntegerInput("\nEnter the width to resize:");
            int height = UserInputHandler.GetIntegerInput("\nEnter the height to resize:");

            _context.Width = width;
            _context.Height = height;

            _pipeline.AddStep(new ImageResizingStep());
            Console.WriteLine("\nImage resized.\n");
        }

        private void AddReverseStep()
        {
            _pipeline.AddStep(new ImageReversingStep());
            Console.WriteLine("\nImage reversed.\n");
        }

        private void AddRotateStep()
        {
            var userInputHandler = new UserInputHandler();
            int degrees = UserInputHandler.GetIntegerInput("\nEnter the degrees to rotate: ");

            _context.Degrees = degrees;

            _pipeline.AddStep(new ImageRotatingStep());
            Console.WriteLine("\nImage rotated.\n");
        }

        private void AddWatermarkStep()
        {
            var userInputHandler = new UserInputHandler();
            string watermark = UserInputHandler.GetStringInput("\nEnter the watermark text: ");

            _context.Watermark = watermark;

            _pipeline.AddStep(new ImageWatermarkingStep());
            Console.WriteLine("\nWatermark added.\n");
        }
    }
}
