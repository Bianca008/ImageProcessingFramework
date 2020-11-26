using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ImageProcessingAlgorithms.Tools
{
    public class Hough
    {
        public static Image<Gray, byte> Sobel(Image<Gray, byte> InputImage, int T)
        {
            Image<Gray, byte> Result = new Image<Gray, byte>(InputImage.Size);

            int gx;
            int gy;

            for (int y = 1; y < InputImage.Height - 1; y++)
            {
                for (int x = 1; x < InputImage.Width - 1; x++)
                {
                    gx = InputImage.Data[y - 1, x + 1, 0] - InputImage.Data[y - 1, x - 1, 0] + InputImage.Data[y + 1, x + 1, 0] - InputImage.Data[y + 1, x - 1, 0] + 2 * InputImage.Data[y, x + 1, 0] - 2 * InputImage.Data[y, x - 1, 0];
                    gy = InputImage.Data[y + 1, x - 1, 0] - InputImage.Data[y - 1, x - 1, 0] + InputImage.Data[y + 1, x + 1, 0] - InputImage.Data[y - 1, x + 1, 0] + 2 * InputImage.Data[y + 1, x, 0] - 2 * InputImage.Data[y - 1, x, 0];
                    double val = Math.Sqrt(gx * gx + gy * gy);
                    if (val > T)
                        Result.Data[y, x, 0] = 255;
                }
            }
            return Result;
        }
    }
}
