using System.Windows;
using System.Windows.Controls;

namespace Common.UserControls
{
    /// <summary>
    /// Interaction logic for MessagePanel.xaml
    /// </summary>
    public partial class MessagePanel : UserControl
    {
        public enum MessageType
        {
            Ok,
            OkCancel,
            YesNo
        }

        private MessageType messageType;

        public MessagePanel()
        {
            InitializeComponent();

            SetUiDefault();
        }

        private void SetUiDefault()
        {
            Visibility = Visibility.Hidden;
            Title.Text = null;
            Message.Text = null;
            CheckBoxConfirm.Content = null;
            CheckBoxConfirm.IsChecked = false;
            ButtonProceed.Content = null;
            ButtonProceed.IsEnabled = true;
            ButtonHalt.Content = null;
            ButtonHalt.Visibility = Visibility.Collapsed;
            messageType = MessageType.Ok;
        }

        public void Show(string message)
        {
            Show("Message", message);
        }

        public void Show(string title, string message, MessageType msgType = MessageType.Ok)
        {
            Show(title, message, null, msgType);
        }

        public void Show(string title, string message, string confirm, MessageType msgType = MessageType.Ok)
        {
            Title.Text = title;
            Message.Text = message;
            messageType = msgType;
            CheckBoxConfirm.Content = confirm;

            SetUiState();
        }

        private void SetUiState()
        {
            Title.Visibility = string.IsNullOrEmpty(Message.Text) ? Visibility.Collapsed : Visibility.Visible;

            if (CheckBoxConfirm.Content != null)
            {
                if (messageType == MessageType.Ok)
                {
                    messageType = MessageType.OkCancel;
                }

                CheckBoxConfirm.Visibility = Visibility.Visible;
                ButtonProceed.IsEnabled = false;
            }
            else
            {
                CheckBoxConfirm.Visibility = Visibility.Collapsed;
                ButtonProceed.IsEnabled = true;
            }

            switch (messageType)
            {
                case MessageType.Ok:
                    ButtonProceed.Content = "OK";
                    ButtonHalt.Visibility = Visibility.Collapsed;
                    break;
                case MessageType.OkCancel:
                    ButtonProceed.Content = "OK";
                    ButtonHalt.Content = "Cancel";
                    ButtonHalt.Visibility = Visibility.Visible;
                    break;
                case MessageType.YesNo:
                    ButtonProceed.Content = "Yes";
                    ButtonHalt.Content = "No";
                    ButtonHalt.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }

            Visibility = Visibility.Visible;
        }

        #region Dependeny Properties

        public string MessageResponse
        {
            get { return (string)GetValue(MessageResponseProperty); }
            set { SetValue(MessageResponseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageResponse.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageResponseProperty =
            DependencyProperty.Register("MessageResponse", typeof(string), typeof(MessagePanel), new PropertyMetadata(""));

        #endregion

        #region Event Handlers

        private void ButtonProceed_Click(object sender, RoutedEventArgs e)
        {
            // User wants to proceed
            MessageResponse = "Proceed";
            Visibility = Visibility.Hidden;
            SetUiDefault();
        }

        private void ButtonHalt_Click(object sender, RoutedEventArgs e)
        {
            // User wants to not proceed
            MessageResponse = "Halt";
            SetUiDefault();
        }

        private void CheckBoxConfirm_Checked(object sender, RoutedEventArgs e)
        {
            ButtonProceed.IsEnabled = true;
        }

        private void CheckBoxConfirm_Unchecked(object sender, RoutedEventArgs e)
        {
            ButtonProceed.IsEnabled = false;
        }

        #endregion
    }
}
