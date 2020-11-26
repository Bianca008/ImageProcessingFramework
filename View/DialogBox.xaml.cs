using System.Windows;

namespace ImageProcessingFramework.View
{

    public partial class DialogBox : Window
    {
        public DialogBox()
        {
            InitializeComponent();
        }

        public string ResponseText
        {
            get { return ResponseTextBox.Text; }
            set { ResponseTextBox.Text = value; }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ResponseTextBox.Text.Trim().Length > 0)
                if (!System.Text.RegularExpressions.Regex.IsMatch(ResponseTextBox.Text, "[^0-9]"))
                    DialogResult = true;
                else DialogResult = false;
            else DialogResult = false;
        }
    }
}