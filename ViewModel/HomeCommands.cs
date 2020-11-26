using Emgu.CV;
using Emgu.CV.Structure;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using ImageProcessingAlgorithms.Tools;
using ImageConverter = ImageProcessingFramework.Model.ImageConverter;
using ImageProcessingFramework.View;
using System.IO;
using System.Text;
using System;

namespace ImageProcessingFramework.ViewModel
{
    class HomeCommands : INotifyPropertyChanged
    {
        public ImageSource InitialImage
        {
            get;
            set;
        }

        public ImageSource ProcessedImage
        {
            get;
            set;
        }

        private Image<Bgr, byte> m_colorImage;
        private Image<Bgr, byte> m_colorProcessedImage;
        private Image<Gray, byte> m_grayImage;
        private Image<Gray, byte> m_grayProcessedImage;
        private ICommand m_rowDisplay;
        private ICommand m_loadColorImage;
        private ICommand m_loadGrayImage;
        private ICommand m_exitWindow;
        private ICommand m_resetButton;
        private ICommand m_saveAsOriginalImage;
        private ICommand m_invertImage;
        private ICommand m_saveImage;
        private ICommand m_copyImage;
        private ICommand m_convertToGrayImage;
        private ICommand m_magnifier;
        private ICommand m_binomial;
        private ICommand m_sobel;
        private bool m_isColorImage;
        private bool m_isPressedConvertButton;
        private string path = "Info.txt";

        private void WriteIntoFile(string str)
        {
            if (File.Exists(path))
            {
                StreamWriter fs = File.AppendText(path);
                fs.WriteLine(str);
                fs.Close();
            }
            else using (FileStream fs = File.Create(path))
                {
                    StreamWriter stream = File.AppendText(path);
                    stream.WriteLine(str);
                    stream.Close();
                }
        }

        private void ResetUiToInitial(object parameter)
        {
            m_colorImage = null;
            m_colorProcessedImage = null;
            m_grayImage = null;
            m_grayProcessedImage = null;
            InitialImage = null;
            ProcessedImage = null;
            m_isPressedConvertButton = false;
            OnPropertyChanged("InitialImage");
            OnPropertyChanged("ProcessedImage");

            ResetZoom(parameter);
        }

        public void LoadColorImage(object parameter)
        {
            ResetUiToInitial(parameter);

            var op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "Image files(*.jpg, *.jpeg, *.jpe, *.bmp, *.png) | *.jpg; *.jpeg; *.jpe; *.bmp; *.png";
            op.ShowDialog();
            if (op.FileName.CompareTo("") == 0)
                return;

            m_colorImage = new Image<Bgr, byte>(op.FileName);
            InitialImage = ImageConverter.Convert(m_colorImage);
            OnPropertyChanged("InitialImage");
            m_isColorImage = true;
            System.IO.File.WriteAllText(path, string.Empty);
        }

        public void LoadGrayImage(object parameter)
        {
            ResetUiToInitial(parameter);

            var op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "Image files(*.jpg, *.jpeg, *.jpe, *.bmp, *.png) | *.jpg; *.jpeg; *.jpe; *.bmp; *.png";
            op.ShowDialog();
            if (op.FileName.CompareTo("") == 0)
                return;

            m_grayImage = new Image<Gray, byte>(op.FileName);
            InitialImage = ImageConverter.Convert(m_grayImage);
            OnPropertyChanged("InitialImage");
            m_isColorImage = false;
            System.IO.File.WriteAllText(path, string.Empty);

        }

        public void InvertImage(object parameter)
        {
            if (m_grayImage != null)
            {
                m_grayProcessedImage = Tools.Invert(m_grayImage);
                ProcessedImage = ImageConverter.Convert(m_grayProcessedImage);
                OnPropertyChanged("ProcessedImage");
                WriteIntoFile("Invert grayscale image.");

            }
            else if (m_colorImage != null)
            {
                m_colorProcessedImage = Tools.Invert(m_colorImage);
                ProcessedImage = ImageConverter.Convert(m_colorProcessedImage);
                OnPropertyChanged("ProcessedImage");
                WriteIntoFile("Invert color image.");
            }
            else
            {
                MessageBox.Show("Please add an image!");
            }
        }

