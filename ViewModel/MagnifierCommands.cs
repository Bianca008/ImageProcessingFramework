using Emgu.CV;
using Emgu.CV.Structure;
using OxyPlot;
using System;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace ImageProcessingFramework.ViewModel
{
    class MagnifierCommands
    {

        public PlotModel MagnifierColorImage(Image<Bgr, byte> colorImage)
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

            plotImage.Axes.Add(new LinearColorAxis
            {
                Palette = OxyPalettes.Hot(200)
            });
            var rand = new Random();
            var data = new double[9, 9];
            for (int x = 0; x < 9; ++x)
            {
                for (int y = 0; y < 9; ++y)
                {
                    data[y, x] = rand.Next(0, 200) * (0.13 * (y + 1));
                }
            }
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

        public PlotModel MagnifierGrayImage(Image<Gray, byte> grayImage)
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

            plotImage.Axes.Add(new LinearColorAxis
            {
                Palette = OxyPalettes.Hot(200)
            });
            var rand = new Random();
            var data = new double[9, 9];
            for (int x = 0; x < 9; ++x)
            {
                for (int y = 0; y < 9; ++y)
                {
                    data[y, x] = rand.Next(0, 200) * (0.13 * (y + 1));
                }
            }
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

        public PlotModel plotImage { get; private set; }
    }
}
