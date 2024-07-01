using Image_Processing_Pipeline.Interfaces;

namespace Image_Processing_Pipeline.Classes
{
    public class ImageSavingStep : IPipelineStep, IStepPrototype
    {
        public async Task ExecuteAsync(IPipelineContext context, CancellationToken token) => 
            await Task.Run(() => context.Image.Save("2.jpg"), token);

        public IPipelineStep Clone() => new ImageSavingStep();
    }
}
