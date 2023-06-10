namespace CheckpointMobile1;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        Routing.RegisterRoute("ScannerPage", typeof(ScannerPage));
        Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
        Routing.RegisterRoute("SettingsPage", typeof(SettingsPage));
        MainPage = new AppShell();
	}
}
