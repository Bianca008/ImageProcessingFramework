using Emgu.CV;
using Emgu.CV.Structure;
using OxyPlot;
using System;
using OxyPlot.Axes;
using OxyPlot.Series;
using ImageProcessingFramework.Model;

namespace ImageProcessingFramework.ViewModel
{
    class MagnifierCommands
    {

        public PlotModel MagnifierColorImage(Image<Bgr, byte> colorImage)
        {
            CreateAxisForPlot();

            plotImage.Axes.Add(new LinearColorAxis
            {
                Palette = OxyPalettes.Hot(255)
            });

            var dataGreen = GenerateColorPixel(colorImage, 0);
            var dataRed = GenerateColorPixel(colorImage, 1);
            var dataBlue = GenerateColorPixel(colorImage, 2);

            var heatMapSeriesBlue = new HeatMapSeries
            {
                X0 = 0,
                X1 = 8,
                Y0 = 0,
                Y1 = 8,
                XAxisKey = "xAxis",
                YAxisKey = "yAxis",
                RenderMethod = HeatMapRenderMethod.Rectangles,
                LabelFontSize = 0.2,
                Data = dataBlue
            };
            var heatMapSeriesGreen = new HeatMapSeries
            {
                X0 = 0,
                X1 = 8,
                Y0 = 0,
                Y1 = 8,
                XAxisKey = "xAxis",
                YAxisKey = "yAxis",
                RenderMethod = HeatMapRenderMethod.Rectangles,
                LabelFontSize = 0.2,
                Data = dataGreen
            };
            var heatMapSeriesRed = new HeatMapSeries
            {
                X0 = 0,
                X1 = 8,
                Y0 = 0,
                Y1 = 8,
                XAxisKey = "xAxis",
                YAxisKey = "yAxis",
                RenderMethod = HeatMapRenderMethod.Rectangles,
                LabelFontSize = 0.2,
                Data = dataRed
            };
            plotImage.Series.Add(heatMapSeriesBlue);
            plotImage.Series.Add(heatMapSeriesGreen);
            plotImage.Series.Add(heatMapSeriesRed);

            return plotImage;
        }

        public PlotModel MagnifierGrayImage(Image<Gray, byte> grayImage)
        {
            CreateAxisForPlot();

            plotImage.Axes.Add(new LinearColorAxis
            {
                Palette = OxyPalettes.Gray(255)
            });

            var data = GenerateGrayPixel(grayImage);
            
            var heatMapSeries = new HeatMapSeries
            {
                X0 = 0,
                X1 = 8,
                Y0 = 0,
                Y1 = 8,
                XAxisKey = "xAxis",
                YAxisKey = "yAxis",
                RenderMethod = HeatMapRenderMethod.Rectangles,
                LabelFontSize = 0.2,
                Data = data
            };
            plotImage.Series.Add(heatMapSeries);

            return plotImage;
        }

        public void CreateAxisForPlot()
        {
            plotImage = new PlotModel();
            plotImage.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                Key = "xAxis",
                ItemsSource = new[] {
                                    "1",
                                    "2",
                                    "3",
                                    "4",
                                    "5",
                                    "6",
                                    "7",
                                    "8",
                                    "9"
                }
            });

            plotImage.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                Key = "yAxis",
                ItemsSource = new[] {
                                    "1",
                                    "2",
                                    "3",
                                    "4",
                                    "5",
                                    "6",
                                    "7",
                                    "8",
                                    "9"
                }
            });
        }

        private double[,] GenerateColorPixel(Image<Bgr, byte> colorImage, int channel)
        {
            var data = new double[9, 9];
            int i = 0;
            for (int x = -4; x < 5; ++x)
            {
                int j = 0;
                for (int y = -4; y < 5; ++y)
                {
                    data[j, i] = colorImage.Data[(int)DataProvider.MousePosition.Y + y,
                        (int)DataProvider.MousePosition.X + x,
                        channel];
                    ++j;
                }
                ++i;
            }

            return data;
        }

        private double[,] GenerateGrayPixel(Image<Gray, byte> grayImage)
        {
            var data = new double[9, 9];
            int i = 0;
            for (int x = -4; x <= 4; ++x)
            {
                int j = 0;
                for (int y = 4; y >= -4; --y)
                {
                    data[i, j] = grayImage.Data[(int)DataProvider.MousePosition.Y + y,
                        (int)DataProvider.MousePosition.X + x,
                        0];
                    ++j;
                }
                ++i;
            }

            return data;
        }

        public PlotModel plotImage { get; private set; }
    }
}