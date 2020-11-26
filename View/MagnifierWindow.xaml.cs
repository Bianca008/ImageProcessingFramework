using Emgu.CV;
using ImageProcessingFramework.ViewModel;
using Emgu.CV.Structure;
using System.Windows;


namespace ImageProcessingFramework.View
{
    public partial class MagnifierWindow : Window
    {
        public MagnifierWindow()
        {
            InitializeComponent();
        }

        public MagnifierWindow(Image<Bgr, byte> colorImage, Image<Bgr, byte> colorProccesedImage = null)
        {
            InitializeComponent();

            var magnifierCommands = new MagnifierCommands();
            originalImageView.Model = magnifierCommands.MagnifierColorImage(colorImage);
            if (colorProccesedImage != null)
            {
                processedImageView.Model = magnifierCommands.MagnifierColorImage(colorProccesedImage);
            }
        }

        public MagnifierWindow(Image<Gray, byte> grayImage, Image<Gray, byte> grayProccesedImage = null)
        {

            InitializeComponent();

            var magnifierCommands = new MagnifierCommands();
            originalImageView.Model = magnifierCommands.MagnifierGrayImage(grayImage);
            if (grayProccesedImage != null)
            {
                processedImageView.Model = magnifierCommands.MagnifierGrayImage(grayProccesedImage);
            }
        }
    }
}
