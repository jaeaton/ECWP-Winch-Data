namespace Models
{
    public partial class TabItemModel : ObservableObject
    {
        [ObservableProperty]
        private string header = string.Empty;

        [ObservableProperty]
        private string content = string.Empty;

        public TabItemModel() { }

        public TabItemModel(string _header, string _content)
        {
            Header = _header;
            Content = _content;
        }
        public override string ToString()
        {
            return Header;
        }
    }


}
