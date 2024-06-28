using Image_Processing_Pipeline.Interfaces;
using System.Drawing;

namespace Image_Processing_Pipeline.Classes
{
    public class ImageRotatingStep : IPipelineStep
    {
        public async Task ExecuteAsync(IPipelineContext context)
        {
            await Task.Run(() =>
            {
                Bitmap rotatedImage = new Bitmap(context.Image.Width, context.Image.Height);

                using var graphics = Graphics.FromImage(rotatedImage);

                graphics.TranslateTransform((float)context.Image.Width / 2, (float)context.Image.Height / 2);
                graphics.RotateTransform(context.Degrees);
                graphics.TranslateTransform(-(float)context.Image.Width / 2, -(float)context.Image.Height / 2);
                graphics.DrawImage(context.Image, new Point(0, 0));

                context.Image = rotatedImage;
            });
        }
    }
}
