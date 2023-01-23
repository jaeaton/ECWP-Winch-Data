namespace Models
{
    internal class WinchModel
    {
        public int Id { get; set; }
        public string WinchName { get; set; }
        public string FileExtension { get; set; }
        public WinchModel(int id, string winchName, string fileExtension)
        {
            Id = id;
            WinchName = winchName;
            FileExtension = fileExtension;
        }
    }
}
