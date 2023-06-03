namespace CheckpointMobile1;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        Routing.RegisterRoute("ScannerPage", typeof(ScannerPage));
        MainPage = new AppShell();
	}
}
