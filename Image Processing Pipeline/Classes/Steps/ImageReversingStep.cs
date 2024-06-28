using Image_Processing_Pipeline.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Image_Processing_Pipeline.Classes
{
    public class ImageReversingStep : IPipelineStep
    {
        public async Task ExecuteAsync(IPipelineContext context)
        {
            await Task.Run(() => 
                context.Image.RotateFlip(RotateFlipType.RotateNoneFlipX));
        }
    }
}
