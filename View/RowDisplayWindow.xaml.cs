using System;
using System.Windows;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using ImageProcessingFramework.Model;
using ImageProcessingFramework.ViewModel;
using OxyPlot;

namespace ImageProcessingFramework
{
    public partial class RowDisplayWindow : Window
    {
        public RowDisplayWindow()
        {
            InitializeComponent();
        }

        public RowDisplayWindow(Image<Bgr, byte> colorImage, Image<Bgr, byte> colorProccesedImage = null)
        {

            InitializeComponent();

            var rowDisplayCommands = new RowDisplayCommands();
            originalImageView.Model = rowDisplayCommands.PlotColorImage(colorImage);
            if (colorProccesedImage != null)
            {
                processedImageView.Model = rowDisplayCommands.PlotColorImage(colorProccesedImage);
            }
            checkBoxBlue.Visibility = Visibility.Visible;
            checkBoxGreen.Visibility = Visibility.Visible;
            checkBoxRed.Visibility = Visibility.Visible;
        }

        public RowDisplayWindow(Image<Gray, byte> grayImage, Image<Gray, byte> grayProccesedImage = null)
        {

            InitializeComponent();

            var rowDisplayCommands = new RowDisplayCommands();
            originalImageView.Model = rowDisplayCommands.PlotGrayImage(grayImage);
            if (grayProccesedImage != null)
            {
                processedImageView.Model = rowDisplayCommands.PlotGrayImage(grayProccesedImage);
            }
        }

        private void AddGreenSeries(object sender, RoutedEventArgs e)
        {
            SetVisibility(originalImageView.Model, 0, true);
            SetVisibility(processedImageView.Model, 0, true);
        }

        private void RemoveGreenSeries(object sender, RoutedEventArgs e)
        {
            SetVisibility(originalImageView.Model, 0, false);
            SetVisibility(processedImageView.Model, 0, false);
        }

        private void AddRedSeries(object sender, RoutedEventArgs e)
        {
            SetVisibility(originalImageView.Model, 1, true);
            SetVisibility(processedImageView.Model, 1, true);
        }

        private void RemoveRedSeries(object sender, RoutedEventArgs e)
        {
            SetVisibility(originalImageView.Model, 1, false);
            SetVisibility(processedImageView.Model, 1, false);
        }

        private void AddBlueSeries(object sender, RoutedEventArgs e)
        {
            SetVisibility(originalImageView.Model, 2, true);
            SetVisibility(processedImageView.Model, 2, true);
        }

        private void RemoveBlueSeries(object sender, RoutedEventArgs e)
        {
            SetVisibility(originalImageView.Model, 2, false);
            SetVisibility(processedImageView.Model, 2, false);
        }

        private void SetVisibility(PlotModel model, int indexSeries, bool isVisible)
        {
            if (model != null)
            {
                model.Series[indexSeries].IsVisible = isVisible;
                model.InvalidatePlot(true);
            }
        }
    }
}
