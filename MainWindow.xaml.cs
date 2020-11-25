using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static ImageProcessingFramework.Model.DataProvider;

namespace ImageProcessingFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImageMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var position = e.GetPosition(initialImage);

            xPos.Text = "X: " + ((int)(position.X)).ToString() + " ";
            yPos.Text = "Y: " + ((int)(position.Y)).ToString();
        }

        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (sender == ScrollViewerInitial)
            {
                ScrollViewerProcessed.ScrollToVerticalOffset(e.VerticalOffset);
                ScrollViewerProcessed.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
            else
            {
                ScrollViewerInitial.ScrollToVerticalOffset(e.VerticalOffset);
                ScrollViewerInitial.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        private void MousePressed(object sender, MouseButtonEventArgs e)
        {
            MousePosition = e.GetPosition(initialImage);
        }
    }
}