using Image_Processing_Pipeline.Interfaces;
using System.Drawing;

namespace Image_Processing_Pipeline.Classes
{
    public class Picture : IPipelineContext
    {
        public string? ImageName { get; set; }
        public string? Watermark { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Degrees { get; set; }
        Bitmap? IPipelineContext.Image { get; set; }
    }
}
