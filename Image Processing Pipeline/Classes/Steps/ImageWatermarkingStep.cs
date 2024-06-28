using Image_Processing_Pipeline.Interfaces;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Image_Processing_Pipeline.Classes.Steps
{
    public class ImageWatermarkingStep : IPipelineStep
    {
        public async Task ExecuteAsync(IPipelineContext context)
        {
            const string Font = "Times New Roman";

            await Task.Run(() =>
            {
                using var graphics = Graphics.FromImage(context.Image);

                string watermarkString = context.Watermark;
                int fontSize = Math.Min(context.Image.Width, context.Image.Height) / 5;

                Font font = new(Font, fontSize, FontStyle.Bold);

                SolidBrush brush = new(Color.FromArgb(128, 215, 90, 90));

                StringFormat format = new()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.MeasureTrailingSpaces
                };

                Matrix matrix = new();

                matrix.Translate(context.Image.Width / 2, context.Image.Height / 2);
                matrix.Rotate(-45.0f);

                graphics.Transform = matrix;
                graphics.DrawString(watermarkString, font, brush, 0, 0, format);
            });
        }
    }
}
