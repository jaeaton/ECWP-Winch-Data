namespace Models
{
    //[INotifyPropertyChanged]
    public partial class CommunicationModel  : ObservableObject
    {
        [ObservableProperty]
        private string? tcpIpAddress;
        [ObservableProperty]
        private string? portNumber; 
           
        public CommunicationModel()
        {

        }
        public CommunicationModel(string _ipAdress, string _pNumber)
        {
            TcpIpAddress = _ipAdress;

            //int portNumberValue = 50505;
            //int.TryParse(_portNumber, out portNumberValue);
            PortNumber = _pNumber;//portNumberValue;
        }
        //public event PropertyChangedEventHandler PropertyChanged;

        //private void RaisePropertyChanged(string property)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(property));
        //    }
        //}
        //public CommunicationModel ShallowCopy()
        //{
        //    return (CommunicationModel)this.MemberwiseClone();
        //}
    }
}
