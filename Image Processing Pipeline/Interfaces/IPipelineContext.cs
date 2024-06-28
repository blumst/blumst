using System.Drawing;

namespace Image_Processing_Pipeline.Interfaces
{
    public interface IPipelineContext
    {
        public string? ImageName { get; set; }
        public string? Watermark { get; set; }
        public Bitmap? Image { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Degrees { get; set; }
    }
}             
