using Image_Processing_Pipeline.Interfaces;
using System.Drawing;

namespace Image_Processing_Pipeline.Classes
{
    public class ImageReversingStep : IPipelineStep, IStepPrototype
    {
        public async Task ExecuteAsync(IPipelineContext context, CancellationToken token)
        {
            await Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();

                context.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }, token);
        }

        public IPipelineStep Clone() => new ImageReversingStep();
    }
}
