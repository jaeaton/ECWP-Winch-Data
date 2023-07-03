namespace ViewModels
{
    public class MessageBoxViewModel
    {
        public async static void DisplayMessage(string message)
        {
            var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandard("Note", message);
            await messageBoxStandardWindow.ShowAsync();
        }
    }
        
    
}
