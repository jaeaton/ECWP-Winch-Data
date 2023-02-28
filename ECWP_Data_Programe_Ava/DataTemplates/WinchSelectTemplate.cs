namespace DataTemplates
{
    public class WinchSelectTemplate
    {
        public FuncDataTemplate<WinchModel> WinchTemplate { get; } = new FuncDataTemplate<WinchModel>(
            (winchModel) => winchModel is not null, 
            BuildWinchPresenter);

        private static IControl BuildWinchPresenter(WinchModel Winch)
        {
            Panel panel = new Panel();
            {
                for (int i = 1; i <= MainWindowViewModel._configDataStore.NumberOfPlots; i++)
                {
                    StackPanel stackPanel = new StackPanel();
                    {
                        StackPanel stackPanel2 = new StackPanel()
                        {
                            Orientation = Orientation.Horizontal,
                            //var textBlock1 = new TextBlock()
                            //{
                            //    textBlock1.Text = "Winch Name: "
                            //}
                            //TextBlock textBlock1 = new TextBlock { Text = "Winch Name: " },
                            //TextBlock textBlock2 = new TextBlock { [!TextBlock.TextProperty] = new Binding("WinchName"), },
                        };
                       

                    }
                }
            }
            return panel;
        }
    }
}
