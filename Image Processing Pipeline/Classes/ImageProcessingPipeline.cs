using Image_Processing_Pipeline.Interfaces;

namespace Image_Processing_Pipeline.Classes
{
    public class ImageProcessingPipeline
    {
        private readonly List<IPipelineStep> _steps;

        public ImageProcessingPipeline()
        {
            _steps = new List<IPipelineStep>();
        }

        public void AddStep(IPipelineStep step)
        {
            _steps.Add(step);
        }

        public async Task ExecutePipelineAsync(IPipelineContext context)
        {
            foreach (var step in _steps)
            {
                await step.ExecuteAsync(context);
            }
        }
    }
}
