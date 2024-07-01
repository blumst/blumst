using Image_Processing_Pipeline.Interfaces;
using System.Drawing;


namespace Image_Processing_Pipeline.Classes
{
    public class ImageLoadingStep : IPipelineStep, IStepPrototype
    {
        public async Task ExecuteAsync(IPipelineContext context, CancellationToken token) => 
            context.Image = new Bitmap(await Task.Run(() => Image.FromFile(context.ImageName + ".jpg"), token));

        public IPipelineStep Clone() => new ImageLoadingStep();
    }
}
