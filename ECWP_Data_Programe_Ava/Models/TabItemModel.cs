namespace Models
{
    public partial class TabItemModel : ObservableObject
    {
        [ObservableProperty]
        private string? header;

        [ObservableProperty]
        private string? content;

        public TabItemModel() { }

        public TabItemModel(string? _header, string? _content)
        {
            Header = _header;
            Content = _content;
        }
    }


}
