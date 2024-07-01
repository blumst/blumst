using Image_Processing_Pipeline.Interfaces;
using System.Drawing;

namespace Image_Processing_Pipeline.Classes
{
    public class ImageRotatingStep : IPipelineStep, IStepPrototype
    {
        public async Task ExecuteAsync(IPipelineContext context, CancellationToken token)
        {
            await Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();

                Bitmap rotatedImage = new Bitmap(context.Image.Width, context.Image.Height);

                using var graphics = Graphics.FromImage(rotatedImage);

                graphics.TranslateTransform((float)context.Image.Width / 2, (float)context.Image.Height / 2);
                graphics.RotateTransform(context.Degrees);
                graphics.TranslateTransform(-(float)context.Image.Width / 2, -(float)context.Image.Height / 2);
                graphics.DrawImage(context.Image, new Point(0, 0));

                context.Image = rotatedImage;
            }, token);
        }

        public IPipelineStep Clone() => new ImageRotatingStep();
    }
}
