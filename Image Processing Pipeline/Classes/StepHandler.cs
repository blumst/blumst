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

        public void AddFilterStep()
        {
            _pipeline.AddStep(new ImageFilteringStep().Clone());
        }

        public void AddResizeStep(int width, int height)
        {
            _context.Width = width;
            _context.Height = height;
            _pipeline.AddStep(new ImageResizingStep().Clone());
        }

        public void AddReverseStep()
        {
            _pipeline.AddStep(new ImageReversingStep().Clone());
        }

        public void AddRotateStep(int degrees)
        {
            _context.Degrees = degrees;
            _pipeline.AddStep(new ImageRotatingStep().Clone());
        }

        public void AddWatermarkStep(string watermark)
        {
            _context.Watermark = watermark;
            _pipeline.AddStep(new ImageWatermarkingStep().Clone());
        }
    }
}
