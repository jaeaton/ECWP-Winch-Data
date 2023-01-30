using CommunityToolkit.Mvvm.Input;

namespace ViewModels
{
    public partial class ProcessingViewModel : ObservableObject
    {
        //public  ParseDataStore? parseData = new ParseDataStore();
        [ObservableProperty]
        private ParseDataStore? _parseData = new ParseDataStore();

        [RelayCommand]
        void File_Location()
        {

        }
        [RelayCommand]
        void Save_Config()
        {

        }

        [RelayCommand]
        void Combine_Files() 
        {
            ProcessingReadFilesViewModel.CombineFiles(ParseData);
        }

        [RelayCommand]
        void Process_Files() 
        {
        ProcessingReadFilesViewModel.ParseFiles(ParseData);
        }
    }

    
}
