namespace Models
{
    public class CommunicationModel //: INotifyPropertyChanged
    {
        
        public string IPAddress { get; set; }
       
        public string PortNumber {get; set; }
           
        public CommunicationModel()
        {

        }
        public CommunicationModel(string _ipAdress, string _portNumber)
        {
            IPAddress = _ipAdress;

            //int portNumberValue = 50505;
            //int.TryParse(_portNumber, out portNumberValue);
            PortNumber = _portNumber;//portNumberValue;
        }
        //public event PropertyChangedEventHandler PropertyChanged;

        //private void RaisePropertyChanged(string property)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(property));
        //    }
        //}
    }
}
