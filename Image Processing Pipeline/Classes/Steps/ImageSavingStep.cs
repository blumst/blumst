using Image_Processing_Pipeline.Interfaces;

namespace Image_Processing_Pipeline.Classes
{
    public class ImageSavingStep : IPipelineStep
    {
        public async Task ExecuteAsync(IPipelineContext context) => 
            await Task.Run(() => context.Image.Save("2.jpg"));
    }
}
