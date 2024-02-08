namespace ViewModels
{
    public class MessageBoxViewModel
    {
        public async static Task DisplayMessage(string message)
        {
            var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandard("Note", message);
            await messageBoxStandardWindow.ShowAsync();
        }
    }
        
    
}