        public void SaveImage(object parameter)
        {
            if (m_grayProcessedImage == null && m_colorProcessedImage == null)
            {
                MessageBox.Show("If you want to save your processed image, please add and process an image first!");
                return;
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "image.jpg";
            saveFile.Filter = "Image files(*.jpg, *.jpeg, *.jpe, *.bmp, *.png) | *.jpg; *.jpeg; *.jpe; *.bmp; *.png";

            saveFile.ShowDialog();

            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(
                System.Drawing.Imaging.Encoder.Quality,
                (long)100
            );

            var jpegCodec = (from codec in ImageCodecInfo.GetImageEncoders()
                             where codec.MimeType == "image/jpeg"
                             select codec).Single();

            if (m_grayProcessedImage != null)
                m_grayProcessedImage.Bitmap.Save(saveFile.FileName, jpegCodec, encoderParams);

            if (m_colorProcessedImage != null)
                m_colorProcessedImage.Bitmap.Save(saveFile.FileName, jpegCodec, encoderParams);
        }

        public void ExitWindow(object parameter)
        {
            System.Environment.Exit(0);
        }

        public void ResetZoom(object parameter)
        {
            if (parameter is Slider slider)
                slider.Value = 1;
            OnPropertyChanged("buttonResetZoom");
        }

        public void SaveAsOriginal(object parameter)
        {
            switch (m_isColorImage)
            {
                case true:
                    {
                        if (m_colorProcessedImage == null && m_grayProcessedImage == null)
                        {
                            System.Windows.MessageBox.Show("Doesn't exist processed image.");
                            return;
                        }

                        if (m_isPressedConvertButton == true)
                        {
                            m_grayImage = m_grayProcessedImage;
                            InitialImage = ImageConverter.Convert(m_grayImage);
                            m_grayProcessedImage = null;
                        }
                        else
                        {
                            m_colorImage = m_colorProcessedImage;
                            InitialImage = ImageConverter.Convert(m_colorImage);
                            m_colorProcessedImage = null;
                        }

                        ProcessedImage = null;
                        OnPropertyChanged("InitialImage");
                        OnPropertyChanged("ProcessedImage");
                        break;
                    }

                case false:
                    {
                        if (m_grayProcessedImage == null)
                        {
                            System.Windows.MessageBox.Show("Doesn't exist processed image.");
                            return;
                        }

                        m_grayImage = m_grayProcessedImage;
                        InitialImage = ImageConverter.Convert(m_grayImage);
                        m_grayProcessedImage = null;
                        ProcessedImage = null;
                        OnPropertyChanged("InitialImage");
                        OnPropertyChanged("ProcessedImage");
                        break;
                    }
            }
        }

        public void CopyImage(object parameter)
        {
            if (m_isColorImage == true)
            {
                m_colorProcessedImage = Tools.Copy(m_colorImage);
                ProcessedImage = ImageConverter.Convert(m_colorProcessedImage);
                OnPropertyChanged("ProcessedImage");
                WriteIntoFile("Copy color image.");
                return;
            }
            else
            {
                if (m_grayImage != null)
                {
                    m_grayProcessedImage = Tools.Copy(m_grayImage);
                    ProcessedImage = ImageConverter.Convert(m_grayProcessedImage);
                    OnPropertyChanged("ProcessedImage");
                    WriteIntoFile("Copy grayscale image.");
                    return;
                }
            }
            System.Windows.MessageBox.Show("Please add an image.");
        }

        public void ConvertToGray(object parameter)
        {
            if (m_isColorImage == true)
            {
                m_grayProcessedImage = Tools.Convert(m_colorImage);
                ProcessedImage = ImageConverter.Convert(m_grayProcessedImage);
                OnPropertyChanged("ProcessedImage");
                m_isPressedConvertButton = true;
                return;
            }

            System.Windows.MessageBox.Show(m_colorImage != null
               ? "It is possible to copy only colored images."
               : "Please add a colored image first.");
        }

        public void GrayLevelsRow(object parameter)
        {
            if (m_isColorImage == true)
            {
                if (m_colorImage != null)
                {
                    var rowDisplayWindow = new RowDisplayWindow(m_colorImage, m_colorProcessedImage);
                    rowDisplayWindow.Show();
                }
            }
            else
            {
                if (m_grayImage != null)
                {
                    var rowDisplayWindow = new RowDisplayWindow(m_grayImage, m_grayProcessedImage);
                    rowDisplayWindow.Show();
                }
                else System.Windows.MessageBox.Show("Please load an image!");
            }
        }

        public void MagnifierShow(object parameter)
        {
            if (m_isColorImage == true)
            {
                if (m_colorImage != null)
                {
                    var magnifierWindow = new MagnifierWindow(m_colorImage, m_colorProcessedImage);
                    magnifierWindow.Show();
                }
            }
            else
            {
                if (m_grayImage != null)
                {
                    var magnifierWindow = new MagnifierWindow(m_grayImage, m_grayProcessedImage);
                    magnifierWindow.Show();
                }
                else System.Windows.MessageBox.Show("Please load an image!");
            }
        }

        public void Binomial7x7(object parameter)
        {
            if (m_isColorImage == true)
                System.Windows.MessageBox.Show("Please load an grayscale image for this filter!");

            if (m_grayImage != null)
            {
                m_grayProcessedImage = Binomial.Binomial7(m_grayImage);
                ProcessedImage = ImageConverter.Convert(m_grayProcessedImage);
                OnPropertyChanged("ProcessedImage");
                WriteIntoFile("Binomial 7x7 filter.");
                return;
            }
        }

        public void Sobel(object parameter)
        {
            if (m_isColorImage == true)
                System.Windows.MessageBox.Show("Please load an grayscale image for this filter!");

            if (m_grayImage != null)
            {
                DialogBox dialogBox = new DialogBox();
                int param = -1;
                if (dialogBox.ShowDialog() == true)
                {
                    param = Int32.Parse(dialogBox.ResponseText);
                }
                if (param >= 0)
                {
                    m_grayProcessedImage = Hough.Sobel(m_grayImage, param);
                    ProcessedImage = ImageConverter.Convert(m_grayProcessedImage);
                    OnPropertyChanged("ProcessedImage");
                    WriteIntoFile("Sobel filter with threshold " + param.ToString() + " .");
                }
                else
                    System.Windows.MessageBox.Show("Threshold must be between 0 and 255.");
            }
        }

        public ICommand AddColorImage
        {
            get
            {
                if (m_loadColorImage == null)
                    m_loadColorImage = new RelayCommand(LoadColorImage);
                return m_loadColorImage;
            }
        }

        public ICommand AddGrayImage
        {
            get
            {
                if (m_loadGrayImage == null)
                    m_loadGrayImage = new RelayCommand(LoadGrayImage);
                return m_loadGrayImage;
            }
        }

        public ICommand Exit
        {
            get
            {
                if (m_exitWindow == null)
                    m_exitWindow = new RelayCommand(ExitWindow);
                return m_exitWindow;
            }

        }

        public ICommand Reset
        {
            get
            {
                if (m_resetButton == null)
                    m_resetButton = new RelayCommand(ResetZoom);
                return m_resetButton;
            }
        }

        public ICommand SaveAsOriginalImage
        {
            get
            {
                if (m_saveAsOriginalImage == null)
                    m_saveAsOriginalImage = new RelayCommand(SaveAsOriginal);
                return m_saveAsOriginalImage;
            }
        }

        public ICommand Invert
        {
            get
            {
                if (m_invertImage == null)
                    m_invertImage = new RelayCommand(InvertImage);
                return m_invertImage;
            }
        }

        public ICommand Copy
        {
            get
            {
                if (m_copyImage == null)
                    m_copyImage = new RelayCommand(CopyImage);
                return m_copyImage;
            }
        }

        public ICommand Save
        {
            get
            {
                if (m_saveImage == null)
                    m_saveImage = new RelayCommand(SaveImage);
                return m_saveImage;
            }
        }

        public ICommand ConvertToGrayImage
        {
            get
            {
                if (m_convertToGrayImage == null)
                    m_convertToGrayImage = new RelayCommand(ConvertToGray);
                return m_convertToGrayImage;
            }
        }

        public ICommand RowDisplay
        {
            get
            {
                if (m_rowDisplay == null)
                    m_rowDisplay = new RelayCommand(GrayLevelsRow);
                return m_rowDisplay;
            }
        }

        public ICommand Magnifier
        {
            get
            {
                if (m_magnifier == null)
                    m_magnifier = new RelayCommand(MagnifierShow);
                return m_magnifier;
            }
        }

        public ICommand Binomial7
        {
            get
            {
                if (m_binomial == null)
                    m_binomial = new RelayCommand(Binomial7x7);
                return m_binomial;
            }
        }

        public ICommand SobelFilter
        {
            get
            {
                if (m_sobel == null)
                    m_sobel = new RelayCommand(Sobel);
                return m_sobel;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
