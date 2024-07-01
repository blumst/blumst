using Image_Processing_Pipeline.Interfaces;
using System.Drawing;

namespace Image_Processing_Pipeline.Classes
{
    public class ImageResizingStep : IPipelineStep, IStepPrototype
    {
        public async Task ExecuteAsync(IPipelineContext context, CancellationToken token)
        {
            await Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();

                var newSize = new Size(context.Width, context.Height);
                var resized = new Bitmap(context.Image, newSize);

                context.Image = resized;
            }, token);
        }

        public IPipelineStep Clone() => new ImageResizingStep();
    }
}
