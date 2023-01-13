namespace ViewModels
{
    public class MessageBoxViewModel
    {
        public static void DisplayMessage(string message)
        {
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Note", message);
            messageBoxStandardWindow.Show();
        }
    }
        
    
}
