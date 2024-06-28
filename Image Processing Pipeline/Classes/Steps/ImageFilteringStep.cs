using Image_Processing_Pipeline.Interfaces;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Image_Processing_Pipeline.Classes
{
    internal class ImageFilteringStep : IPipelineStep
    {
        public async Task ExecuteAsync(IPipelineContext context)
        {
            await Task.Run(() =>
            {
                Rectangle rect = new(0, 0, context.Image.Width, context.Image.Height);
                BitmapData bmpData = context.Image.LockBits(rect, ImageLockMode.ReadWrite, context.Image.PixelFormat);

                IntPtr ptr = bmpData.Scan0;

                int bytes = Math.Abs(bmpData.Stride) * context.Image.Height;

                byte[] rgbValues = new byte[bytes];

                Marshal.Copy(ptr, rgbValues, 0, bytes);

                int pixelSize = Image.GetPixelFormatSize(context.Image.PixelFormat) / 8;

                for (int i = 0; i < rgbValues.Length; i += pixelSize)
                {                 
                    byte gray = (byte)(rgbValues[i] * 0.114 + rgbValues[i + 1] * 0.587 + rgbValues[i + 2] * 0.299);
                    rgbValues[i] = rgbValues[i + 1] = rgbValues[i + 2] = gray;
                }

                Marshal.Copy(rgbValues, 0, ptr, bytes);

                context.Image.UnlockBits(bmpData);
            });
        }
    }
}
