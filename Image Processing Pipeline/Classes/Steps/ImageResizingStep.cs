using Image_Processing_Pipeline.Interfaces;
using System.Drawing;

namespace Image_Processing_Pipeline.Classes
{
    public class ImageResizingStep : IPipelineStep
    {
        public async Task ExecuteAsync(IPipelineContext context)
        {
          await Task.Run(() =>
            {
                var newSize = new Size(context.Width, context.Height);
                var resized = new Bitmap(context.Image, newSize);

                context.Image = resized;
            });
        }
    }
}
